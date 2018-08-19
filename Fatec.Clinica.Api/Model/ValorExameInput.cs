using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class ValorExameInput
    {
        public int IdTipoExame { get; set; }
        public int IdClinica { get; set; }
        public decimal Valor { get; set; }
    }
}
