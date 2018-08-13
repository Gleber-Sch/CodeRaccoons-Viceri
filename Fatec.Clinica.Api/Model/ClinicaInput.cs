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
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
