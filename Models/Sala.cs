using ReservaSala.Bibliotecas;
using ReservaSala.Controllers.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaSala.Models
{
    public class Sala
    {
        [Required(ErrorMessage = "Escolha uma Sala!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Nome da Sala")]
        [MinLength(5, ErrorMessage = "Escolha um nome com no mínimo 5 caracteres")]
        [MaxLength(100, ErrorMessage = "Tamanho excedido, máximo de 100 caracteres!")]
        public string Nome { get; set; }


        public Sala() { }

        /// <summary>
        /// Carrega o objeto com as informações da sala com o Id fornecido
        /// </summary>
        /// <param name="id_sala">ID da sala</param>
        public Sala(int id_sala)
        {
            SalaAPI api = new SalaAPI();
            Sala dados = api.Get(id_sala);

            Id = dados.Id;
            Nome = dados.Nome;
        }

        public void Validar()
        {
            List<ValidationResult> erros = new List<ValidationResult>();

            if (!Validator.TryValidateObject(this, new ValidationContext(this), erros, true))
            {
                throw new DadosInvalidosException(erros);
            }
        }
    }
}
