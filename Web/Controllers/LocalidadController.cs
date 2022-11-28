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

public class LocalidadController : Controller {
    private readonly ApplicationDbContext _context;

    public LocalidadController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: Localidad
    public async Task<IActionResult> Index() {
        List<LocalidadViewModel> localidadesVM = new List<LocalidadViewModel>();
        if (_context.Localidad == null)
            return View(localidadesVM);

        List<Localidad> localidades = await _context.Localidad.ToListAsync();
        foreach(Localidad unaLocalidad in localidades) {
            localidadesVM.Add(new LocalidadViewModel(unaLocalidad));
        }
        return View(localidadesVM);
    }

    // GET: Localidad/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || _context.Localidad == null) {
            return NotFound();
        }

        var localidad = await _context.Localidad
            .FirstOrDefaultAsync(m => m.LocalidadId == id);
        if (localidad == null) {
            return NotFound();
        }

        return View(new LocalidadViewModel(localidad));
    }

    // GET: Localidad/Create
    public IActionResult Create() {
        return View();
    }

    // POST: Localidad/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LocalidadId,Nombre")] LocalidadViewModel localidadVM) {
        if (ModelState.IsValid) {
            _context.Add(localidadVM.ToLocalidad());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(localidadVM);
    }

    // GET: Localidad/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Localidad == null) {
            return NotFound();
        }

        var localidad = await _context.Localidad.FindAsync(id);
        if (localidad == null) {
            return NotFound();
        }
        return View(new LocalidadViewModel(localidad));
    }

    // POST: Localidad/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("LocalidadId,Nombre")] LocalidadViewModel localidadVM) {
        if (id != localidadVM.LocalidadId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(localidadVM.ToLocalidad());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!LocalidadExists(localidadVM.LocalidadId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(localidadVM);
    }

    // GET: Localidad/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.Localidad == null) {
            return NotFound();
        }

        var localidad = await _context.Localidad
            .FirstOrDefaultAsync(m => m.LocalidadId == id);
        if (localidad == null) {
            return NotFound();
        }

        return View(new LocalidadViewModel(localidad));
    }

    // POST: Localidad/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (_context.Localidad == null)
            return NotFound();
        var localidad = await _context.Localidad.FindAsync(id);
        if (localidad == null) {
            return NotFound();
        }
        _context.Localidad.Remove(localidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LocalidadExists(int id) {
        if (_context.Localidad == null)
            return false;
        return _context.Localidad.Any(e => e.LocalidadId == id);
    }
}
