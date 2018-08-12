using System;

namespace Fatec.Clinica.Api.Model
{
    public class PacienteInput
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public DateTime DataNasc { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string TelefoneRes { get; set; }
    }
}
