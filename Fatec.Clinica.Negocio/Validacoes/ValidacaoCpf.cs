namespace Fatec.Clinica.Negocio.Validacoes
{
    public static class ValidacaoCpf
    {
        /// <summary>
        /// Verifica se o CPF é válido.
        /// </summary>
        /// <param name="cpf">Dado a ser verificado.</param>
        /// <returns>TRUE se o CPF é válido ou FALSE caso ele seja inválido.</returns>
        public static bool Verificar(string cpf)
        {
            //Limpa a formatação da String
            cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (long.TryParse(cpf, out long somenteNumero) == false)
                return false;

            //Se o CPF não tem 11 digitos ele é inválido
            if (cpf.Length != 11)
                return false;

            //Cpf válidos, porém não utilizados.
            if (cpf == "00000000000000" ||
               cpf == "11111111111111" ||
               cpf == "22222222222222" ||
               cpf == "33333333333333" ||
               cpf == "44444444444444" ||
               cpf == "55555555555555" ||
               cpf == "66666666666666" ||
               cpf == "77777777777777" ||
               cpf == "88888888888888" ||
               cpf == "99999999999999")
            {
                return false;
            }

            int[] Multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] Multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string DigitosCpf, DigitoVerificador;
            int Soma=0, Resto;

            //Gera uma string sem os dígitos verificadores
            DigitosCpf = cpf.Substring(0, 9);

            for (int i = 0; i < 9; i++)
            {
                Soma += int.Parse(DigitosCpf[i].ToString()) * Multiplicador1[i];
            }      

            Resto = Soma % 11;

            if (Resto < 2)
            {
                Resto = 0;
            }
            else
            {
                Resto = 11 - Resto;
            }     

            DigitoVerificador = Resto.ToString();

            DigitosCpf += DigitoVerificador;

            Soma = 0;

            for (int i = 0; i < 10; i++)
            {
                Soma += int.Parse(DigitosCpf[i].ToString()) * Multiplicador2[i];
            }  

            Resto = Soma % 11;

            if (Resto < 2)
            {
                Resto = 0;
            }
            else
            {
                Resto = 11 - Resto;
            }

            DigitoVerificador += Resto.ToString();

            //Compara se DigitoVerificador é igual ao foi recebido como parâmetro pelo método e retorna um booleano
            return cpf.EndsWith(DigitoVerificador);
        }
    }
}
