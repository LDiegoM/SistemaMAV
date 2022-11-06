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

public class ItemMantenimientoController : Controller {
    private readonly ApplicationDbContext _context;

    public ItemMantenimientoController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: ItemMantenimiento
    public async Task<IActionResult> Index() {
        List<ItemMantenimiento> itemsMantenimiento = await _context.ItemMantenimiento.ToListAsync();
        List<ItemMantenimientoViewModel> itemsMantenimientoVM = new List<ItemMantenimientoViewModel>();
        foreach(ItemMantenimiento unItemMantenimiento in itemsMantenimiento) {
            itemsMantenimientoVM.Add(new ItemMantenimientoViewModel(unItemMantenimiento));
        }
        return View(itemsMantenimientoVM);
    }

    // GET: ItemMantenimiento/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null) {
            return NotFound();
        }

        var itemMantenimiento = await _context.ItemMantenimiento
            .FirstOrDefaultAsync(m => m.ItemMantenimientoId == id);
        if (itemMantenimiento == null) {
            return NotFound();
        }

        return View(new ItemMantenimientoViewModel(itemMantenimiento));
    }

    // GET: ItemMantenimiento/Create
    public IActionResult Create() {
        return View();
    }

    // POST: ItemMantenimiento/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado")] ItemMantenimientoViewModel itemMantenimientoVM) {
        if (ModelState.IsValid) {
            _context.Add(itemMantenimientoVM.ToItemMantenimiento());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(itemMantenimientoVM);
    }

    // GET: ItemMantenimiento/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null) {
            return NotFound();
        }

        var itemMantenimiento = await _context.ItemMantenimiento.FindAsync(id);
        if (itemMantenimiento == null) {
            return NotFound();
        }
        return View(new ItemMantenimientoViewModel(itemMantenimiento));
    }

    // POST: ItemMantenimiento/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado")] ItemMantenimientoViewModel itemMantenimientoVM) {
        if (id != itemMantenimientoVM.ItemMantenimientoId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(itemMantenimientoVM.ToItemMantenimiento());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ItemMantenimientoExists(itemMantenimientoVM.ItemMantenimientoId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(itemMantenimientoVM);
    }

    // GET: ItemMantenimiento/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null) {
            return NotFound();
        }

        var itemMantenimiento = await _context.ItemMantenimiento
            .FirstOrDefaultAsync(m => m.ItemMantenimientoId == id);
        if (itemMantenimiento == null) {
            return NotFound();
        }

        return View(new ItemMantenimientoViewModel(itemMantenimiento));
    }

    // POST: ItemMantenimiento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var itemMantenimiento = await _context.ItemMantenimiento.FindAsync(id);
        _context.ItemMantenimiento.Remove(itemMantenimiento);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ItemMantenimientoExists(int id) {
        return _context.ItemMantenimiento.Any(e => e.ItemMantenimientoId == id);
    }
}
