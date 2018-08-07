using Fatec.Clinica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class AtedimentoInput
    {
        public Clinicas Clinica { get; set; }
        public Medico Medico { get; set; }
    }
}
