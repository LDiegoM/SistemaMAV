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

public class PlanillaController : Controller {
    private readonly ApplicationDbContext _context;

    public PlanillaController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: Planilla
    public async Task<IActionResult> Index() {
        var mavDbContext = _context.Planilla.Include(p => p.Modelo);
        List<Planilla> planillas = await _context.Planilla.ToListAsync();
        List<PlanillaViewModel> planillasVM = new List<PlanillaViewModel>();
        foreach(Planilla unaPlanilla in planillas) {
            unaPlanilla.Modelo = await _context.Modelo.FirstOrDefaultAsync(m => m.ModeloId == unaPlanilla.ModeloId);
            planillasVM.Add(new PlanillaViewModel(unaPlanilla));
        }
        return View(planillasVM);
    }

    // GET: Planilla/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null) {
            return NotFound();
        }

        var planilla = await _context.Planilla
            .Include(p => p.Modelo)
            .FirstOrDefaultAsync(m => m.PlanillaId == id);
        if (planilla == null) {
            return NotFound();
        }

        return View(new PlanillaViewModel(planilla));
    }

    // GET: Planilla/Create
    public IActionResult Create() {
        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle");
        return View();
    }

    // POST: Planilla/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PlanillaId,ModeloId,Detalle,AnioFabricacion,Version,Activo")] PlanillaViewModel planillaVM) {
        if (ModelState.IsValid) {
            _context.Add(planillaVM.ToPlanilla());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle", planillaVM.ModeloId);
        return View(planillaVM);
    }

    // GET: Planilla/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null) {
            return NotFound();
        }

        var planilla = await _context.Planilla.FindAsync(id);
        if (planilla == null) {
            return NotFound();
        }
        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle", planilla.ModeloId);
        return View(new PlanillaViewModel(planilla));
    }

    // POST: Planilla/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PlanillaId,ModeloId,Detalle,AnioFabricacion,Version,Activo")] PlanillaViewModel planillaVM) {
        if (id != planillaVM.PlanillaId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(planillaVM.ToPlanilla());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!PlanillaExists(planillaVM.PlanillaId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ModeloId"] = new SelectList(_context.Modelo, "ModeloId", "Detalle", planillaVM.ModeloId);
        return View(planillaVM);
    }

    // GET: Planilla/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null) {
            return NotFound();
        }

        var planilla = await _context.Planilla
            .Include(p => p.Modelo)
            .FirstOrDefaultAsync(m => m.PlanillaId == id);
        if (planilla == null) {
            return NotFound();
        }

        return View(new PlanillaViewModel(planilla));
    }

    // POST: Planilla/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var planilla = await _context.Planilla.FindAsync(id);
        _context.Planilla.Remove(planilla);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlanillaExists(int id) {
        return _context.Planilla.Any(e => e.PlanillaId == id);
    }
}
