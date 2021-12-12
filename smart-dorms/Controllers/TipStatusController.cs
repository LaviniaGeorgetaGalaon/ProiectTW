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
    public class TipStatusController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public TipStatusController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: TipStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipStatus.ToListAsync());
        }

        // GET: TipStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipStatus = await _context.TipStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipStatus == null)
            {
                return NotFound();
            }

            return View(tipStatus);
        }

        // GET: TipStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Descriere")] TipStatus tipStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipStatus);
        }

        // GET: TipStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipStatus = await _context.TipStatus.FindAsync(id);
            if (tipStatus == null)
            {
                return NotFound();
            }
            return View(tipStatus);
        }

        // POST: TipStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere")] TipStatus tipStatus)
        {
            if (id != tipStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipStatusExists(tipStatus.Id))
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
            return View(tipStatus);
        }

        // GET: TipStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipStatus = await _context.TipStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipStatus == null)
            {
                return NotFound();
            }

            return View(tipStatus);
        }

        // POST: TipStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipStatus = await _context.TipStatus.FindAsync(id);
            _context.TipStatus.Remove(tipStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipStatusExists(int id)
        {
            return _context.TipStatus.Any(e => e.Id == id);
        }
    }
}
