using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Usado para validar se é uma data valida no formato brasileiro
    /// </summary>
    public class DataBrasileira : ValidationAttribute
    {
        /// <summary>
        /// Armazena o formato a ser verificado
        /// </summary>
        private string Formato { get; set; }

        /// <summary>
        /// Inicializa o objeto a validação com o formato desejado
        /// </summary>
        /// <param name="Formato">Formato a ser validado, padrâo: dd/MM/yyyy HH:mm</param>
        public DataBrasileira(string Formato = "dd/MM/yyyy HH:mm")
        {
            this.Formato = Formato;
        }

        /// <summary>
        /// Chamada automaticamente na execução da validação do model
        /// </summary>
        public override bool IsValid(object value)
        {
            try
            {
                // tenta converter o objeto com o formato desejado, caso cause um exceçã, quer dizer q esta incorreto
                DateTime.ParseExact(value.ToString(), Formato, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
