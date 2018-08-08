namespace Fatec.Clinica.Dominio
{
    public class Endereco
    {
        public int Id { get; set; }
        public char Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public int IdClinica { get; set; }
    }
}