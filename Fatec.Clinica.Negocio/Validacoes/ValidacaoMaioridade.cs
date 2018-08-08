using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class ValidacaoMaioridade
    {
        /// <summary>
        /// Verifica se o usuário é maior de idade.
        /// </summary>
        /// <param name="DataNasc">Usado para calcular a idade do usuário.</param>
        /// <returns>TRUE se o usuário for maior de idade e FALSE caso não seja.</returns>
        public static bool Verificar(DateTime DataNasc)
        {
            if (DateTime.Now.Year - DataNasc.Year > 18)
            {
                return true;
            }
            else if (DateTime.Now.Year - DataNasc.Year == 18 &&
                    DateTime.Now.Month > DataNasc.Month ||
                    DateTime.Now.Month == DataNasc.Month &&
                    DateTime.Now.Day >= DataNasc.Day)
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
