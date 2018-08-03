using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class Consulta
    {
        public int Id { get; set; }
        public Atendimento Atendimento { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataHora { get; set; }
        public string Historico { get; set; }
        public byte Nota { get; set; }
    }
}
