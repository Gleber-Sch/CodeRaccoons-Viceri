using System;

namespace Fatec.Clinica.Dominio
{
    public class Consulta
    {
        public int Id { get; set; }
        public int IdAtendimento { get; set; }
        public int IdPaciente { get; set; }
        public DateTime DataHora { get; set; }
        public string Historico { get; set; }
        public byte Nota { get; set; }
    }
}
