namespace Fatec.Clinica.Api.Model
{
    public class ClinicaInput
    {
        public string Cnpj { get; set; }
        public bool StatusAtividade { get; set; }
        public string TelefoneCom { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int IdEndereco { get; set; }
    }
}
