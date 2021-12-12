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
    public class TipCamerasController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public TipCamerasController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: TipCameras
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipCamera.ToListAsync());
        }

        // GET: TipCameras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCamera = await _context.TipCamera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipCamera == null)
            {
                return NotFound();
            }

            return View(tipCamera);
        }

        // GET: TipCameras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipCameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Descriere,NrMaxLocuriDisp")] TipCamera tipCamera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipCamera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipCamera);
        }

        // GET: TipCameras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCamera = await _context.TipCamera.FindAsync(id);
            if (tipCamera == null)
            {
                return NotFound();
            }
            return View(tipCamera);
        }

        // POST: TipCameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere,NrMaxLocuriDisp")] TipCamera tipCamera)
        {
            if (id != tipCamera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipCamera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipCameraExists(tipCamera.Id))
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
            return View(tipCamera);
        }

        // GET: TipCameras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipCamera = await _context.TipCamera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipCamera == null)
            {
                return NotFound();
            }

            return View(tipCamera);
        }

        // POST: TipCameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipCamera = await _context.TipCamera.FindAsync(id);
            _context.TipCamera.Remove(tipCamera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipCameraExists(int id)
        {
            return _context.TipCamera.Any(e => e.Id == id);
        }
    }
}
