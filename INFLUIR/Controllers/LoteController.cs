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
    public class LoteController : Controller
    {
        private readonly Contexto _context;

        public LoteController(Contexto context)
        {
            _context = context;
        }

        // GET: Lote
        public async Task<IActionResult> Index()
        {
              return _context.Lote != null ? 
                          View(await _context.Lote.Include(x => x.Perimetro).Include(x => x.Produtor).ToListAsync()) :
                          Problem("Entity set 'Contexto.LOTE'  is null.");
        }

        // GET: Lote/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lote == null)
            {
                return NotFound();
            }

            var lote = await _context.Lote.Include(x => x.Perimetro).Include(x => x.Produtor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // GET: Lote/Create
        public async Task<IActionResult> Create()
        {            
            await FillBags();
            return View();
        }
        private async Task<bool> FillBags()
        {
            ViewBag.Perimetros = await _context.Perimetro.ToListAsync();
            ViewBag.Produtores = await _context.Produtor.ToListAsync();
            return true;
        }

        // POST: Lote/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,PerimetroId,ProdutorId")] Lote newLote)
        {
            if (!ModelState.IsValid)
            {
                await FillBags();
                return View(newLote);
            }
            if (_context.Lote.Where(lote => lote.Codigo == newLote.Codigo).Any())
            {
                TempData["error"] = "Código já cadastrado";
                await FillBags();                
                return View(newLote);
            }
            
            _context.Add(newLote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Lote/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lote == null)
            {
                return NotFound();
            }

            var lote = await _context.Lote.FindAsync(id);
            if (lote == null)
            {
                return NotFound();
            }
            await FillBags();
            return View(lote);
        }

        // POST: Lote/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,PerimetroId,ProdutorId")] Lote lote)
        {
            if (id != lote.Id)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                return View(lote);
            }
            if (_context.Lote.Where(loteCur => lote.Codigo == loteCur.Codigo).Any())
            {
                TempData["error"] = "Código já cadastrado";
                await FillBags();
                return View(lote);
            }
            try
            {
                _context.Update(lote);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoteExists(lote.Id))
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

        // GET: Lote/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lote == null)
            {
                return NotFound();
            }

            var lote = await _context.Lote.Include(x => x.Perimetro).Include(x => x.Produtor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // POST: Lote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lote == null)
            {
                return Problem("Entity set 'Contexto.LOTE'  is null.");
            }
            var lote = await _context.Lote.FindAsync(id);
            if (lote != null)
            {
                _context.Lote.Remove(lote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoteExists(int id)
        {
          return (_context.Lote?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
