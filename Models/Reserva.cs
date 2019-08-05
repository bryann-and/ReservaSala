using System;
using System.ComponentModel.DataAnnotations;

namespace ReservaSala.Models
{
    public class Reserva
    {
        public Sala Sala { get; set; }
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Data de inicio")]
        public string DataInicio
        {
            get => dataInicio.ToString("dd/MM/yyyy HH:mm");
            set
            {
                dataInicio = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        [Display(Name = "Data de término")]
        public string DataTermino
        {
            get => dataTermino.ToString("dd/MM/yyyy HH:mm");
            set
            {
                dataTermino = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private DateTime dataInicio { get; set; }
        private DateTime dataTermino { get; set; }
    }
}
