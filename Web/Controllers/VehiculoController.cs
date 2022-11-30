using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMAV.Web.Data;
using SistemaMAV.Web.ViewModels;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.Controllers;

[Authorize]
public class VehiculoController : Controller {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public VehiculoController(UserManager<IdentityUser> userManager, ApplicationDbContext context) {
        _userManager = userManager;
        _context = context;
    }

    // GET: Vehiculo
    public async Task<IActionResult> Index() {
        List<VehiculoViewModel> vehiculosVM = new List<VehiculoViewModel>();
        if (_context.Vehiculo == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
            return NotFound();

        List<Vehiculo> vehiculos = await _context.Vehiculo.Where(v => v.UserId == user.Id).ToListAsync();
        foreach(Vehiculo unVehiculo in vehiculos) {
            unVehiculo.Modelo = new Modelo();
            if (_context.Modelo != null)
                unVehiculo.Modelo = await _context.Modelo.FirstOrDefaultAsync<Modelo>(m => m.ModeloId == unVehiculo.ModeloId)??unVehiculo.Modelo;
            vehiculosVM.Add(new VehiculoViewModel(unVehiculo));
        }
        return View(vehiculosVM);
    }

    // GET: Vehiculo/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || _context.Vehiculo == null) {
            return NotFound();
        }

        var vehiculo = await _context.Vehiculo
            .Include(p => p.Modelo)
            .FirstOrDefaultAsync(m => m.VehiculoId == id);
        if (vehiculo == null) {
            return NotFound();
        }

        // Verifica que el propietario del vehículo sea el usuario actual
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
            return NotFound();
        if (vehiculo.UserId != user.Id)
            return NotFound();

        vehiculo.UserId = ".";
        return View(new VehiculoViewModel(vehiculo));
    }

    // GET: Vehiculo/Create
    public IActionResult Create() {
        VehiculoViewModel vehiculoVM = new VehiculoViewModel();
        vehiculoVM.UserId = ".";
        vehiculoVM.Activo = true;
        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle");
        return View(vehiculoVM);
    }

    // POST: Vehiculo/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,ModeloId,Patente,AnioFabricacion,Activo")] VehiculoViewModel vehiculoVM) {
        if (ModelState.IsValid) {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();

            vehiculoVM.UserId = user.Id;
            vehiculoVM.Patente = vehiculoVM.Patente.ToUpper();
            vehiculoVM.FechaAlta = DateTime.Now;
            _context.Add(vehiculoVM.ToVehiculo());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } else {
            var entries = SistemaMAV.Web.Helpers.Utils.GetErrorsList(ModelState);
            return View(vehiculoVM);
        }
        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle", vehiculoVM.ModeloId);
        return View(vehiculoVM);
    }

    // GET: Vehiculo/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Vehiculo == null) {
            return NotFound();
        }

        var vehiculo = await _context.Vehiculo.FindAsync(id);
        if (vehiculo == null)
            return NotFound();

        // Verifica que el propietario del vehículo sea el usuario actual
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
            return NotFound();
        if (vehiculo.UserId != user.Id)
            return NotFound();
        vehiculo.UserId = ".";

        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle", vehiculo.ModeloId);
        return View(new VehiculoViewModel(vehiculo));
    }

    // POST: Vehiculo/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("VehiculoId,UserId,ModeloId,Patente,AnioFabricacion,FechaAlta,Activo")] VehiculoViewModel vehiculoVM) {
        if (id != vehiculoVM.VehiculoId || _context.Vehiculo == null) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            // Verifica que el propietario del vehículo sea el usuario actual
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (vehiculo == null)
                return NotFound();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            if (vehiculo.UserId != user.Id)
                return NotFound();
            vehiculo.ModeloId = vehiculoVM.ModeloId;
            vehiculo.Patente = vehiculoVM.Patente.ToUpper();
            vehiculo.AnioFabricacion = vehiculoVM.AnioFabricacion;
            vehiculo.Activo = vehiculoVM.Activo;
            
            try {
                _context.Update(vehiculo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!VehiculoExists(vehiculoVM.VehiculoId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle", vehiculoVM.ModeloId);
        return View(vehiculoVM);
    }

    // GET: Vehiculo/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.Vehiculo == null) {
            return NotFound();
        }

        var vehiculo = await _context.Vehiculo
            .Include(p => p.Modelo)
            .FirstOrDefaultAsync(m => m.VehiculoId == id);
        if (vehiculo == null) {
            return NotFound();
        }

        vehiculo.UserId = ".";
        return View(new VehiculoViewModel(vehiculo));
    }

    // POST: Vehiculo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (_context.Vehiculo == null)
            return NotFound();
        var vehiculo = await _context.Vehiculo.FindAsync(id);
        if (vehiculo == null)
            return NotFound();

        // Verifica que el propietario del vehículo sea el usuario actual
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
            return NotFound();
        if (vehiculo.UserId != user.Id)
            return NotFound();

        _context.Vehiculo.Remove(vehiculo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VehiculoExists(int id) {
        if (_context.Vehiculo == null)
            return false;
        return _context.Vehiculo.Any(e => e.VehiculoId == id);
    }
}
