namespace Fatec.Clinica.Dominio
{
    public class ValorConsulta
    {
        public int Id { get; set; }
        public int IdEspecialidade { get; set; }
        public int IdClinica { get; set; }
        public decimal Valor { get; set; }
    }
}
