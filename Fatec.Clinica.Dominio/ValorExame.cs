namespace Fatec.Clinica.Dominio
{
    public class ValorExame
    {
        public int Id { get; set; }
        public int IdTipoExame { get; set; }
        public int IdClinica { get; set; }
        public decimal Valor { get; set; }
    }
}