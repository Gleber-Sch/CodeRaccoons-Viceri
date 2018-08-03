using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class Clinicas
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public char StatusAtividade { get; set; }
        public string TelefoneCom { get; set; }
    }
}
