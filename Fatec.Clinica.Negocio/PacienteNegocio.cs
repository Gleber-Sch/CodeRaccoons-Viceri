using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Negocio.Validacoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using System;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Paciente.
    /// </summary>
    public class PacienteNegocio : INegocioBase<Paciente>
    {
        /// <summary>
        /// Declara o repositório do Paciente.
        /// </summary>
        private readonly PacienteRepositorio _pacienteRepositorio;

        /// <summary>
        /// Declara e instancia o repositório do Paciente.
        /// </summary>
        private readonly MedicoRepositorio _medicoRepositorio = new MedicoRepositorio();

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public PacienteNegocio()
        {
            _pacienteRepositorio = new PacienteRepositorio();
        }

         /// <summary>
        /// Seleciona todas os Pacientes do Database.
        /// </summary>
        /// <returns>Lista de pacientes</returns>
        public IEnumerable<Paciente> Selecionar()
        {
            return _pacienteRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe o paciente com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um paciente no Database.</param>
        /// <returns>Seleciona um paciente ou gera uma exceção.</returns>
        public Paciente SelecionarPorId(int id)
        {
            var obj = _pacienteRepositorio.SelecionarPorId(id);

            if (obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado nenhum paciente com o ID: {id}");
            }
            
            return obj;
        }

        /// <summary>
        /// Verifica se existe algum paciente com o CPF indicado.
        /// </summary>
        /// <param name="cpf">Usado para buscar um paciente no Database.</param>
        /// <returns>Seleciona um paciente ou gera uma exceção.</returns>
        public Paciente SelecionarPorCpf(string cpf)
        {
            var obj = _pacienteRepositorio.SelecionarPorCpf(cpf);

            if (obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado nenhum paciente com o CPF: {cpf}");
            }

            return obj;
        }

        /// <summary>
        /// Verifica se existe algum paciente com o email indicado.
        /// </summary>
        /// <param name="email">Usado para buscar um paciente no Database.</param>
        /// <returns>Seleciona um paciente ou gera uma exceção.</returns>
        public Paciente SelecionarPorEmail(string email)
        {
            var obj = _pacienteRepositorio.SelecionarPorEmail(email);

            if (obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado nenhum paciente com o Email: {email}");
            }

            return obj;
        }

        /// <summary>
        /// Verifica se existe campos vazios, se algum campo escede o tamanho do campo e se existe algum usuario
        /// com este email e senha.
        /// </summary>
        /// <param name="usuario">Objeto com os dados do usúario.</param>
        /// <returns>Usuario selecionado ou gera exceção.</returns>
        public int Login(string email, string senha)
        {
            //Verifica se existem campos vazios.
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(senha))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (email.Length > 50 || senha.Length > 20)
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Verifica se existe algum usuario com o email e a senha indicados
            var obj = _pacienteRepositorio.Login(email, senha);
            if (obj == null)
            {
                throw new NaoEncontradoException("Usúario não encontrado!");
            }

            var id = obj.Id;

            return id;
        }

        /// <summary>
        /// Verifica se o CPF e o email já não estão cadastrados, se o CPF é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database e se o
        /// paciente é maior de idade. Antes de inserir um paciente.
        /// </summary>
        /// <param name="entity">Objeto com os dados do paciente.</param>
        /// <returns>ID do paciente inserido no Database ou gera alguma exceção.</returns>
        public int Inserir(Paciente entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Converte o gênero para caixa alta e verifica se o gênero é válido.
            if (GeneroValido.Verificar(GeneroValido.CaixaAlta(entity.Genero)) == false)
            {
                throw new DadoInvalidoException($"O gênero: \"{entity.Genero}\", é inválido!");
            }

            //Verifica se o formato e a quantidade de caracteres do celular são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.Celular)) == false)
            {
                throw new DadoInvalidoException($"O número de celular:\"{entity.Celular}\" é inválido!");
            }

            //Verifica se o formato e a quantidade de caracteres do telefone residencial são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.TelefoneRes)) == false)
            {
                throw new DadoInvalidoException($"O número de telefone residencial:\"{entity.TelefoneRes}\" é inválido!");
            }

            //Verifica se o CPF é válido e se ele já foi cadastrado.
            if (ValidacaoCpf.Verificar(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF: \"{entity.Cpf}\" é invalido!");
            }
            else
            {
                if (_pacienteRepositorio.SelecionarPorCpf(entity.Cpf) != null)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
                else if (_medicoRepositorio.SelecionarPorCpf(entity.Cpf) != null)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
            }

            //Verifica se o email já foi casatrado.
            if (_pacienteRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException($"O email: \"{entity.Email}\", já foi cadastrado!");
            }

            //Verifica se o médico é maior de idade.
            if (Maioridade.Verificar(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos podem se cadastrar");
            }

            return _pacienteRepositorio.Inserir(entity);
        }


        /// <summary>
        /// Verifica se o CPF e o email já não estão cadastrados, se o CPF é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database e se o
        /// paciente é maior de idade. Antes de alterar os dados de um paciente.
        /// </summary>
        /// <param name="id">Usado para buscar o paciente.</param>
        /// <param name="entity">Objeto com as informações a serem alteradas.</param>
        /// <returns>Seleciona um paciente do Database ou gera alguma exceção.</returns>
        public Paciente Alterar(int id, Paciente entity)
        {
            Paciente obj;

            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Converte o gênero para caixa alta e verifica se o gênero é válido.
            if (GeneroValido.Verificar(GeneroValido.CaixaAlta(entity.Genero)) == false)
            {
                throw new DadoInvalidoException($"O gênero: \"{entity.Genero}\", é inválido!");
            }

            //Verifica se o formato e a quantidade de caracteres do celular são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.Celular)) == false)
            {
                throw new DadoInvalidoException($"O número de celular:\"{entity.Celular}\" é inválido!");
            }

            //Verifica se o formato e a quantidade de caracteres do telefone residencial são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.TelefoneRes)) == false)
            {
                throw new DadoInvalidoException($"O número de telefone residencial:\"{entity.TelefoneRes}\" é inválido!");
            }

            //Verifica se o CPF é válido e se ele já foi cadastrado.
            if (ValidacaoCpf.Verificar(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF: \"{entity.Cpf}\" é invalido!");
            }
            else
            {
                obj = _pacienteRepositorio.SelecionarPorCpf(entity.Cpf);

                if (obj != null && id != obj.Id)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
            }

            //Verifica se o email já foi casatrado.
            obj = _pacienteRepositorio.SelecionarPorEmail(entity.Email);
            if (obj != null && id != obj.Id)
            {
                throw new ConflitoException($"O email: \"{entity.Email}\", já foi cadastrado!");
            }

            //Verifica se o paciente é maior de idade.
            if (Maioridade.Verificar(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos podem se cadastrar");
            }

            entity.Id = id;
            _pacienteRepositorio.Alterar(entity);

            return _pacienteRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o paciente existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o paciente no Database.</param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);

            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");
            }
            _pacienteRepositorio.Deletar(obj.Id);
        }

    }
}
