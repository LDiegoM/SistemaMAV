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

namespace SistemaMAV.Web.Controllers;

public class ProveedorController : Controller {
    private readonly ApplicationDbContext _context;

    public ProveedorController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: Proveedor
    public async Task<IActionResult> Index() {
        List<ProveedorViewModel> proveedoresVM = new List<ProveedorViewModel>();
        if (_context.Proveedor == null)
            return View(proveedoresVM);

        List<Proveedor> proveedores = await _context.Proveedor.ToListAsync();
        foreach(Proveedor unProveedor in proveedores) {
            unProveedor.Localidad = new Localidad();
            if (_context.Localidad != null)
                unProveedor.Localidad = await _context.Localidad.FirstOrDefaultAsync<Localidad>(l => l.LocalidadId == unProveedor.LocalidadId)??unProveedor.Localidad;
            proveedoresVM.Add(new ProveedorViewModel(unProveedor));
        }
        return View(proveedoresVM);
    }

    // GET: Proveedor/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || _context.Proveedor == null) {
            return NotFound();
        }

        var proveedor = await _context.Proveedor
            .Include(p => p.Localidad)
            .FirstOrDefaultAsync(m => m.ProveedorId == id);
        if (proveedor == null) {
            return NotFound();
        }

        return View(new ProveedorViewModel(proveedor));
    }

    // GET: Proveedor/Create
    public IActionResult Create() {
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View();
    }

    // POST: Proveedor/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProveedorId,Nombre,AnioApertura,Direccion,LocalidadId,Telefono,Email,Activo")] ProveedorViewModel proveedorVM) {
        if (ModelState.IsValid) {
            proveedorVM.VotosPositivos = 0;
            proveedorVM.VotosNegativos = 0;
            _context.Add(proveedorVM.ToProveedor());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View(proveedorVM);
    }

    // GET: Proveedor/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Proveedor == null) {
            return NotFound();
        }

        var proveedor = await _context.Proveedor.FindAsync(id);
        if (proveedor == null) {
            return NotFound();
        }
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View(new ProveedorViewModel(proveedor));
    }

    // POST: Proveedor/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProveedorId,Nombre,AnioApertura,Direccion,LocalidadId,Telefono,Email,Activo,VotosPositivos,VotosNegativos")] ProveedorViewModel proveedorVM) {
        if (id != proveedorVM.ProveedorId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(proveedorVM.ToProveedor());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ProveedorExists(proveedorVM.ProveedorId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View(proveedorVM);
    }

    // GET: Proveedor/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.Proveedor == null) {
            return NotFound();
        }

        var proveedor = await _context.Proveedor
            .Include(p => p.Localidad)
            .FirstOrDefaultAsync(m => m.ProveedorId == id);
        if (proveedor == null) {
            return NotFound();
        }

        return View(new ProveedorViewModel(proveedor));
    }

    // POST: Proveedor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (_context.Proveedor == null)
            return NotFound();
        var proveedor = await _context.Proveedor.FindAsync(id);
        if (proveedor == null) {
            return NotFound();
        }
        _context.Proveedor.Remove(proveedor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProveedorExists(int id) {
        if (_context.Proveedor == null)
            return false;
        return _context.Proveedor.Any(e => e.ProveedorId == id);
    }
}
