using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Bibliotecas;
using ReservaSala.Models;

namespace ReservaSala.Controllers.API
{
    [Route("api/Salas")]
    [ApiController]
    public class SalaAPI : ControllerBase
    {
        [HttpGet]
        public List<Sala> Get()
        {
            return SqlServer.ListaSalas();
        }

        [HttpGet("{id:int}")]
        public Sala Get(int id)
        {
            if (id < 1)
            {
                return new Sala();
            }

            return SqlServer.ListaSalas(id);
        }

        [HttpPost]
        [Route("CadastroSala")]
        public MensagemRetorno Post(Sala dados)
        {
            try
            {
                // valida o model, caso não esteja de acordo, gera uma DadosInvalidosException
                dados.Validar();
                SqlServer.Inserir(dados);

                return new MensagemRetorno
                {
                    Titulo = "Sucesso!",
                    Mensagem = "Sala cadastrada com sucesso!",
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
