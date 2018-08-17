using System;

namespace Fatec.Clinica.Dominio.Dto
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string DataNasc
        {
            get
            {
                return Convert.ToDateTime(DataNasc).ToString("dd/MM/yyyy");
            }
            set
            {
                DataNasc = value;
            }
        }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public char Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
