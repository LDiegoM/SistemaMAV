using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMAV.Web.Data;
using SistemaMAV.Web.ViewModels;
using SistemaMAV.Web.ViewModels.Consultar;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.Controllers;

[AllowAnonymous]
public class ConsultarController : Controller {
    private readonly ApplicationDbContext _context;

    public ConsultarController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: Consultar
    public IActionResult Index() {
        IndexViewModel consultarVM = createIndexViewModel();
        return View(consultarVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index([Bind("FiltroMarca,FiltroTipoUnidad,FiltroModelo,FiltroAnioFabricacion")] IndexViewModel consultarVM) {
        string marcaId = Request.Form["FiltroMarca"].ToString();
        string tipoUnidadId = Request.Form["FiltroTipoUnidad"].ToString();
        string modelo = Request.Form["FiltroModelo"].ToString();
        string anioFabricacion = Request.Form["FiltroAnioFabricacion"].ToString();

        IndexViewModel newVM = createIndexViewModel();

        if (marcaId != "" || tipoUnidadId != "" || modelo != "" || anioFabricacion != "") {
            getPlanillas(marcaId, tipoUnidadId, modelo, anioFabricacion, ref newVM);
            newVM.MostrarResultados = newVM.CantidadItems > 1;
            if (newVM.CantidadItems < 1) {
                ModelState.AddModelError(string.Empty, "No tenemos vehículos en nuestra base de datos con esa información");
            } else if (newVM.CantidadItems == 1) {
                return Redirect("~/Consultar/Items/" + newVM.Items.First().PlanillaId.ToString());
            }

        } else {
            ModelState.AddModelError(string.Empty, "Debe ingresar al menos un valor del vehículo a buscar");
        }

        return View(newVM);
    }

    private IndexViewModel createIndexViewModel() {
        IndexViewModel consultarVM = new IndexViewModel();

        ((List<SelectListItem>)consultarVM.FiltroMarca).Add(new SelectListItem() { Value = "", Text = "Todas las marcas" });
        if (_context.Marca != null) {
            foreach (Marca unaMarca in _context.Marca) {
                ((List<SelectListItem>)consultarVM.FiltroMarca).Add(new SelectListItem() {
                    Value = unaMarca.MarcaId.ToString(),
                    Text = unaMarca.Detalle
                });
            }
        }

        ((List<SelectListItem>)consultarVM.FiltroTipoUnidad).Add(new SelectListItem() { Value = "", Text = "Todos" });
        if (_context.TipoUnidad != null) {
            foreach (TipoUnidad unTipoDeUnidad in _context.TipoUnidad) {
                ((List<SelectListItem>)consultarVM.FiltroTipoUnidad).Add(new SelectListItem() {
                    Value = unTipoDeUnidad.TipoUnidadId.ToString(),
                    Text = unTipoDeUnidad.Detalle
                });
            }
        }

        return consultarVM;
    }

    private void getPlanillas(string marcaId, string tipoUnidadId, string modelo, string anioFabricacion, ref IndexViewModel consultarVM) {
        int tmp;
        int? iMarcaId = null;
        if (int.TryParse(marcaId, out tmp))
            iMarcaId = tmp;

        int? iTipoUnidadId = null;
        if (int.TryParse(tipoUnidadId, out tmp))
            iTipoUnidadId = tmp;

        int? iAnioFabricacion = null;
        if (int.TryParse(anioFabricacion, out tmp))
            iAnioFabricacion = tmp;

        consultarVM.Items = (from p in _context.Planilla
                                        join m in _context.Modelo on p.ModeloId equals m.ModeloId
                                        where (iMarcaId == null || m.MarcaId == iMarcaId)
                                            && (iTipoUnidadId == null || m.TipoUnidadId == iTipoUnidadId)
                                            && m.Detalle.ToLower().Contains(modelo.ToLower())
                                            && (iAnioFabricacion == null || p.AnioFabricacion == iAnioFabricacion)
                                            && p.Activo == true
                                        select new IndexItemConsulta {
                                            PlanillaId = p.PlanillaId,
                                            Detalle = p.Detalle + " - " + p.AnioFabricacion.ToString()
                                        }).ToList();
        consultarVM.CantidadItems = consultarVM.Items.Count();
    }

    // GET: Consultar/Items/5
    public async Task<IActionResult> Items(int? id) {
        if (id == null) {
            return NotFound();
        }
        int planillaId = (int)id;

        Planilla? planilla = await _context.Planilla
            .Include(p => p.Modelo)
            .FirstOrDefaultAsync(p => p.PlanillaId == planillaId);
        if (planilla == null) {
            return NotFound();
        }

        List<PlanillaItem>? items = await _context.PlanillaItem
            .Include(pi => pi.ItemMantenimiento)
            .Where(pi => pi.PlanillaId == planillaId).ToListAsync();
        if (items == null) {
            return NotFound();
        }

        ItemsViewModel itemsVM = new ItemsViewModel() {
            Planilla = planilla,
        };
        foreach(PlanillaItem item in items) {
            itemsVM.Items.Add(new PlanillaItemViewModel(item));
        }

        return View(itemsVM);
    }
}
