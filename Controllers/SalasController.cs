using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Controllers.API;
using ReservaSala.Models;

namespace ReservaSala.Controllers
{
    public class SalasController : Controller
    {
        // GET: Salas
        public ActionResult Index()
        {
            ListaSalaAPI lsita = new ListaSalaAPI();

            return View("ListaSalas", lsita.Get());
        }

        // GET: Salas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Salas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Salas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}