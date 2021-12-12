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
    public class TipDocumentesController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public TipDocumentesController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: TipDocumentes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipDocumente.ToListAsync());
        }

        // GET: TipDocumentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipDocumente = await _context.TipDocumente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipDocumente == null)
            {
                return NotFound();
            }

            return View(tipDocumente);
        }

        // GET: TipDocumentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipDocumentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Descriere")] TipDocumente tipDocumente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipDocumente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipDocumente);
        }

        // GET: TipDocumentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipDocumente = await _context.TipDocumente.FindAsync(id);
            if (tipDocumente == null)
            {
                return NotFound();
            }
            return View(tipDocumente);
        }

        // POST: TipDocumentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere")] TipDocumente tipDocumente)
        {
            if (id != tipDocumente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipDocumente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipDocumenteExists(tipDocumente.Id))
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
            return View(tipDocumente);
        }

        // GET: TipDocumentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipDocumente = await _context.TipDocumente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipDocumente == null)
            {
                return NotFound();
            }

            return View(tipDocumente);
        }

        // POST: TipDocumentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipDocumente = await _context.TipDocumente.FindAsync(id);
            _context.TipDocumente.Remove(tipDocumente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipDocumenteExists(int id)
        {
            return _context.TipDocumente.Any(e => e.Id == id);
        }
    }
}
