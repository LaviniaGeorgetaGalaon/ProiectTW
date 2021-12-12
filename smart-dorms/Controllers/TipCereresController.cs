using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_dorms.Models;
using smart_dorms.Utils;

namespace UVT.Dorms
{
    public class TipCereresController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public TipCereresController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: TipCereres
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipCerere.ToListAsync());
        }

        // GET: TipCereres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCerere = await _context.TipCerere
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipCerere == null)
            {
                return NotFound();
            }

            return View(tipCerere);
        }

        // GET: TipCereres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipCereres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Descriere")] TipCerere tipCerere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipCerere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipCerere);
        }

        // GET: TipCereres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCerere = await _context.TipCerere.FindAsync(id);
            if (tipCerere == null)
            {
                return NotFound();
            }
            return View(tipCerere);
        }

        // POST: TipCereres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere")] TipCerere tipCerere)
        {
            if (id != tipCerere.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipCerere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipCerereExists(tipCerere.Id))
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
            return View(tipCerere);
        }

        // GET: TipCereres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCerere = await _context.TipCerere
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipCerere == null)
            {
                return NotFound();
            }

            return View(tipCerere);
        }

        // POST: TipCereres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipCerere = await _context.TipCerere.FindAsync(id);
            _context.TipCerere.Remove(tipCerere);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipCerereExists(int id)
        {
            return _context.TipCerere.Any(e => e.Id == id);
        }
    }
}
