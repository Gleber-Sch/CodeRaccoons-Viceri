using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class TelefoneValido
    {
        /// <summary>
        /// Remove caracteres não numéricos que podem estar presentes na string telefone.
        /// </summary>
        /// <param name="telefone">Contêm o dado ter a formatação limpa.</param>
        /// <returns>Telefone formatada com a formataçaõ limpa.</returns>
        public static string LimparFormatacao(string telefone)
        {
            telefone.Trim();
            telefone = telefone.Replace("+","").Replace("(", "").Replace(")","").Replace("-", "");

            return telefone;
        }

        /// <summary>
        /// Verifica se o telefone possui apenas números e se a quantidade de números é válida.
        /// </summary>
        /// <remarks>Usar somente após a utilização do método LimparFormatacao.</remarks>
        /// <param name="telefone">Contêm o telefone a ser verificado.</param>
        /// <returns>True se o tefelone possuir apenas números e se a quantidade de números for válida ou
        /// FALSE caso contrário.</returns>
        public static bool Verificar(string telefone)
        {
            if(long.TryParse(telefone, out long x) &&
               telefone.Length >= 10 &&
               telefone.Length <= 11)
            {
                return true;
            }

            return false;
        }
    }
}
