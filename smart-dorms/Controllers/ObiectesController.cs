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
    public class ObiectesController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public ObiectesController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: Obiectes
        public async Task<IActionResult> Index()
        {
            var uvtdemosdbContext = _context.Obiecte.Include(o => o.IdCameraNavigation).Include(o => o.IdTipObiectNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }

        // GET: Obiectes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obiecte = await _context.Obiecte
                .Include(o => o.IdCameraNavigation)
                .Include(o => o.IdTipObiectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (obiecte == null)
            {
                return NotFound();
            }

            return View(obiecte);
        }

        // GET: Obiectes/Create
        public IActionResult Create()
        {
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id");
            ViewData["IdTipObiect"] = new SelectList(_context.TipObiect, "Id", "Id");
            return View();
        }

        // POST: Obiectes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipObiect,Cantitate,IdCamera,Id")] Obiecte obiecte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obiecte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", obiecte.IdCamera);
            ViewData["IdTipObiect"] = new SelectList(_context.TipObiect, "Id", "Id", obiecte.IdTipObiect);
            return View(obiecte);
        }

        // GET: Obiectes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obiecte = await _context.Obiecte.FindAsync(id);
            if (obiecte == null)
            {
                return NotFound();
            }
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", obiecte.IdCamera);
            ViewData["IdTipObiect"] = new SelectList(_context.TipObiect, "Id", "Id", obiecte.IdTipObiect);
            return View(obiecte);
        }

        // POST: Obiectes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipObiect,Cantitate,IdCamera,Id")] Obiecte obiecte)
        {
            if (id != obiecte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obiecte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObiecteExists(obiecte.Id))
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
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", obiecte.IdCamera);
            ViewData["IdTipObiect"] = new SelectList(_context.TipObiect, "Id", "Id", obiecte.IdTipObiect);
            return View(obiecte);
        }

        // GET: Obiectes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obiecte = await _context.Obiecte
                .Include(o => o.IdCameraNavigation)
                .Include(o => o.IdTipObiectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (obiecte == null)
            {
                return NotFound();
            }

            return View(obiecte);
        }

        // POST: Obiectes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obiecte = await _context.Obiecte.FindAsync(id);
            _context.Obiecte.Remove(obiecte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObiecteExists(int id)
        {
            return _context.Obiecte.Any(e => e.Id == id);
        }
    }
}
