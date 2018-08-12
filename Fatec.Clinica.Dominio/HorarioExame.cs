using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class HorarioExame
    {
        public int Id { get; set; }
        public DateTime DiaHora { get; set; }
        public string Valor { get; set; }
        public int IdAtendimento { get; set; }
    }
}