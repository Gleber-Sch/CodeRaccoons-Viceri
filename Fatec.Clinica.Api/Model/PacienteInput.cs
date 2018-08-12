using System;

namespace Fatec.Clinica.Api.Model
{
    public class PacienteInput
    {
        private DateTime dataNasc;

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public DateTime DataNasc
        {
            get
            {
                return dataNasc;
            }
            set
            {
                dataNasc = DataNasc.Date;
            }
        }
        public char Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string TelefoneRes { get; set; }
    }
}
