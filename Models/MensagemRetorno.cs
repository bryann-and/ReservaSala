namespace ReservaSala.Models
{
    public class MensagemRetorno
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }

        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
