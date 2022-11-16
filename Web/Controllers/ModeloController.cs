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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SistemaMAV.Web.Controllers;

public class ModeloController : Controller {
    private readonly ApplicationDbContext _context;

    public ModeloController(ApplicationDbContext context) {
        _context = context;
    }

    // GET: Modelo
    public async Task<IActionResult> Index() {
        List<Modelo> modelos = await _context.Modelo.ToListAsync();
        List<ModeloViewModel> modelosVM = new List<ModeloViewModel>();
        foreach(Modelo unModelo in modelos) {
            unModelo.Marca = await _context.Marca.FirstOrDefaultAsync<Marca>(m => m.MarcaId == unModelo.MarcaId)??new Marca();
            unModelo.TipoUnidad = await _context.TipoUnidad.FirstOrDefaultAsync(t => t.TipoUnidadId == unModelo.TipoUnidadId)??new TipoUnidad();
            modelosVM.Add(new ModeloViewModel(unModelo));
        }
        return View(modelosVM);
    }

    // GET: Modelo/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null) {
            return NotFound();
        }

        var modelo = await _context.Modelo
            .Include(m => m.Marca)
            .Include(m => m.TipoUnidad)
            .FirstOrDefaultAsync(m => m.ModeloId == id);
        if (modelo == null) {
            return NotFound();
        }

        return View(new ModeloViewModel(modelo));
    }

    // GET: Modelo/Create
    public IActionResult Create() {
        ViewData["MarcaId"] = new SelectList(_context.Marca, "MarcaId", "Detalle");
        ViewData["TipoUnidadId"] = new SelectList(_context.TipoUnidad, "TipoUnidadId", "Detalle");
        return View();
    }

    // POST: Modelo/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ModeloId,Detalle,MarcaId,TipoUnidadId,FechaAlta,FechaBaja")] ModeloViewModel modeloVM) {
        if (ModelState.IsValid) {
            _context.Add(modeloVM.ToModelo());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MarcaId"] = new SelectList(_context.Marca, "MarcaId", "Detalle", modeloVM.MarcaId);
        ViewData["TipoUnidadId"] = new SelectList(_context.TipoUnidad, "TipoUnidadId", "Detalle", modeloVM.TipoUnidadId);
        return View(modeloVM);
    }

    // GET: Modelo/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null) {
            return NotFound();
        }

        var modelo = await _context.Modelo.FindAsync(id);
        if (modelo == null) {
            return NotFound();
        }
        ViewData["MarcaId"] = new SelectList(_context.Marca, "MarcaId", "Detalle", modelo.MarcaId);
        ViewData["TipoUnidadId"] = new SelectList(_context.TipoUnidad, "TipoUnidadId", "Detalle", modelo.TipoUnidadId);
        return View(new ModeloViewModel(modelo));
    }

    // POST: Modelo/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ModeloId,Detalle,MarcaId,TipoUnidadId,FechaAlta,FechaBaja")] ModeloViewModel modeloVM) {
        if (id != modeloVM.ModeloId) {
            return NotFound();
        }

        if (ModelState.IsValid) {
            try {
                _context.Update(modeloVM.ToModelo());
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ModeloExists(modeloVM.ModeloId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["MarcaId"] = new SelectList(_context.Marca, "MarcaId", "Detalle", modeloVM.MarcaId);
        ViewData["TipoUnidadId"] = new SelectList(_context.TipoUnidad, "TipoUnidadId", "Detalle", modeloVM.TipoUnidadId);
        return View(modeloVM);
    }

    // GET: Modelo/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null) {
            return NotFound();
        }

        var modelo = await _context.Modelo
            .Include(m => m.Marca)
            .Include(m => m.TipoUnidad)
            .FirstOrDefaultAsync(m => m.ModeloId == id);
        if (modelo == null) {
            return NotFound();
        }

        return View(new ModeloViewModel(modelo));
    }

    // POST: Modelo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var modelo = await _context.Modelo.FindAsync(id);
        if (modelo == null)
            return NotFound();
        _context.Modelo.Remove(modelo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ModeloExists(int id) {
        return _context.Modelo.Any(e => e.ModeloId == id);
    }
}
