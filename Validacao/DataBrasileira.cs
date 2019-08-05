using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Usado para validar se é uma data valida no formato brasileiro
    /// </summary>
    public class DataBrasileira : ValidationAttribute
    {
        public string Formato { get; set; }

        public DataBrasileira(string Formato = "dd/MM/yyyy HH:mm")
        {
            this.Formato = Formato;
        }

        public override bool IsValid(object value)
        {
            try
            {
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
