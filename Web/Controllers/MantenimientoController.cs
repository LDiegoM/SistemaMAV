using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMAV.Web.Data;
using SistemaMAV.Web.ViewModels;
using SistemaMAV.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace SistemaMAV.Web.Controllers;

public class MantenimientoController : Controller {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public MantenimientoController(UserManager<IdentityUser> userManager, ApplicationDbContext context) {
        _userManager = userManager;
        _context = context;
    }

    // GET: Mantenimiento/Index/5
    public async Task<IActionResult> Index(int? id) { // id is vehiculoId
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
            return NotFound();

        // Si recibe un vehiculoId, valida que pertenezca al usuario
        int vehiculoId = -1;

        if (id != null && _context.Vehiculo != null) {
            vehiculoId = (int)id;
            Vehiculo? vehiculo = await _context.Vehiculo.FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId);
            if (vehiculo == null)
                return NotFound();
        }
        ViewData["VehiculoId"] = vehiculoId;

        List<MantenimientoViewModel> mantenimientosVM = new List<MantenimientoViewModel>();
        if (_context.Mantenimiento == null || _context.Vehiculo == null)
            return View(mantenimientosVM);

        List<Mantenimiento> mantenimientos = (from m in _context.Mantenimiento
                                              join v in _context.Vehiculo on m.VehiculoId equals v.VehiculoId
                                              where (v.Activo == true)
                                                && v.UserId == user.Id
                                                && (vehiculoId == -1 || v.VehiculoId == vehiculoId)
                                              select new Mantenimiento {
                                                  MantenimientoId = m.MantenimientoId,
                                                  VehiculoId = m.VehiculoId,
                                                  TallerId = m.TallerId,
                                                  PlanillaId = m.PlanillaId,
                                                  Fecha = m.Fecha,
                                                  Kilometros = m.Kilometros,
                                                  Precio = m.Precio,
                                              }).ToList();
        foreach(Mantenimiento unMantenimiento in mantenimientos) {
            unMantenimiento.Vehiculo = new Vehiculo();
            if (_context.Vehiculo != null) {
                unMantenimiento.Vehiculo = await _context.Vehiculo.Include(v => v.Modelo).FirstOrDefaultAsync<Vehiculo>(
                    v => v.VehiculoId == unMantenimiento.VehiculoId) ?? unMantenimiento.Vehiculo;
            }
            if (_context.Taller != null)
                unMantenimiento.Taller = await _context.Taller.FirstOrDefaultAsync<Taller>(
                    t => t.TallerId == unMantenimiento.TallerId) ?? unMantenimiento.Taller;
            if (_context.Planilla != null)
                unMantenimiento.Planilla = await _context.Planilla.FirstOrDefaultAsync<Planilla>(
                    p => p.PlanillaId == unMantenimiento.PlanillaId) ?? unMantenimiento.Planilla;

            MantenimientoViewModel mantenimientoVM = new MantenimientoViewModel(unMantenimiento);
            mantenimientoVM.VehiculoDetalle = mantenimientoVM.Vehiculo.Modelo.Detalle + " " + mantenimientoVM.Vehiculo.AnioFabricacion.ToString();

            mantenimientosVM.Add(mantenimientoVM);
        }
        return View(mantenimientosVM);
    }

    // GET: Mantenimiento/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || _context.Mantenimiento == null) {
            return NotFound();
        }

        var mantenimiento = await _context.Mantenimiento
            .Include(m => m.Vehiculo)
            .Include(m => m.Vehiculo.Modelo)
            .Include(m => m.Taller)
            .Include(m => m.Planilla)
            .FirstOrDefaultAsync(m => m.MantenimientoId == id);
        if (mantenimiento == null) {
            return NotFound();
        }

        MantenimientoViewModel mantenimientoVM = new MantenimientoViewModel(mantenimiento);
        mantenimientoVM.VehiculoDetalle = mantenimientoVM.Vehiculo.Modelo.Detalle + " " + mantenimientoVM.Vehiculo.AnioFabricacion.ToString();

        return View(mantenimientoVM);
    }

    // GET: Mantenimiento/Create/5
    public async Task<IActionResult> Create(int? id) { // id is tallerId
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null || _context.Vehiculo == null || _context.Modelo == null || _context.Planilla == null
            || _context.Taller == null || _context.Localidad == null)
            return NotFound();

        CargarListasDeVehiculosYTalleres(id, user.Id);

        return View(new MantenimientoViewModel() {
            Fecha = DateTime.Now,
        });
    }

    // POST: Mantenimiento/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VehiculoId,TallerId,Fecha,Kilometros,Precio")] MantenimientoViewModel mantenimientoVM) {
        if (ModelState.IsValid) {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null || _context.Vehiculo == null)
                return NotFound();

            Vehiculo? vehiculo = await _context.Vehiculo.FirstOrDefaultAsync(v => v.VehiculoId == mantenimientoVM.VehiculoId);
            if (vehiculo == null) {
                ModelState.AddModelError(string.Empty, "El Vehículo ingresado es inválido");
                return View(mantenimientoVM);
            }

            // Valida que el vehículo ingresado sea propiedad del usuario actual.
            if (vehiculo.UserId != user.Id) {
                ModelState.AddModelError(string.Empty, "El Vehículo ingresado no pertenece al usuario");
                return View(mantenimientoVM);
            }

            // Toma la planilla de mantenimiento desde el vehículo ingresado.
            if (_context.Planilla == null) {
                ModelState.AddModelError(string.Empty, "No tenemos datos de mantenimiento para el vehículo ingresado");
                return View(mantenimientoVM);
            }
            Planilla? planilla = await _context.Planilla.FirstOrDefaultAsync(p => p.ModeloId == vehiculo.ModeloId && p.AnioFabricacion == vehiculo.AnioFabricacion && p.Activo == true);
            if (planilla == null) {
                ModelState.AddModelError(string.Empty, "No tenemos datos de mantenimiento para el vehículo ingresado");
                return View(mantenimientoVM);
            }
            mantenimientoVM.PlanillaId = planilla.PlanillaId;

            Mantenimiento mantenimiento = mantenimientoVM.ToMantenimiento();
            _context.Add(mantenimiento);
            await _context.SaveChangesAsync();
            int mantenimientoId = mantenimiento.MantenimientoId;

            // Agrega los Items de Mantenimiento
            if (_context.PlanillaItem != null) {
                List<PlanillaItem> items = _context.PlanillaItem.Where(i => i.PlanillaId == planilla.PlanillaId).ToList();
                foreach(PlanillaItem unItem in items) {
                    MantenimientoItem mantenimientoItem = new MantenimientoItem() {
                        MantenimientoId = mantenimientoId,
                        PlanillaId = mantenimiento.PlanillaId,
                        PlanillaItemId = unItem.PlanillaItemId,
                        Reemplazo = true
                    };
                    _context.Add(mantenimientoItem);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View(mantenimientoVM);
    }

    private void CargarListasDeVehiculosYTalleres(int? tallerId, string userId) {
        if (_context.Vehiculo == null || _context.Modelo == null || _context.Planilla == null
            || _context.Taller == null || _context.Localidad == null)
            return;

        // Crea la lista de vehículos del usuario, activos y que tengan planilla de mantenimiento en el sistema.
        List<KeyValuePair<int, string>> vehiculos = (from v in _context.Vehiculo
                                                    join m in _context.Modelo on v.ModeloId equals m.ModeloId
                                                    join p in _context.Planilla on m.ModeloId equals p.ModeloId
                                                    where v.UserId == userId
                                                        && v.Activo == true
                                                        && v.AnioFabricacion == p.AnioFabricacion
                                                        && p.Activo == true
                                                    select new KeyValuePair<int, string>(
                                                    v.VehiculoId,
                                                    m.Detalle + " " + v.AnioFabricacion.ToString() + " - " + v.Patente
                                                    )).ToList();
        ViewData["VehiculoId"] = new SelectList(vehiculos, "Key", "Value");

        // Crea la lista de talleres.
        List<KeyValuePair<int, string>> talleres = (from t in _context.Taller
                                                    join l in _context.Localidad on t.LocalidadId equals l.LocalidadId
                                                    where t.Activo == true
                                                        && (tallerId == null || t.TallerId == tallerId)
                                                    select new KeyValuePair<int, string>(
                                                        t.TallerId,
                                                        t.Nombre + " - " + l.Nombre
                                                    )).ToList();
        ViewData["TallerId"] = new SelectList(talleres, "Key", "Value");
    }

    // GET: Mantenimiento/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Mantenimiento == null) {
            return NotFound();
        }

        var mantenimiento = await _context.Mantenimiento
            .Include(m => m.Vehiculo)
            .Include(v => v.Vehiculo.Modelo)
            .FirstOrDefaultAsync(m => m.MantenimientoId == id);
        if (mantenimiento == null) {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null || _context.Vehiculo == null)
            return NotFound();

        // Valida que el vehículo del mantenimiento a modificar sea propiedad del usuario
        var vehiculo = await _context.Vehiculo.FirstOrDefaultAsync(v => v.VehiculoId == mantenimiento.VehiculoId && v.UserId == user.Id);
        if (vehiculo == null)
            return NotFound();

        MantenimientoViewModel mantenimientoVM = new MantenimientoViewModel(mantenimiento);
        mantenimientoVM.VehiculoDetalle = mantenimientoVM.Vehiculo.Modelo.Detalle + " " + mantenimientoVM.Vehiculo.AnioFabricacion.ToString() + " - " + mantenimientoVM.Vehiculo.Patente;

        CargarListasDeVehiculosYTalleres(null, user.Id);

        return View(mantenimientoVM);
    }

    // POST: Mantenimiento/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MantenimientoId,VehiculoDetalle,VehiculoId,PlanillaId,TallerId,Fecha,Kilometros,Precio")] MantenimientoViewModel mantenimientoVM) {
        if (id != mantenimientoVM.MantenimientoId) {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null || _context.Vehiculo == null)
            return NotFound();

        if (ModelState.IsValid) {
            try {
                // Valida que el vehículo del mantenimiento a modificar sea propiedad del usuario
                var vehiculo = await _context.Vehiculo.FirstOrDefaultAsync(v => v.VehiculoId == mantenimientoVM.VehiculoId && v.UserId == user.Id);
                if (vehiculo == null)
                    return NotFound();

                _context.Update(mantenimientoVM.ToMantenimiento());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!MantenimientoExists(mantenimientoVM.MantenimientoId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        CargarListasDeVehiculosYTalleres(null, user.Id);

        return View(mantenimientoVM);
    }

    // GET: Mantenimiento/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.Mantenimiento == null) {
            return NotFound();
        }

        var mantenimiento = await _context.Mantenimiento
            .Include(m => m.Vehiculo)
            .Include(m => m.Vehiculo.Modelo)
            .Include(m => m.Taller)
            .FirstOrDefaultAsync(m => m.MantenimientoId == id);
        if (mantenimiento == null) {
            return NotFound();
        }

        MantenimientoViewModel mantenimientoVM = new MantenimientoViewModel(mantenimiento);
        mantenimientoVM.VehiculoDetalle = mantenimientoVM.Vehiculo.Modelo.Detalle + " " + mantenimientoVM.Vehiculo.AnioFabricacion.ToString() + " - " + mantenimientoVM.Vehiculo.Patente;

        return View(mantenimientoVM);
    }

    // POST: Mantenimiento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (_context.Mantenimiento == null)
            return NotFound();
        var mantenimiento = await _context.Mantenimiento.FindAsync(id);
        if (mantenimiento == null) {
            return NotFound();
        }
        _context.Mantenimiento.Remove(mantenimiento);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MantenimientoExists(int id) {
        if (_context.Mantenimiento == null)
            return false;
        return _context.Mantenimiento.Any(e => e.MantenimientoId == id);
    }
}
