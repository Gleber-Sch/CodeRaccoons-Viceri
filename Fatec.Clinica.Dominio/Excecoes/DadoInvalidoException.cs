using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio.Excecoes
{
    public class DadoInvalidoException : Exception
    {
        public DadoInvalidoException()
        {
        }

        public DadoInvalidoException(int id)
        {
        }

        public DadoInvalidoException(string message) : base(message)
        {
        }
    }
}
