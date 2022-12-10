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

public class MarcaController : Controller {
    private readonly ApplicationDbContext _context;

    public MarcaController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: Marca
    public async Task<IActionResult> Index() {
        List<Marca> marcas = await _context.Marca.ToListAsync();
        List<MarcaViewModel> marcasVM = new List<MarcaViewModel>();
        foreach(Marca unaMarca in marcas) {
            marcasVM.Add(new MarcaViewModel(unaMarca));
        }
        return View(marcasVM);
    }

    // GET: Marca/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null) {
            return NotFound();
        }

        var marca = await _context.Marca
            .FirstOrDefaultAsync(m => m.MarcaId == id);
        if (marca == null) {
            return NotFound();
        }

        return View(new MarcaViewModel(marca));
    }

    // GET: Marca/Create
    public IActionResult Create() {
        return View();
    }

    // POST: Marca/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MarcaId,Detalle,Activo")] MarcaViewModel marcaVM) {
        if (ModelState.IsValid) {
            _context.Add(marcaVM.ToMarca());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(marcaVM);
    }

    // GET: Marca/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null) {
            return NotFound();
        }

        var marca = await _context.Marca.FindAsync(id);
        if (marca == null) {
            return NotFound();
        }
        return View(new MarcaViewModel(marca));
    }

    // POST: Marca/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MarcaId,Detalle,Activo")] MarcaViewModel marcaVM) {
        if (id != marcaVM.MarcaId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(marcaVM.ToMarca());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!MarcaExists(marcaVM.MarcaId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(marcaVM);
    }

    // GET: Marca/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null) {
            return NotFound();
        }

        var marca = await _context.Marca
            .FirstOrDefaultAsync(m => m.MarcaId == id);
        if (marca == null) {
            return NotFound();
        }

        return View(new MarcaViewModel(marca));
    }

    // POST: Marca/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var marca = await _context.Marca.FindAsync(id);
        if (marca == null) {
            return NotFound();
        }
        _context.Marca.Remove(marca);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MarcaExists(int id) {
        return _context.Marca.Any(e => e.MarcaId == id);
    }
}
