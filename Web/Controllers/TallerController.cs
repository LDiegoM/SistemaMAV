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

public class TallerController : Controller {
    private readonly ApplicationDbContext _context;

    public TallerController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: Taller
    public async Task<IActionResult> Index() {
        List<TallerViewModel> talleresVM = new List<TallerViewModel>();
        if (_context.Taller == null)
            return View(talleresVM);

        List<Taller> talleres = await _context.Taller.ToListAsync();
        foreach(Taller unTaller in talleres) {
            unTaller.Localidad = new Localidad();
            if (_context.Localidad != null)
                unTaller.Localidad = await _context.Localidad.FirstOrDefaultAsync<Localidad>(l => l.LocalidadId == unTaller.LocalidadId)??unTaller.Localidad;
            talleresVM.Add(new TallerViewModel(unTaller));
        }
        return View(talleresVM);
    }

    // GET: Taller/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null || _context.Taller == null) {
            return NotFound();
        }

        var taller = await _context.Taller
            .Include(p => p.Localidad)
            .FirstOrDefaultAsync(m => m.TallerId == id);
        if (taller == null) {
            return NotFound();
        }

        return View(new TallerViewModel(taller));
    }

    // GET: Taller/Create
    public IActionResult Create() {
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View();
    }

    // POST: Taller/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TallerId,Nombre,AnioApertura,Direccion,LocalidadId,Telefono,Email,Activo")] TallerViewModel tallerVM) {
        if (ModelState.IsValid) {
            tallerVM.VotosPositivos = 0;
            tallerVM.VotosNegativos = 0;
            _context.Add(tallerVM.ToTaller());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View(tallerVM);
    }

    // GET: Taller/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Taller == null) {
            return NotFound();
        }

        var taller = await _context.Taller.FindAsync(id);
        if (taller == null) {
            return NotFound();
        }
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View(new TallerViewModel(taller));
    }

    // POST: Taller/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TallerId,Nombre,AnioApertura,Direccion,LocalidadId,Telefono,Email,Activo,VotosPositivos,VotosNegativos")] TallerViewModel tallerVM) {
        if (id != tallerVM.TallerId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(tallerVM.ToTaller());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!TallerExists(tallerVM.TallerId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["LocalidadId"] = new SelectList(_context.Localidad, "LocalidadId", "Nombre");
        return View(tallerVM);
    }

    // GET: Taller/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.Taller == null) {
            return NotFound();
        }

        var taller = await _context.Taller
            .Include(p => p.Localidad)
            .FirstOrDefaultAsync(m => m.TallerId == id);
        if (taller == null) {
            return NotFound();
        }

        return View(new TallerViewModel(taller));
    }

    // POST: Taller/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (_context.Taller == null)
            return NotFound();
        var taller = await _context.Taller.FindAsync(id);
        if (taller == null) {
            return NotFound();
        }
        _context.Taller.Remove(taller);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TallerExists(int id) {
        if (_context.Taller == null)
            return false;
        return _context.Taller.Any(e => e.TallerId == id);
    }
}
