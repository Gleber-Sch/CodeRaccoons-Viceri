namespace Fatec.Clinica.Dominio
{
    public class Medico : Pessoa
    {
        public int Crm { get; set; }
        public string CrmEstado { get; set; }
        public int IdEspecialidade { get; set; }
        public bool StatusAtividade { get; set; }
    }
}
