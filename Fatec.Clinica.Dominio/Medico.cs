namespace Fatec.Clinica.Dominio
{
    public class Medico : Pessoa
    {
        public int Crm { get; set; }
        public int IdEspecialidade { get; set; }
        public string Telefone_Com { get; set; }
    }
}
