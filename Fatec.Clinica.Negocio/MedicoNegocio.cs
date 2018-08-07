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
    public class MedicoNegocio : Validacao, INegocioBase<Medico>
    {

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
        /// Verifica se o medico com o ID indicado existe.
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
                                                 $" (ID da Especialidade {id})");
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
                throw new NaoEncontradoException($"Não foi encontrado nenhum médico com o CRM {crm}!");
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
                throw new NaoEncontradoException($"Não foi encontrado nenhum médico com o CPF {cpf}!");
            return obj;
        }

        /// <summary>
        /// Verifica se o CPF e o CRM já não estão cadastrados, se o CPF é válido e se existem campos obrigatórios
        /// sem serem preenchidos. Antes de inserir um médico.
        /// </summary>
        /// <param name="entity">Objeto com os dados do médico a ser inserido.</param>
        /// <returns>ID do médico inserido no Database ou exceção.</returns>
        public int Inserir(Medico entity)
        {

            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException($"Os seguintes campos são obrigatórios:" +
                                                $"Nome, CPF, CRM, Telefone Movel, Gênero, " +
                                                $"Data de Nascimento e Especialidade");
            }


            if (_medicoRepositorio.SelecionarPorCrm(entity.Crm) != null)
                throw new ConflitoException($"Já existe cadastrado o CRM {entity.Crm}!");

            if (VerificarCPF(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF {entity.Cpf} é invalido");
            }
            else
            {
                var cpfExistente = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);

                if (cpfExistente != null)
                    throw new ConflitoException($"Já existe cadastrado o CPF {cpfExistente.Cpf}!");
            }

            if (_medicoRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException("O email já foi cadastrado");
            }

            if (VerificarIdade(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos!!");
            }

            return _medicoRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o CPF e o CRM já não estão cadastrados, se o CPF é válido e se existem campos obrigatórios
        /// sem serem preenchidos. Antes de alterar os dados sobre um médico.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <param name="entity">Objeto com as informações a serem alteradas.</param>
        /// <returns>Médico selecionado do Database ou exceção.</returns>
        public Medico Alterar(int id, Medico entity)
        {

            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException($"Os seguintes campos são obrigatórios:" +
                                                $"Nome, CPF, CRM, Telefone Movel, Gênero, " +
                                                $"Data de Nascimento e Especialidade");
            }

            var crmExistente = _medicoRepositorio.SelecionarPorCrm(entity.Crm);

            if (crmExistente != null)
            {
                if (crmExistente.Id != id)
                    throw new ConflitoException($"Já existe cadastrado o CRM {crmExistente.Crm}, para outro médico!");
            }

            if (VerificarCPF(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF {entity.Cpf} é invalido!");
            }
            else
            {
                var cpfExistente = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);

                if (cpfExistente != null)
                {
                    if (cpfExistente.Id != id)
                        throw new ConflitoException($"Já existe cadastrado o CPF {cpfExistente.Cpf}, para outro médico!");
                }
            }

            if (_medicoRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException("O email já foi cadastrado");
            }

            if (VerificarIdade(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos!!");
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
            var obj = SelecionarPorId(id);

            if (obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado nenhum Médico com este ID: {id}");
            }
            _medicoRepositorio.Deletar(obj.Id);
        }
    }
}
