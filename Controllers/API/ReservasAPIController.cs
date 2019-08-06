using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Bibliotecas;
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
        [Route("CadastroReserva")]
        public MensagemRetorno Post([FromBody] Reserva dados)
        {
            try
            {
                dados.Validar();                
                if (dados.Sala.Id < 1)
                {
                    return new MensagemRetorno
                    {
                        Titulo = "Erro!",
                        Mensagem = "É preciso o ID da sala",
                        Tipo = "error"
                    };
                }

                if (!SqlServer.PodeReservar(dados))
                {
                    return new MensagemRetorno
                    {
                        Titulo = "Erro!",
                        Mensagem = "Conflito de horários, tente outro.",
                        Tipo = "error"
                    };
                }

                SqlServer.Inserir(dados);

                return new MensagemRetorno
                {
                    Titulo = "Sucesso!",
                    Mensagem = "Reserva feita com sucesso!",
                    Tipo = "success"
                };
            }
            catch (DadosInvalidosException e)
            {
                return new MensagemRetorno
                {
                    Titulo = "Dados Invalidos!",
                    Mensagem = e.Erro,
                    Tipo = "error"
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