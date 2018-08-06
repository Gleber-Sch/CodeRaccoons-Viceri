using System;

namespace Fatec.Clinica.Api.Model

{
    public class MedicoInput
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public DateTime DataNasc { get; set; }
        public char Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Crm { get; set; }
        public int IdEspecialidade { get; set; }
        public bool StatusAtividade { get; set; }
    }
}
