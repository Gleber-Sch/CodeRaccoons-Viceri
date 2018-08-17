using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class Maioridade
    {
        /// <summary>
        /// Verifica se o usuário é maior de idade.
        /// </summary>
        /// <param name="DataNasc">Usado para calcular a idade do usuário.</param>
        /// <returns>TRUE se o usuário for maior de idade e FALSE caso não seja.</returns>
        public static bool Verificar(DateTime dataNasc)
        {
              if (DateTime.Now.Year - dataNasc.Year > 18)
            {
                return true;
            }
            else if (DateTime.Now.Year - dataNasc.Year == 18 &&
                     DateTime.Now.Month > dataNasc.Month ||
                     DateTime.Now.Month == dataNasc.Month &&
                     DateTime.Now.Day >= dataNasc.Day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
