using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using smart_dorms.Models;
using smart_dorms.Utils;

namespace smart_dorms
{
    public class CamerasController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public CamerasController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: Cameras
        public async Task<IActionResult> Index()
        {
            var uvtdemosdbContext = _context.Camera.Include(c => c.IdCaminNavigation).Include(c => c.IdTipCameraNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }

        // GET: Cameras/Details/5
        public async Task<IActionResult> Status(int etaj = 1, int camin = 1)
        {
            var uvtdemosdbContext = _context.Camera
                .Where(c => c.NrEtaj == etaj && c.IdCamin == camin)
                .Include(c => c.IdCaminNavigation)
                .Include(c => c.IdTipCameraNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camera
                .Include(c => c.IdCaminNavigation)
                .Include(c => c.IdTipCameraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (camera == null)
            {
                return NotFound();
            }
            ViewData[Constants.ObjectListKey] = _context.Obiecte
               .Include(x => x.IdTipObiectNavigation)
               .Where(x => x.IdCamera == camera.Id)
               .ToList();
            int? previousRoomId = _context.Request
                .FirstOrDefault(x => x.IdTipCerere == (int)StudentRequestType.SchimbareCamera
                && x.IdTipStatus == null
                && x.IdUser == Convert.ToInt32(HttpContext.Session.GetString(Constants.UserIdKey)))?.IdCamera;
            ViewData[Constants.PreviousRoomChangeKey] = previousRoomId;
            if (previousRoomId != null)
            {
                Camera previousCamera = _context.Camera.Include(c => c.IdCaminNavigation).FirstOrDefault(x => x.Id == previousRoomId);
                ViewData[Constants.PreviousRoomInfoKey] = previousCamera;
            }

            return View(camera);
        }
        
        public async Task<IActionResult> UseriList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camera
                .Include(c => c.IdCaminNavigation)
                .Include(c => c.IdTipCameraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (camera == null)
            {
                return NotFound();
            }
            ViewData[Constants.UseriListKey] = _context.Useri
               .Include(x => x.IdDateUserNavigation)
               .Include(x => x.IdRolNavigation)
               .Where(x => x.IdCamera == camera.Id)
               .ToList();
            return View(camera);
        }

        // GET: Cameras/Create
        public IActionResult Create()
        {
            ViewData["IdCamin"] = new SelectList(_context.Camin, "Id", "Id");
            ViewData["IdTipCamera"] = new SelectList(_context.TipCamera, "Id", "Id");
            return View();
        }

        // POST: Cameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTipCamera,NrPersoane,NrEtaj,IdCamin")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCamin"] = new SelectList(_context.Camin, "Id", "Id", camera.IdCamin);
            ViewData["IdTipCamera"] = new SelectList(_context.TipCamera, "Id", "Id", camera.IdTipCamera);
            return View(camera);
        }

        // GET: Cameras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            bool isAdmin = bool.Parse(HttpContext.Session.GetString(Constants.IsAdminKey) ?? "false");
            if (!isAdmin) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camera.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            ViewData["IdCamin"] = new SelectList(_context.Camin, "Id", "Id", camera.IdCamin);
            ViewData["IdTipCamera"] = new SelectList(_context.TipCamera, "Id", "Id", camera.IdTipCamera);
            return View(camera);
        }

        // POST: Cameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTipCamera,NrPersoane,NrEtaj,IdCamin")] Camera camera)
        {
            if (id != camera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameraExists(camera.Id))
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
            ViewData["IdCamin"] = new SelectList(_context.Camin, "Id", "Id", camera.IdCamin);
            ViewData["IdTipCamera"] = new SelectList(_context.TipCamera, "Id", "Id", camera.IdTipCamera);
            return View(camera);
        }

        // GET: Cameras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camera
                .Include(c => c.IdCaminNavigation)
                .Include(c => c.IdTipCameraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }
        
        // POST: Cameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _context.Camera.FindAsync(id);
            _context.Camera.Remove(camera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CameraExists(int id)
        {
            return _context.Camera.Any(e => e.Id == id);
        }
    }
}
