using Fatec.Clinica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class ConsultaInput
    {
        public Atendimento Atendimento { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataHora { get; set; }
        public string Historico { get; set; }
        public byte Nota { get; set; }
    }
}
