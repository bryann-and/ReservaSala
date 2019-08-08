using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Bibliotecas;
using ReservaSala.Models;

namespace ReservaSala.Controllers.API
{
    [Route("api/Reservas")]
    //[ApiController]
    //removido o DataAnnotation ApiController pq com ele o model é verificado automaticamente, mas isso faz com que a mensagem de resposta não tenha um padrão
    public class ReservasAPI : ControllerBase
    {
        [HttpGet]
        public List<Reserva> Get()
        {
            return SqlServer.ListaReservas();
        }        

        [HttpPost]
        [Route("CadastroReserva")]
        public MensagemRetorno Post([FromBody] Reserva dados)
        {
            try
            {
                // validação dos dados
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

                // verificando se não existe comflito de horario
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