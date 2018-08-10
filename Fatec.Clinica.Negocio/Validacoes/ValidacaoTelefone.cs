using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class ValidacaoTelefone
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
        /// Verifica se o telefone possui apenas números.
        /// </summary>
        /// <remarks>Usar somente após a utilização do método LimparFormatacao.</remarks>
        /// <param name="telefone">Contêm o telefone a ser verificado.</param>
        /// <returns>TRUE se o tefelone possuir apenas números ou FALSE caso não tenha.</returns>
        public static bool Verificar(string telefone)
        {
            if(int.TryParse(telefone, out int x))
            {
                return true;
            }
            return false;
        }
    }
}
