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
    public class UserisController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public UserisController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: Useris
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Useri user = new Useri();
                user.IdDateUserNavigation.Nume = User.Identity.Name;
            }
            var uvtdemosdbContext = _context.Useri.Include(u => u.IdCameraNavigation).Include(u => u.IdDateUserNavigation).Include(u => u.IdRolNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }

        // GET: Useris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useri = await _context.Useri
                .Include(u => u.IdCameraNavigation)
                .Include(u => u.IdDateUserNavigation)
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useri == null)
            {
                return NotFound();
            }

            return View(useri);
        }

        // GET: Useris/Create
        public IActionResult Create()
        {
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id");
            ViewData["IdDateUser"] = new SelectList(_context.DateUser, "Id", "Id");
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id");
            return View();
        }

        // POST: Useris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdRol,Gdpr,IdCamera,IdDateUser")] Useri useri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(useri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", useri.IdCamera);
            ViewData["IdDateUser"] = new SelectList(_context.DateUser, "Id", "Id", useri.IdDateUser);
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id", useri.IdRol);
            return View(useri);
        }

        // GET: Useris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useri = await _context.Useri.FindAsync(id);
            if (useri == null)
            {
                return NotFound();
            }
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", useri.IdCamera);
            ViewData["IdDateUser"] = new SelectList(_context.DateUser, "Id", "Id", useri.IdDateUser);
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id", useri.IdRol);
            return View(useri);
        }

        // POST: Useris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdRol,Gdpr,IdCamera,IdDateUser")] Useri useri)
        {
            if (id != useri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(useri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseriExists(useri.Id))
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
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", useri.IdCamera);
            ViewData["IdDateUser"] = new SelectList(_context.DateUser, "Id", "Id", useri.IdDateUser);
            ViewData["IdRol"] = new SelectList(_context.Roluri, "Id", "Id", useri.IdRol);
            return View(useri);
        }

        // GET: Useris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useri = await _context.Useri
                .Include(u => u.IdCameraNavigation)
                .Include(u => u.IdDateUserNavigation)
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useri == null)
            {
                return NotFound();
            }

            return View(useri);
        }

        // POST: Useris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var useri = await _context.Useri.FindAsync(id);
            _context.Useri.Remove(useri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseriExists(int id)
        {
            return _context.Useri.Any(e => e.Id == id);
        }
    }
}

