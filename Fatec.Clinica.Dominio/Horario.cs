using System;

namespace Fatec.Clinica.Dominio
{
    public class Horario
    {
        public int Id { get; set; }
        public DateTime DiaHora { get; set; }
        public int IdClinica { get; set; }
    }
}
