using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Controllers.API;

namespace ReservaSala.Controllers
{
    public class ReservasController : Controller
    {
        public ActionResult Index()
        {
            ReservasAPIController reservas = new ReservasAPIController();

            return View("ListaReservas", reservas.Get());
        }


        // GET: Reservas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
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
    }
}