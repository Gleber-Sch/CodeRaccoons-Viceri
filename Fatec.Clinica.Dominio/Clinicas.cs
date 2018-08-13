namespace Fatec.Clinica.Dominio
{
    public class Clinicas
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cnpj { get; set; }
        public bool StatusAtividade { get; set; }
        public string TelefoneCom { get; set; }
        public string Nome { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
