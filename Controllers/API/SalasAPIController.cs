using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Models;

namespace ReservaSala.Controllers.API
{
    [Route("api/Salas")]
    [ApiController]
    public class SalaAPI : ControllerBase
    {
        [HttpGet]
        [Route("Lista")]
        public List<Sala> Get()
        {
            return new List<Sala>
            {
                new Sala() { Nome = "Sala 01" },
                new Sala() { Nome = "Sala 02" }
            };
        }

        [HttpGet]
        [Route("Lista/{id:int}")]
        public Sala Get(int id)
        {
            if (id <= 0)
            {
                return new Sala();
            }

            return new Sala
            {
                Id = id,
                Nome = "Sala 01"
            };
        }

        [HttpPost]
        [Route("api/CadastroSala")]
        public MensagemRetorno Post([FromBody] Sala dados)
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
                    Mensagem = "Instabilidade no sistem, tente novamente mais tarde!",
                    Tipo = "error"
                };
            }
        }
    }
}
