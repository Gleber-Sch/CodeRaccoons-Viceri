using System;

namespace Fatec.Clinica.Dominio
{
    public abstract class Pessoa
    {
        public DateTime DataNasc { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
