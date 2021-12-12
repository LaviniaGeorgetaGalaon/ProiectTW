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
    public class TipObiectsController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public TipObiectsController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: TipObiects
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipObiect.ToListAsync());
        }

        // GET: TipObiects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipObiect = await _context.TipObiect
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipObiect == null)
            {
                return NotFound();
            }

            return View(tipObiect);
        }

        // GET: TipObiects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipObiects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Descriere,Cantitate")] TipObiect tipObiect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipObiect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipObiect);
        }

        // GET: TipObiects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipObiect = await _context.TipObiect.FindAsync(id);
            if (tipObiect == null)
            {
                return NotFound();
            }
            return View(tipObiect);
        }

        // POST: TipObiects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere,Cantitate")] TipObiect tipObiect)
        {
            if (id != tipObiect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipObiect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipObiectExists(tipObiect.Id))
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
            return View(tipObiect);
        }

        // GET: TipObiects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipObiect = await _context.TipObiect
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipObiect == null)
            {
                return NotFound();
            }

            return View(tipObiect);
        }

        // POST: TipObiects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipObiect = await _context.TipObiect.FindAsync(id);
            _context.TipObiect.Remove(tipObiect);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipObiectExists(int id)
        {
            return _context.TipObiect.Any(e => e.Id == id);
        }
    }
}
