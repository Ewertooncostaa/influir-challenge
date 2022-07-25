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
    public class ProdutorsController : Controller
    {
        private readonly Contexto _context;

        public ProdutorsController(Contexto context)
        {
            _context = context;
        }

        // GET: Produtors
        public async Task<IActionResult> Index()
        {
            var listProdutor = (await _context.Produtor.ToListAsync()).Select(produtor =>
            {
                produtor.CPF = produtor.CPF.Insert(3,".").Insert(7,".").Insert(11,"-");
                return produtor;
            });

              return _context.Produtor != null ? 
                          View(listProdutor) :
                          Problem("Entity set 'Contexto.PRODUTOR'  is null.");
        }

        // GET: Produtors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtor == null)
            {
                return NotFound();
            }

            var produtor = await _context.Produtor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtor == null)
            {
                return NotFound();
            }

            return View(produtor);
        }

        // GET: Produtors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CPF")] Produtor newProdutor)
        {
            if (!ModelState.IsValid)
            {
                return View(newProdutor);
            }
            newProdutor.CPF = newProdutor.CPF.Replace("-", "").Replace(".", "");
            if (_context.Produtor.Where(produtor => produtor.CPF == newProdutor.CPF).Any()) {
                TempData["error"] = "CPF já cadastrado";               
                return View(newProdutor);
            }

            _context.Add(newProdutor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Produtors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtor == null)
            {
                return NotFound();
            }

            var produtor = await _context.Produtor.FindAsync(id);
            if (produtor == null)
            {
                return NotFound();
            }
            produtor.CPF = produtor.CPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            return View(produtor);
        }

        // POST: Produtors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF")] Produtor produtor)
        {
            if (id != produtor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    produtor.CPF = produtor.CPF.Replace("-", "").Replace(".", "");
                    _context.Update(produtor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutorExists(produtor.Id))
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
            return View(produtor);
        }

        // GET: Produtors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtor == null)
            {
                return NotFound();
            }

            var produtor = await _context.Produtor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtor == null)
            {
                return NotFound();
            }

            return View(produtor);
        }

        // POST: Produtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtor == null)
            {
                return Problem("Entity set 'Contexto.PRODUTOR'  is null.");
            }
            var produtor = await _context.Produtor.FindAsync(id);
            if (produtor != null)
            {
                _context.Produtor.Remove(produtor);
            }
            if (_context.Lote.Where(lote => lote.ProdutorId == produtor.Id).Any())
            {
                TempData["error"] = "Não é possível excluir, Produtor já em uso!";
                return View(produtor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutorExists(int id)
        {
          return (_context.Produtor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
