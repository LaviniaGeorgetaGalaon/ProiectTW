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
    public class DateUsersController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public DateUsersController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: DateUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.DateUser.ToListAsync());
        }

        // GET: DateUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateUser = await _context.DateUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dateUser == null)
            {
                return NotFound();
            }

            return View(dateUser);
        }

        // GET: DateUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DateUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Nume,Prenume")] DateUser dateUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dateUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dateUser);
        }

        // GET: DateUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateUser = await _context.DateUser.FindAsync(id);
            if (dateUser == null)
            {
                return NotFound();
            }
            return View(dateUser);
        }

        // POST: DateUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Nume,Prenume")] DateUser dateUser)
        {
            if (id != dateUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dateUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DateUserExists(dateUser.Id))
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
            return View(dateUser);
        }

        // GET: DateUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateUser = await _context.DateUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dateUser == null)
            {
                return NotFound();
            }

            return View(dateUser);
        }

        // POST: DateUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dateUser = await _context.DateUser.FindAsync(id);
            _context.DateUser.Remove(dateUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DateUserExists(int id)
        {
            return _context.DateUser.Any(e => e.Id == id);
        }
    }
}
