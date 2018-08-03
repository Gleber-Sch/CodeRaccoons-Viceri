using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class Exame
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public Atendimento Atendimento { get; set; }
        public Consulta Consulta { get; set; }
        public TipoExame TipoExame{ get; set; }
    }
}
