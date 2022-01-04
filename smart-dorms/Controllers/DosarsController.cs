using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_dorms.Models;
using smart_dorms.Utils;

namespace smart_dorms
{
    public class DosarsController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public DosarsController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: Dosars
        public async Task<IActionResult> Index()
        {
            var uvtdemosdbContext = _context.Dosar.Include(d => d.IdTipDocumenteNavigation).Include(d => d.IdUserNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }

        public IActionResult DepunereDosar(int roomId)
        {
            HttpContext.Session.SetString(Constants.RoomKey, roomId.ToString());
            return View(new UploadFile());
        }
        [HttpPost]
        public async Task<IActionResult> DepunereDosar(UploadFile model)
        {
            var img = model.MyImage;
            var imgCaption = model.ImageCaption;

            //Getting file meta data
            var fileName = HttpContext.Session.GetString(Constants.UserIdKey).Split('.').First() + ".zip";
            if (string.IsNullOrEmpty(model.MyImage.ContentType))
            {
                return NotFound();
            }
            else
            {
                var contentType = model.MyImage.ContentType;
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=dosare;AccountKey=nAFcDVcSP2BnMF4WqM1y3Evxx93TN3Jsv+WSbzIjTgpWUFpu+/q6L5qhw/wpyBBo+sEwR0h03wvseHaL7x9Xpw==;EndpointSuffix=core.windows.net");
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("dosare");

                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

                await blobClient.UploadAsync(img.OpenReadStream(), new BlobHttpHeaders { ContentType = contentType });

                _context.Request.Add(new Models.Request
                {
                    Detalii = fileName,
                    IdCamera = Convert.ToInt32(HttpContext.Session.GetString(Constants.RoomKey)),
                    IdTipCerere = (int)StudentRequestType.Student,
                    IdUser = Convert.ToInt32(HttpContext.Session.GetString(Constants.UserIdKey)),
                    Prioritate = 10
                });
                _context.SaveChanges();
                HttpContext.Session.SetString(Constants.PopUpMessageKey, "Felicitari, cererea a fost inregistrata!");
                return RedirectToAction("Index", "Home");
            }


        }

        // GET: Dosars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dosar = await _context.Dosar
                .Include(d => d.IdTipDocumenteNavigation)
                .Include(d => d.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dosar == null)
            {
                return NotFound();
            }

            return View(dosar);
        }

        // GET: Dosars/Create
        public IActionResult Create()
        {
            ViewData["IdTipDocumente"] = new SelectList(_context.TipDocumente, "Id", "Id");
            ViewData["IdUser"] = new SelectList(_context.Useri, "Id", "Id");
            return View();
        }

        // POST: Dosars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTipDocumente,IdUser")] Dosar dosar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dosar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipDocumente"] = new SelectList(_context.TipDocumente, "Id", "Id", dosar.IdTipDocumente);
            ViewData["IdUser"] = new SelectList(_context.Useri, "Id", "Id", dosar.IdUser);
            return View(dosar);
        }

        // GET: Dosars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dosar = await _context.Dosar.FindAsync(id);
            if (dosar == null)
            {
                return NotFound();
            }
            ViewData["IdTipDocumente"] = new SelectList(_context.TipDocumente, "Id", "Id", dosar.IdTipDocumente);
            ViewData["IdUser"] = new SelectList(_context.Useri, "Id", "Id", dosar.IdUser);
            return View(dosar);
        }

        // POST: Dosars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTipDocumente,IdUser")] Dosar dosar)
        {
            if (id != dosar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dosar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DosarExists(dosar.Id))
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
            ViewData["IdTipDocumente"] = new SelectList(_context.TipDocumente, "Id", "Id", dosar.IdTipDocumente);
            ViewData["IdUser"] = new SelectList(_context.Useri, "Id", "Id", dosar.IdUser);
            return View(dosar);
        }

        // GET: Dosars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dosar = await _context.Dosar
                .Include(d => d.IdTipDocumenteNavigation)
                .Include(d => d.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dosar == null)
            {
                return NotFound();
            }

            return View(dosar);
        }

        // POST: Dosars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dosar = await _context.Dosar.FindAsync(id);
            _context.Dosar.Remove(dosar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DosarExists(int id)
        {
            return _context.Dosar.Any(e => e.Id == id);
        }
    }
}
