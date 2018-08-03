namespace Fatec.Clinica.Dominio
{
    public class Atendimento
    {
        public int Id { get; set; }
        public Clinica Clinica { get; set; }
        public Medico Medico { get; set; }
    }
}