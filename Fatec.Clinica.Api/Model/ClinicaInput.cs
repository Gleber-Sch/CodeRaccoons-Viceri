using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class ClinicaInput
    {
        public string Cnpj { get; set; }
        public bool StatusAtividade { get; set; }
        public string TelefoneCom { get; set; }
        public string Nome { get; set; }
    }
}
