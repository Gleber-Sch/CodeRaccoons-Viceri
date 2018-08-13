using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class HorariosConsultaInput
    {
        public DateTime DiaHora { get; set; }
        public Decimal Valor { get; set; }
        public int IdAtendimento { get; set; }

    }
}
