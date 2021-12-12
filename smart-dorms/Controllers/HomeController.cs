using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_dorms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using smart_dorms.Utils;

namespace smart_dorms.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly uvtdemosdbContext _context;

        public HomeController(ILogger<HomeController> logger, uvtdemosdbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            var user = User.Identity;
            Useri currentUser = _context.Useri.Include(c => c.IdDateUserNavigation)
                                                .FirstOrDefault(x => x.IdDateUserNavigation.Email == user.Name);
            if (currentUser == null)
            {
                _context.DateUser.Add(new DateUser
                {
                    Email = user.Name


                });
                _context.SaveChanges();
                DateUser currentData = _context.DateUser.FirstOrDefault(x => x.Email == user.Name);
                _context.Useri.Add(new Useri
                {
                    IdRol = (int)UserRole.Student,
                    IdDateUser = currentData.Id,
                    IdCamera = null
                });
                _context.SaveChanges();
                currentUser = _context.Useri.FirstOrDefault(x => x.IdDateUser == currentData.Id);
            }
            HttpContext.Session.SetString(Constants.UserKey, JsonConvert.SerializeObject(currentUser));
            HttpContext.Session.SetString(Constants.IsAdminKey, currentUser.IsAdmin.ToString());
            HttpContext.Session.SetString(Constants.RoomKey, currentUser.IdCamera.ToString());
            HttpContext.Session.SetString(Constants.UserIdKey, currentUser.Id.ToString());
            return View();
        }

        public IActionResult Privacy()
        {
            var tryget = HttpContext.Session.GetString("IdDataUser");
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}