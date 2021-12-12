using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs.Models;
using System.IO;
using Azure.Storage.Blobs;
using smart_dorms.Utils;
using smart_dorms.Models;

namespace smart_dorms.Models
{
    public class RequestsController : Controller
    {
        private readonly uvtdemosdbContext _context;

        public RequestsController(uvtdemosdbContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            var uvtdemosdbContext = _context.Request
                .Include(r => r.IdCameraNavigation)
                .Include(r => r.IdTipCerereNavigation)
                .Include(r => r.IdTipStatusNavigation)
                .Include(r => r.IdUserNavigation)
                .ThenInclude(u => u.IdDateUserNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }
        public async Task<IActionResult> IstoricCereri()
        {
            var uvtdemosdbContext = _context.Request
                .Include(r => r.IdCameraNavigation)
                .Include(r => r.IdTipCerereNavigation)
                .Include(r => r.IdTipStatusNavigation)
                .Include(r => r.IdUserNavigation)
                .ThenInclude(u => u.IdDateUserNavigation);
            return View(await uvtdemosdbContext.ToListAsync());
        }
        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .Include(r => r.IdCameraNavigation)
                .Include(r => r.IdTipCerereNavigation)
                .Include(r => r.IdTipStatusNavigation)
                .Include(r => r.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewData["IdTipCerere"] = new SelectList(_context.TipCerere, "Id", "Nume");
            return View();
        }


        public IActionResult ChangeRoom(int roomId)
        {
            _context.Request.Add(new Models.Request
            {
                Detalii = "Schimbare camera",
                IdCamera = roomId,
                IdTipCerere = (int)StudentRequestType.SchimbareCamera,
                IdUser = Convert.ToInt32(HttpContext.Session.GetString(Constants.UserIdKey)),
                Prioritate = 10
            });
            _context.SaveChanges();
            HttpContext.Session.SetString(Constants.PopUpMessageKey, "Felicitari, cererea a fost inregistrata!");
            return RedirectToAction("Index", "Home");
        }


        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipCerere,Prioritate,Detalii,Id")] Request request)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity;
                Useri currentUser = _context.Useri.Include(c => c.IdDateUserNavigation)
                                               .FirstOrDefault(x => x.IdDateUserNavigation.Email == user.Name);
                request.IdUser = currentUser.Id;
                request.IdCamera = currentUser.IdCamera;
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction("IstoricCereri");
            }
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", request.IdCamera);
            ViewData["IdTipCerere"] = new SelectList(_context.TipCerere, "Id", "Id", request.IdTipCerere);
            ViewData["IdTipStatus"] = new SelectList(_context.TipStatus, "Id", "Id", request.IdTipStatus);
            ViewData["IdUser"] = new SelectList(_context.Useri, "Id", "Id", request.IdUser);
            return RedirectToAction("IstoricCereri");
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", request.IdCamera);
            ViewData["IdTipCerere"] = new SelectList(_context.TipCerere, "Id", "Id", request.IdTipCerere);
            ViewData["IdTipStatus"] = new SelectList(_context.TipStatus, "Id", "Id", request.IdTipStatus);
            ViewData["IdUser"] = new SelectList(_context.Useri, "Id", "Id", request.IdUser);
            return View(request);
        }
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Request currentRequest = _context.Request
                .Include(x => x.IdTipCerereNavigation)
                .FirstOrDefault(x => x.Id == id);
            Useri whoRequested = _context.Useri
                .Include(x => x.IdDateUserNavigation)
                .FirstOrDefault(x => x.Id == currentRequest.IdUser);
            switch ((StudentRequestType)currentRequest.IdTipCerere)
            {
                case StudentRequestType.Student:
                    {
                        int roomIdToAddTo = currentRequest.IdCamera.Value;
                        Camera roomToAddTo = _context.Camera.Include(c => c.IdTipCameraNavigation).FirstOrDefault(x => x.Id == roomIdToAddTo);

                        if (roomToAddTo.NrPersoane < roomToAddTo.IdTipCameraNavigation.NrMaxLocuriDisp)
                        {
                            roomToAddTo.NrPersoane++;
                            whoRequested.IdCamera = currentRequest.IdCamera;
                            currentRequest.IdTipStatus = (int)RequestStatus.Completed;
                        }
                        else
                        {
                            currentRequest.IdTipStatus = (int)RequestStatus.Invalid;
                        }

                        break;
                    }
                case StudentRequestType.SchimbareCamera:
                    {
                        int roomIdToAddTo = currentRequest.IdCamera.Value;
                        int roomIdToRemoveFrom = whoRequested.IdCamera.Value;
                        Camera roomToAddTo = _context.Camera.Include(c => c.IdTipCameraNavigation).FirstOrDefault(x => x.Id == roomIdToAddTo);
                        Camera roomToRemoveFrom = _context.Camera.FirstOrDefault(x => x.Id == roomIdToRemoveFrom);

                        if (roomToAddTo.NrPersoane < roomToAddTo.IdTipCameraNavigation.NrMaxLocuriDisp)
                        {
                            roomToAddTo.NrPersoane++;
                            roomToRemoveFrom.NrPersoane--;
                            whoRequested.IdCamera = currentRequest.IdCamera;
                            currentRequest.IdTipStatus = (int)RequestStatus.Completed;
                        }
                        else
                        {
                            currentRequest.IdTipStatus = (int)RequestStatus.Invalid;
                        }

                        break;
                    }
                case StudentRequestType.Dezinsectie:
                    {
                        currentRequest.IdTipStatus = (int)RequestStatus.NeedsTime;

                        break;
                    }
                case StudentRequestType.SchimbareObiect:
                    {
                        //Useri whoRequested = _context.Useri.FirstOrDefault(x => x.Id == currentRequest.IdUser);
                        //int roomIdToRemoveFrom = whoRequested.IdCamera.Value;
                        //ViewData["Obiecte"] = _context.Obiecte.Where(x => x.IdCamera == roomIdToRemoveFrom);
                        currentRequest.IdTipStatus = (int)RequestStatus.Allocated;

                        break;
                    }
                case StudentRequestType.EliminareObiect:
                    {
                        currentRequest.IdTipStatus = (int)RequestStatus.Allocated;

                        break;
                    }
                case StudentRequestType.AdaugareObiect:
                    {
                        currentRequest.IdTipStatus = (int)RequestStatus.Allocated;

                        break;
                    }
            }
            await _context.SaveChangesAsync();
            await SendMail.Execute(currentRequest.IdTipCerereNavigation.Nume,
                whoRequested.IdDateUserNavigation.Email,
                whoRequested.FullName,
                currentRequest.IdTipStatus ?? (int)RequestStatus.Pending);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Deny(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Request currentRequest = _context.Request
               .Include(x => x.IdTipCerereNavigation)
               .FirstOrDefault(x => x.Id == id);
            Useri whoRequested = _context.Useri
                .Include(x => x.IdDateUserNavigation)
                .FirstOrDefault(x => x.Id == currentRequest.IdUser);
            currentRequest.IdTipStatus = (int)RequestStatus.Rejected;
            await SendMail.Execute(currentRequest.IdTipCerereNavigation.Nume,
               whoRequested.IdDateUserNavigation.Email,
               whoRequested.FullName,
               (int)RequestStatus.Rejected);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipCerere,IdCamera,IdTipStatus,Prioritate,Detalii,IdUser,Id")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
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
            ViewData["IdCamera"] = new SelectList(_context.Camera, "Id", "Id", request.IdCamera);
            ViewData["IdTipCerere"] = new SelectList(_context.TipCerere, "Id", "Id", request.IdTipCerere);
            ViewData["IdTipStatus"] = new SelectList(_context.TipStatus, "Id", "Id", request.IdTipStatus);
            ViewData["IdUser"] = new SelectList(_context.Useri, "Id", "Id", request.IdUser);
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .Include(r => r.IdCameraNavigation)
                .Include(r => r.IdTipCerereNavigation)
                .Include(r => r.IdTipStatusNavigation)
                .Include(r => r.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Request.FindAsync(id);
            _context.Request.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("IstoricCereri");
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }


        public async Task<FileResult> DownloadDetails(int id)
        {
            Request requestToDownload = _context.Request.FirstOrDefault(x => x.Id == id);
            string fileName = requestToDownload.Detalii;

            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=uvtdemosteam3;AccountKey=rycYcMytEOF80qJfFf5p91NqTtZHKHe4ZSSQyQUaCwEAAiPnHpuqPsmIseG+BWsTXujeSNuCqqk+1j2J3flDKw==;EndpointSuffix=core.windows.net");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("dosare");

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            return File(download.Content, System.Net.Mime.MediaTypeNames.Application.Zip, fileName);
        }
    }
}
