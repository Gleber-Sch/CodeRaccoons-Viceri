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

            string DigitosCpf, DigitoVerificador = null;
            int Soma = 0, Resto = 0, Multiplicador, Contador = 0;

            //Gera uma string sem os dígitos verificadores
            DigitosCpf = cpf.Substring(0, 9);

            while (Contador < 2)
            {
                Multiplicador = 10 + Contador;
                for (int i = 0; i < 9; i++, Multiplicador--)
                    Soma += int.Parse(DigitosCpf[i].ToString()) * Multiplicador;

                Resto = Soma % 11;

                if (Resto < 2)
                    Resto = 0;
                else
                    Resto = 11 - Resto;

                DigitoVerificador = Resto.ToString();
                DigitosCpf += DigitoVerificador;
                Soma = 0;
                Contador++;
            }

            DigitoVerificador += Resto.ToString();

            //Compara se DigitoVerificador é igual ao foi recebido como parâmetro pelo método e retorna um booleano
            return cpf.EndsWith(DigitoVerificador);
        }
    }
}
