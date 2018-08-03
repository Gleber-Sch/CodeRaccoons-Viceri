using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class Consulta
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime Data { get; set; }
        public string Historico { get; set; }
    }
}
