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
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SistemaMAV.Web.Helpers;

namespace SistemaMAV.Web.Controllers;

[AllowAnonymous]
public class ConsultarController : Controller {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public ConsultarController(UserManager<IdentityUser> userManager, ApplicationDbContext context) {
        _userManager = userManager;
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
        if (id == null || _context.Planilla == null || _context.PlanillaItem == null) {
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
            ConsultaPlanillaItemViewModel itemVM = new ConsultaPlanillaItemViewModel(item);
            if (_context.Stock != null) {
                Stock? firstStock = _context.Stock.FirstOrDefault(s => s.ItemMantenimientoId == item.ItemMantenimientoId);
                if (firstStock != null)
                    itemVM.HayStock = true;
            }
            itemsVM.Items.Add(itemVM);
        }

        // Busca si el usuario tiene un vehículo con esta planilla de mantenimiento
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user != null && _context.Vehiculo != null) {
            Vehiculo? vehiculo = await _context.Vehiculo.FirstOrDefaultAsync(v =>
                v.UserId == user.Id &&
                v.ModeloId == planilla.ModeloId &&
                v.AnioFabricacion == planilla.AnioFabricacion &&
                v.Activo == true);
            if (vehiculo != null) {
                itemsVM.Vehiculo = new VehiculoViewModel(vehiculo);

                int? proximoMantenimiento = vehiculo.GetProximoMantenimiento(_context);
                if (proximoMantenimiento != null)
                    itemsVM.Vehiculo.ProximoMantenimiento = proximoMantenimiento.ToString() + " Km";
            }
        }

        return View(itemsVM);
    }

    // GET: Consultar/Taller
    public IActionResult Taller()
    {
        SistemaMAV.Web.ViewModels.Consultar.TallerViewModel tallerVM = createTallerViewModel();
        getTalleres("", "", ref tallerVM);
        return View(tallerVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Taller([Bind("FiltroLocalidad,FiltroNombre")] IndexViewModel consultarVM) {
        string localidadId = Request.Form["FiltroLocalidad"].ToString();
        string nombre = Request.Form["FiltroNombre"].ToString();

        SistemaMAV.Web.ViewModels.Consultar.TallerViewModel newVM = createTallerViewModel();

        getTalleres(localidadId, nombre, ref newVM);
        newVM.MostrarResultados = newVM.CantidadItems > 1;
        if (newVM.CantidadItems < 1) {
            ModelState.AddModelError(string.Empty, "No tenemos talleres en nuestra base de datos con esa información");
        }

        return View(newVM);
    }

    private SistemaMAV.Web.ViewModels.Consultar.TallerViewModel createTallerViewModel() {
        SistemaMAV.Web.ViewModels.Consultar.TallerViewModel tallerVM = new SistemaMAV.Web.ViewModels.Consultar.TallerViewModel();

        ((List<SelectListItem>)tallerVM.FiltroLocalidad).Add(new SelectListItem() { Value = "", Text = "Todas las localidades" });
        if (_context.Localidad != null) {
            foreach (Localidad unaLocalidad in _context.Localidad) {
                ((List<SelectListItem>)tallerVM.FiltroLocalidad).Add(new SelectListItem() {
                    Value = unaLocalidad.LocalidadId.ToString(),
                    Text = unaLocalidad.Nombre
                });
            }
        }

        return tallerVM;
    }

    private void getTalleres(string localidadId, string nombre, ref SistemaMAV.Web.ViewModels.Consultar.TallerViewModel consultarVM) {
        int tmp;
        int? iLocalidadId = null;
        if (int.TryParse(localidadId, out tmp))
            iLocalidadId = tmp;

        consultarVM.Talleres = (from t in _context.Taller
                             where (iLocalidadId == null || t.LocalidadId == iLocalidadId)
                                 && (nombre == "" || t.Nombre.ToLower().Contains(nombre.ToLower()))
                                 && t.Activo == true
                             select new TallerItemConsulta {
                                 TallerId = t.TallerId,
                                 Nombre = t.Nombre,
                                 Localidad = _context.Localidad.FirstOrDefault(l => l.LocalidadId == t.LocalidadId),
                                 Direccion = t.Direccion,
                                 Telefono = t.Telefono,
                                 Email = t.Email,
                                 Puntaje = t.VotosPositivos - t.VotosNegativos,
                             }).ToList();
        consultarVM.CantidadItems = consultarVM.Talleres.Count();
    }

    public IActionResult Stock(int? id) { // id is itemMantenimientoId
        if (id == null || _context.Stock == null || _context.ItemMantenimiento == null)
            return NotFound();

        SistemaMAV.Web.ViewModels.Consultar.StockViewModel stockVM = new SistemaMAV.Web.ViewModels.Consultar.StockViewModel();
        stockVM.ItemDetalle = (from d in _context.Stock
                               join i in _context.ItemMantenimiento on d.ItemMantenimientoId equals i.ItemMantenimientoId
                               where d.ItemMantenimientoId == id
                               select i.Detalle).FirstOrDefault();
        if (stockVM.ItemDetalle == null)
            return View(stockVM);

        List<Stock> ? stock = _context.Stock
            .Include(s => s.Proveedor)
            .Include(s => s.Proveedor.Localidad)
            .Include(s => s.ItemMantenimiento)
            .Where(s => s.ItemMantenimientoId == id && s.Proveedor.Activo == true).ToList();
        if (stock == null)
            return View(stockVM);
        foreach(Stock st in stock) {
            SistemaMAV.Web.ViewModels.StockViewModel stVM = new SistemaMAV.Web.ViewModels.StockViewModel(st);
            stockVM.Stock.Add(stVM);
        }

        return View(stockVM);
    }
}
