using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_dorms.Models;
using smart_dorms.Utils;

namespace smart_dorms
{
    public class PermisiunisController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public PermisiunisController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: Permisiunis
        public async Task<IActionResult> Index()
        {
            var uvtdemosdbContext = _context.Permisiuni.Include(p => p.IdRolNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }

        // GET: Permisiunis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisiuni = await _context.Permisiuni
                .Include(p => p.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permisiuni == null)
            {
                return NotFound();
            }

            return View(permisiuni);
        }

        // GET: Permisiunis/Create
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id");
            return View();
        }

        // POST: Permisiunis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRol,NumePagina,Id")] Permisiuni permisiuni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permisiuni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id", permisiuni.IdRol);
            return View(permisiuni);
        }

        // GET: Permisiunis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisiuni = await _context.Permisiuni.FindAsync(id);
            if (permisiuni == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id", permisiuni.IdRol);
            return View(permisiuni);
        }

        // POST: Permisiunis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRol,NumePagina,Id")] Permisiuni permisiuni)
        {
            if (id != permisiuni.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permisiuni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermisiuniExists(permisiuni.Id))
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
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id", permisiuni.IdRol);
            return View(permisiuni);
        }

        // GET: Permisiunis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisiuni = await _context.Permisiuni
                .Include(p => p.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permisiuni == null)
            {
                return NotFound();
            }

            return View(permisiuni);
        }

        // POST: Permisiunis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permisiuni = await _context.Permisiuni.FindAsync(id);
            _context.Permisiuni.Remove(permisiuni);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermisiuniExists(int id)
        {
            return _context.Permisiuni.Any(e => e.Id == id);
        }
    }
}
