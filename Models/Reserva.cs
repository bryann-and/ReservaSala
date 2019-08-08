using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservaSala.Models
{
    public class Reserva
    {
        /// <summary>
        /// Sala da reserva
        /// </summary>
        public Sala Sala { get; set; }
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "Escolha uma descrição com no mínimo 5 caracteres")]
        [MaxLength(100, ErrorMessage = "Tamanho excedido, máximo de 100 caracteres!")]
        public string Descricao { get; set; }

        [Display(Name = "Data de inicio")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataBrasileira(ErrorMessage = "Data inválida!")]
        public string DataInicio { get; set; }

        [Display(Name = "Data de término")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataBrasileira(ErrorMessage = "Data inválida!")]
        public string DataTermino { get; set; }

        /// <summary>
        /// Usado para validação manual, como verificação na API
        /// </summary>
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
