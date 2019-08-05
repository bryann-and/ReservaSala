using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Models;

namespace ReservaSala.Controllers.API
{
    [Route("api/Reservas")]
    [ApiController]
    public class ReservasAPI : ControllerBase
    {
        [HttpGet]
        public List<Reserva> Get()
        {
            return new List<Reserva>
            {
                new Reserva() { Descricao = "Reunião Tals", DataInicio = "31/05/2019 21:00", DataTermino = "31/05/2019 21:30", Sala = new Sala { Nome = "Sala 01" } },
            };
        }

        [HttpGet("{id:int}")]
        public Reserva Get(int id)
        {
            if (id <= 0)
            {
                return new Reserva();
            }

            return new Reserva
            {
                Id = id,
                Descricao = "Sala 01"
            };
        }

        [HttpPost]
        [Route("api/CadastroSala")]
        public MensagemRetorno Post([FromBody] Reserva dados)
        {
            try
            {
                return new MensagemRetorno
                {
                    Titulo = "Sucesso!",
                    Mensagem = "Sala cadastrada com sucesso!",
                    Tipo = "success"
                };
            }
            catch (Exception)
            {
                return new MensagemRetorno
                {
                    Titulo = "Erro!",
                    Mensagem = "Instabilidade no sistema, tente novamente mais tarde!",
                    Tipo = "error"
                };
            }
        }
    }
}