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

public class PlanillaItemController : Controller {
    private readonly ApplicationDbContext _context;

    public PlanillaItemController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: PlanillaItem/5
    public async Task<IActionResult> Index(int? id) {
        List<PlanillaItem> planillaItems;
        if (id == null)
            planillaItems = await _context.PlanillaItem.ToListAsync();
        else
            planillaItems = await _context.PlanillaItem.Where(i => i.PlanillaId == id).ToListAsync();

        List<PlanillaItemViewModel> planillaItemsVM = new List<PlanillaItemViewModel>();
        foreach(PlanillaItem unaPlanillaItem in planillaItems) {
            unaPlanillaItem.Planilla = await _context.Planilla.FirstOrDefaultAsync(p => p.PlanillaId == unaPlanillaItem.PlanillaId);
            unaPlanillaItem.ItemMantenimiento = await _context.ItemMantenimiento.FirstOrDefaultAsync(i => i.ItemMantenimientoId == unaPlanillaItem.ItemMantenimientoId);
            planillaItemsVM.Add(new PlanillaItemViewModel(unaPlanillaItem));
        }
        return View(planillaItemsVM);
    }

    // GET: PlanillaItem/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null) {
            return NotFound();
        }

        var planillaItem = await _context.PlanillaItem
            .Include(p => p.ItemMantenimiento)
            .Include(p => p.Planilla)
            .FirstOrDefaultAsync(m => m.PlanillaItemId == id);
        if (planillaItem == null) {
            return NotFound();
        }

        return View(new PlanillaItemViewModel(planillaItem));
    }

    // GET: PlanillaItem/Create
    public IActionResult Create() {
        ViewData["ItemMantenimientoId"] = new SelectList(_context.ItemMantenimiento, "ItemMantenimientoId", "Detalle");
        ViewData["PlanillaId"] = new SelectList(_context.Planilla, "PlanillaId", "Detalle");
        return View();
    }

    // POST: PlanillaItem/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra")] PlanillaItemViewModel planillaItemVM) {
        if (ModelState.IsValid) {
            _context.Add(planillaItemVM.ToPlanillaItem());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ItemMantenimientoId"] = new SelectList(_context.ItemMantenimiento, "ItemMantenimientoId", "Detalle", planillaItemVM.ItemMantenimientoId);
        ViewData["PlanillaId"] = new SelectList(_context.Planilla, "PlanillaId", "Detalle", planillaItemVM.PlanillaId);
        return View(planillaItemVM);
    }

    // GET: PlanillaItem/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null) {
            return NotFound();
        }

        var planillaItem = await _context.PlanillaItem.FindAsync(id);
        if (planillaItem == null) {
            return NotFound();
        }
        ViewData["ItemMantenimientoId"] = new SelectList(_context.ItemMantenimiento, "ItemMantenimientoId", "Detalle", planillaItem.ItemMantenimientoId);
        ViewData["PlanillaId"] = new SelectList(_context.Planilla, "PlanillaId", "Detalle", planillaItem.PlanillaId);
        planillaItem.Planilla = await _context.Planilla.FirstOrDefaultAsync(p => p.PlanillaId == planillaItem.PlanillaId);
        planillaItem.ItemMantenimiento = await _context.ItemMantenimiento.FirstOrDefaultAsync(i => i.ItemMantenimientoId == planillaItem.ItemMantenimientoId);
        return View(new PlanillaItemViewModel(planillaItem));
    }

    // POST: PlanillaItem/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra")] PlanillaItemViewModel planillaItemVM) {
        if (id != planillaItemVM.PlanillaItemId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(planillaItemVM.ToPlanillaItem());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!PlanillaItemExists(planillaItemVM.PlanillaItemId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        var planillaItem = await _context.PlanillaItem.FindAsync(id);
        if (planillaItem == null) {
            return NotFound();
        }
        ViewData["ItemMantenimientoId"] = new SelectList(_context.ItemMantenimiento, "ItemMantenimientoId", "Detalle", planillaItemVM.ItemMantenimientoId);
        ViewData["PlanillaId"] = new SelectList(_context.Planilla, "PlanillaId", "Detalle", planillaItemVM.PlanillaId);
        planillaItem.Planilla = await _context.Planilla.FirstOrDefaultAsync(p => p.PlanillaId == planillaItem.PlanillaId);
        planillaItem.ItemMantenimiento = await _context.ItemMantenimiento.FirstOrDefaultAsync(i => i.ItemMantenimientoId == planillaItem.ItemMantenimientoId);
        return View(planillaItemVM);
    }

    // GET: PlanillaItem/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null) {
            return NotFound();
        }

        var planillaItem = await _context.PlanillaItem
            .Include(p => p.ItemMantenimiento)
            .Include(p => p.Planilla)
            .FirstOrDefaultAsync(m => m.PlanillaItemId == id);
        if (planillaItem == null) {
            return NotFound();
        }

        return View(new PlanillaItemViewModel(planillaItem));
    }

    // POST: PlanillaItem/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var planillaItem = await _context.PlanillaItem.FindAsync(id);
        _context.PlanillaItem.Remove(planillaItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlanillaItemExists(int id) {
        return _context.PlanillaItem.Any(e => e.PlanillaItemId == id);
    }
}
