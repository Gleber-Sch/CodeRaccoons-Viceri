using System;

namespace Fatec.Clinica.Dominio
{
    public class Horarios
    {
        public int Id { get; set; }
        public string Dia { get; set; }
        public string Horario { get; set; }
        public int IdClinica { get; set; }

    }
}
