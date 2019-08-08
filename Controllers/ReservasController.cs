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


        public ActionResult Create()
        {
            return View("CadastroReserva");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserva collection)
        {
            try
            {
                // remove a verifição do nome da sala, que não é usado aqui
                ModelState.Remove("Sala.Nome");

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