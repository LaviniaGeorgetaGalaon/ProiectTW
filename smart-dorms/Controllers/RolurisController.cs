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
    public class RolurisController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public RolurisController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: Roluris
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roluri.ToListAsync());
        }

        // GET: Roluris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roluri = await _context.Roluri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roluri == null)
            {
                return NotFound();
            }

            return View(roluri);
        }

        // GET: Roluris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roluris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Descriere")] Roluri roluri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roluri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roluri);
        }

        // GET: Roluris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roluri = await _context.Roluri.FindAsync(id);
            if (roluri == null)
            {
                return NotFound();
            }
            return View(roluri);
        }

        // POST: Roluris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere")] Roluri roluri)
        {
            if (id != roluri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roluri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoluriExists(roluri.Id))
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
            return View(roluri);
        }

        // GET: Roluris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roluri = await _context.Roluri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roluri == null)
            {
                return NotFound();
            }

            return View(roluri);
        }

        // POST: Roluris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roluri = await _context.Roluri.FindAsync(id);
            _context.Roluri.Remove(roluri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoluriExists(int id)
        {
            return _context.Roluri.Any(e => e.Id == id);
        }
    }
}
