using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class Exame
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int IdAtendimento { get; set; }
        public int IdConsulta { get; set; }
        public int IdTipoExame{ get; set; }
    }
}
