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

public class TipoUnidadController : Controller {
    private readonly ApplicationDbContext _context;

    public TipoUnidadController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: TipoUnidad
    public async Task<IActionResult> Index() {
        List<TipoUnidad> tiposUnidad = await _context.TipoUnidad.ToListAsync();
        List<TipoUnidadViewModel> tiposUnidadVM = new List<TipoUnidadViewModel>();
        foreach(TipoUnidad unTipoUnidad in tiposUnidad) {
            tiposUnidadVM.Add(new TipoUnidadViewModel(unTipoUnidad));
        }
        return View(tiposUnidadVM);
    }

    // GET: TipoUnidad/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null) {
            return NotFound();
        }

        var tipoUnidad = await _context.TipoUnidad
            .FirstOrDefaultAsync(m => m.TipoUnidadId == id);
        if (tipoUnidad == null) {
            return NotFound();
        }

        return View(new TipoUnidadViewModel(tipoUnidad));
    }

    // GET: TipoUnidad/Create
    public IActionResult Create() {
        return View();
    }

    // POST: TipoUnidad/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TipoUnidadId,Detalle,Activo")] TipoUnidadViewModel tipoUnidadVM) {
        if (ModelState.IsValid) {
            _context.Add(tipoUnidadVM.ToTipoUnidad());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tipoUnidadVM);
    }

    // GET: TipoUnidad/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null) {
            return NotFound();
        }

        var tipoUnidad = await _context.TipoUnidad.FindAsync(id);
        if (tipoUnidad == null) {
            return NotFound();
        }
        return View(new TipoUnidadViewModel(tipoUnidad));
    }

    // POST: TipoUnidad/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TipoUnidadId,Detalle,Activo")] TipoUnidadViewModel tipoUnidadVM) {
        if (id != tipoUnidadVM.TipoUnidadId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(tipoUnidadVM.ToTipoUnidad());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!TipoUnidadExists(tipoUnidadVM.TipoUnidadId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(tipoUnidadVM);
    }

    // GET: TipoUnidad/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null) {
            return NotFound();
        }

        var tipoUnidad = await _context.TipoUnidad
            .FirstOrDefaultAsync(m => m.TipoUnidadId == id);
        if (tipoUnidad == null) {
            return NotFound();
        }

        return View(new TipoUnidadViewModel(tipoUnidad));
    }

    // POST: TipoUnidad/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var tipoUnidad = await _context.TipoUnidad.FindAsync(id);
        _context.TipoUnidad.Remove(tipoUnidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TipoUnidadExists(int id) {
        return _context.TipoUnidad.Any(e => e.TipoUnidadId == id);
    }
}
