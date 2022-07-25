using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INFLUIR.Models;

namespace INFLUIR.Controllers
{
    public class PERIMETROController : Controller
    {
        private readonly Contexto _context;

        public PERIMETROController(Contexto context)
        {
            _context = context;
        }

        // GET: Perimetro
        public async Task<IActionResult> Index()
        {                                   
            return View(await GetPerimetros());
        }
        private async Task<List<Perimetro>> GetPerimetros() {
            return new List<Perimetro>(await _context.Perimetro.ToListAsync());
        }

        // GET: Perimetro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Perimetro == null)
            {
                return NotFound();
            }

            var Perimetro = await _context.Perimetro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Perimetro == null)
            {
                return NotFound();
            }

            return View(Perimetro);
        }

        // GET: Perimetro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perimetro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Perimetro Perimetro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Perimetro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Perimetro);
        }

        // GET: Perimetro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Perimetro == null)
            {
                return NotFound();
            }

            var Perimetro = await _context.Perimetro.FindAsync(id);
            if (Perimetro == null)
            {
                return NotFound();
            }
            return View(Perimetro);
        }

        // POST: Perimetro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Perimetro Perimetro)
        {
            if (id != Perimetro.Id)
            {
                return base.NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Perimetro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerimetroExists(Perimetro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Perimetro);
        }

        // GET: Perimetro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Perimetro == null)
            {
                return NotFound();
            }

            var Perimetro = await _context.Perimetro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Perimetro == null)
            {
                return NotFound();
            }

            return View(Perimetro);
        }

        // POST: Perimetro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Perimetro == null)
            {
                return Problem("Entity set 'Contexto.Perimetro'  is null.");
            }
            var Perimetro = await _context.Perimetro.FindAsync(id);
            if (Perimetro != null)
            {
                _context.Perimetro.Remove(Perimetro);
            }
            
            if (_context.Lote.Where(lote => lote.PerimetroId == Perimetro.Id).Any())
            {
                TempData["error"] = "Não é possível excluir, perimetro já em uso!";                
                return View(Perimetro);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerimetroExists(int id)
        {
          return (_context.Perimetro?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
