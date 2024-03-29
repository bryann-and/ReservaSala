﻿using Microsoft.AspNetCore.Mvc;
using ReservaSala.Controllers.API;
using ReservaSala.Models;

namespace ReservaSala.Controllers
{
    public class SalasController : Controller
    {
        public ActionResult Index()
        {
            SalaAPI lsita = new SalaAPI();

            return View("ListaSalas", lsita.Get());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("CadastroSala");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sala collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("CadastroSala");
                }

                SalaAPI API = new SalaAPI();
                MensagemRetorno retorno = API.Post(collection);
                TempData["Mensagem"] = retorno.ToString();

                if (retorno.Tipo == "success")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("CadastroSala");
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

                return View("CadastroSala");
            }
        }
    }
}