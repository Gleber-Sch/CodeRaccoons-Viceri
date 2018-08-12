using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Médico
    /// </summary>
    public class MedicoNegocio : INegocioBase<Medico>
    {
        /// <summary>
        /// Declara o repositório do Médico.
        /// </summary>
        private readonly MedicoRepositorio _medicoRepositorio;

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public MedicoNegocio()
        {
            _medicoRepositorio = new MedicoRepositorio();
        }

        /// <summary>
        /// Seleciona todas os Médicos do Database.
        /// </summary>
        /// <returns>Lista de médicos</returns>
        public IEnumerable<Medico> Selecionar()
        {
            return _medicoRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe algum médico com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um médico no Database.</param>
        /// <returns>Seleciona um médico ou gera uma exceção.</returns>
        public Medico SelecionarPorId(int id)
        {
            var obj = _medicoRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado um médico com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se existe algum médico com a especialidade indicada.
        /// </summary>
        /// <param name="id">Usado para buscar o ID da especialidade no Database.</param>
        /// <returns>Lista de médicos ou gera uma exceção.</returns>
        public IEnumerable<Medico> SelecionarPorEspecialidade(int id)
        {
            var lista = _medicoRepositorio.SelecionarPorEspecialidade(id);

            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum médico com esta epecialidade!" +
                                                 $" (ID da Especialidade: {id})");
            return lista;
        }

        /// <summary>
        /// Verifica se o medico com o CRM indicado existe.
        /// </summary>
        /// <param name="crm">Usado para buscar um médico no Database.</param>
        /// <returns>Seleciona um médico ou gera uma exceção.</returns>
        public Medico SelecionarPorCrm(int crm)
        {
            var obj = _medicoRepositorio.SelecionarPorCrm(crm);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum médico com o CRM: {crm}");

            return obj;
        }

        /// <summary>
        /// Verifica se existe o médico com o CPF indicado.
        /// </summary>
        /// <param name="cpf">Usado para buscar um médico no Database.</param>
        /// <returns>Seleciona um médico ou gera uma exceção.</returns>
        public Medico SelecionarPorCpf(string cpf)
        {
            var obj = _medicoRepositorio.SelecionarPorCpf(cpf);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum médico com o CPF: {cpf}");

            return obj;
        }

        /// <summary>
        /// Verifica se o CPF, o CRM e o email já não estão cadastrados, se o CPF é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database e se o
        /// médico é maior de idade. Antes de inserir um médico.
        /// </summary>
        /// <param name="entity">Objeto com os dados do médico.</param>
        /// <returns>ID do médico inserido no Database ou gera alguma exceção.</returns>
        public int Inserir(Medico entity)
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

            //Verifica se o formato e a quantidade de caracteres do celular são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.Celular)) == false)
            {
                throw new DadoInvalidoException($"O número de celular:\"{entity.Celular}\" é inválido!");
            }

            //Verifica se o CRM já não foi cadastrado.
            if (_medicoRepositorio.SelecionarPorCrm(entity.Crm) != null)
            {
                throw new ConflitoException($"O CRM: \"{entity.Crm}\", já foi cadastrado!");
            }

            //Verifica se o CPF é válido e se ele já foi cadastrado.
            if (ValidacaoCpf.Verificar(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF: \"{entity.Cpf}\" é invalido!");
            }
            else
            {
                var cpfExistente = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);

                if (cpfExistente != null)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
            }

            //Verifica se o email já foi casatrado.
            if (_medicoRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException($"O email: \"{entity.Email}\", já foi cadastrado!");
            }

            return _medicoRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o CPF, o CRM e o email já não estão cadastrados, se o CPF é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database e se o
        /// médico é maior de idade. Antes de alterar os dados sobre um médico.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <param name="entity">Objeto com as informações a serem alteradas.</param>
        /// <returns>Seleciona um médico do Database ou gera alguma exceção.</returns>
        public Medico Alterar(int id, Medico entity)
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

            //Verifica se o formato e a quantidade de caracteres do celular são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.Celular)) == false)
            {
                throw new DadoInvalidoException($"O número de celular:\"{entity.Celular}\" é inválido!");
            }

            //Verifica se o CRM já não foi cadastrado.
            if (_medicoRepositorio.SelecionarPorCrm(entity.Crm) != null)
            {
                throw new ConflitoException($"O CRM: \"{entity.Crm}\", já foi cadastrado!");
            }

            //Verifica se o CPF é válido e se ele já foi cadastrado.
            if (ValidacaoCpf.Verificar(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF: \"{entity.Cpf}\" é invalido!");
            }
            else
            {
                var cpfExistente = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);

                if (cpfExistente != null)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
            }

            //Verifica se o email já foi casatrado.
            if (_medicoRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException($"O email: \"{entity.Email}\", já foi cadastrado!");
            }

            entity.Id = id;
            _medicoRepositorio.Alterar(entity);

            return _medicoRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o médico existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        public void Deletar(int id)
        {
            //Verifica se o ID do médico existe.
            var obj = SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");
            }

            _medicoRepositorio.Deletar(obj.Id);
        }
    }
}
