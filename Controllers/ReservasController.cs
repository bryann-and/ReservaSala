using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Controllers.API;
using ReservaSala.Models;

namespace ReservaSala.Controllers
{
    public class ReservasController : Controller
    {
        public ActionResult Index()
        {
            ReservasAPI reservas = new ReservasAPI();

            return View("ListaReservas", reservas.Get());
        }


        // GET: Reservas/Create
        public ActionResult Create()
        {
            return View("CadastroReserva");
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserva collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("CadastroReserva");
                }

                ReservasAPI API = new ReservasAPI();
                MensagemRetorno retorno = API.Post(collection);
                TempData["Mensagem"] = retorno.ToString();

                if (retorno.Tipo == "success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("CadastroReserva");
                }
            }
            catch
            {
                TempData["Mensagem"] = new MensagemRetorno
                {
                    Titulo = "Erro!",
                    Mensagem = "Instabilidade no sistema, tente novamente mais tarde!",
                    Tipo = "error"
                }.ToString();

                return View("CadastroReserva");
            }
        }
    }
}