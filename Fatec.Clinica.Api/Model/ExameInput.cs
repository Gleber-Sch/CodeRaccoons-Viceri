using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class ExameInput
    {
        public DateTime DataHora { get; set; }
        public int IdAtendimento { get; set; }
        public int IdConsulta { get; set; }
        public int IdTipoExame { get; set; }
    }
}
