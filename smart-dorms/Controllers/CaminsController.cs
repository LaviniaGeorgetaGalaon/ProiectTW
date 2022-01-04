using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using smart_dorms.Models;
using smart_dorms.Utils;

namespace smart_dorms
{
    public class CaminsController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public CaminsController(uvtdemosdbContext context)
        {
            _context = context;
        }
        //List<Camera> camera = new List<Camera>();
        //List<Camin> camin = new List<Camin>();

        // GET: Camins
        public async Task<IActionResult> Index()
        {

            var ap = (from p in _context.Camera
                      where p.IdTipCamera >= 5
                      select new
                      {
                          caminId = p.IdCamin,
                          tipCamera = p.IdTipCameraNavigation.Nume

                      }).ToList();
            ViewBag.Message = ap;

            return View(await _context.Camin.ToListAsync());
        }

        // GET: Camins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var camin = await _context.Camin
                 .FirstOrDefaultAsync(m => m.Id == id);
            ViewData[Constants.FacilitatiListKey] = _context.Camera
               .Include(x => x.IdTipCameraNavigation)
               .Where(x => x.IdCamin == id)
               .ToList();

            if (camin == null)
            {
                return NotFound();
            }

            return View(camin);
        }

        // GET: Camins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Camins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameCamin,LocatieCamin")] Camin camin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(camin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(camin);
        }

        // GET: Camins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camin = await _context.Camin.FindAsync(id);
            if (camin == null)
            {
                return NotFound();
            }
            return View(camin);
        }

        // POST: Camins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameCamin,LocatieCamin")] Camin camin)
        {
            if (id != camin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaminExists(camin.Id))
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
            return View(camin);
        }

        // GET: Camins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camin = await _context.Camin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (camin == null)
            {
                return NotFound();
            }

            return View(camin);
        }

        // POST: Camins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camin = await _context.Camin.FindAsync(id);
            _context.Camin.Remove(camin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaminExists(int id)
        {
            return _context.Camin.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Map(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var camin = await _context.Camin
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (camin == null)
            {
                return NotFound();
            }

            return View(camin);
        }

       
        public async Task<IActionResult> StatusCamine(int camin = 1)
        {
            var uvtdemosdbContext = _context.Camera
                .Where(c=> c.IdCamin == camin)
                .Include(c => c.IdCaminNavigation)
                .Include(c => c.IdTipCameraNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }
    }
}
