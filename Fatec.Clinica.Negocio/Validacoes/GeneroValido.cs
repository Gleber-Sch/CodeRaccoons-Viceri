using System;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class GeneroValido
    {
        /// <summary>
        /// Coverte o valor do gênero para caixa alta.
        /// </summary>
        /// <param name="genero">Dado a ser convertido.</param>
        /// <returns>gênero em caixa alta.</returns>
        public static string CaixaAlta(string genero)
        {
            genero = genero.ToUpper();
            return genero;
        }
        /// <summary>
        /// Verfica se o gênero é F (Feminino) ou M (Masculino)
        /// </summary>
        /// <remarks>Use somente após o método CaixaAlta.</remarks>
        /// <param name="genero">Dado a ser comparado.</param>
        /// <returns>TRUE se o gênero for válido ou FALSE caso ele seja inválido.</returns>
        public static bool Verificar(string genero)
        {
            if(!(genero.Equals('F')) &&
               !(genero.Equals('M')))
            {
                return false;
            }

            return true;
        }
    }
}
