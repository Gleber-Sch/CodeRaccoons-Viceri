namespace Fatec.Clinica.Api.Model
{
    public class ValorExameInput
    {
        public int IdTipoExame { get; set; }
        public int IdClinica { get; set; }
        public decimal Valor { get; set; }
    }
}