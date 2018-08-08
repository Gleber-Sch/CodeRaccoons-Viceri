namespace Fatec.Clinica.Negocio.Validacoes
{
    public class ValidacaoCnpj
    {
        /// <summary>
        /// Verifica se o CNPJ é válido.
        /// </summary>
        /// <param name="cnpj">Dado a ser verificado.</param>
        /// <returns>TRUE se o CNPJ é válido ou FALSE caso seja inválido.</returns>
        public static bool Verificar(string cnpj)
        {
            //Limpa a formatação da String
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            //Verifica se existem caracteres diferentes de números
            if (long.TryParse(cnpj, out long somenteNumero) == false)
            {
                return false;
            }

            //Se o CNPJ não tem 14 digitos ele é inválido
            if (cnpj.Length != 14)
                return false;

            int[] Multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int Soma, Resto;
            string DigitoVerificador, DigitosCnpj;

            //Gera uma string sem os dígitos verificadores
            DigitosCnpj = cnpj.Substring(0, 12);

            Soma = 0;
            for (int i = 0; i < 12; i++)
                Soma += int.Parse(DigitosCnpj[i].ToString()) * Multiplicador1[i];

            Resto = (Soma % 11);
            if (Resto < 2)
                Resto = 0;
            else
                Resto = 11 - Resto;

            DigitoVerificador = Resto.ToString();

            DigitosCnpj = DigitosCnpj + DigitoVerificador;
            Soma = 0;
            for (int i = 0; i < 13; i++)
                Soma += int.Parse(DigitosCnpj[i].ToString()) * Multiplicador2[i];

            Resto = (Soma % 11);
            if (Resto < 2)
                Resto = 0;
            else
                Resto = 11 - Resto;

            DigitoVerificador += Resto.ToString();

            //Compara se DigitoVerificador é igual ao foi recebido como parâmetro pelo método e retorna um booleano
            return cnpj.EndsWith(DigitoVerificador);
        }
    }
}
