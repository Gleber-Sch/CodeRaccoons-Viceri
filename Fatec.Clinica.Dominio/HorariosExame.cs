using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class HorariosExame
    {
        public int Id { get; set; }
        public DateTime DiaHora { get; set; }

        public Decimal Valor { get; set; }

        public int IdAtendimento { get; set; }

        public int IdTipoExame { get; set; }
    }
}


