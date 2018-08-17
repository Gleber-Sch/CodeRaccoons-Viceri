namespace Fatec.Clinica.Dominio.Dto
{
    public class MedicoDto : PessoaDto
    {
        public int Crm { get; set; }
        public int IdEspecialidade { get; set; }
        public bool StatusAtividade { get; set; }
        public string Especialidade { get; set; }
    }
}
