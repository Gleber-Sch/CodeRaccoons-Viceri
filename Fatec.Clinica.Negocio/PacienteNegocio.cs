using System;
using System.Collections.Generic;
using System.Text;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Negocio.Validacoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Paciente.
    /// </summary>
    public class PacienteNegocio : Validacao, INegocioBase<Paciente>
    {
        
        private readonly PacienteRepositorio _pacienteRepositorio;


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
        /// Verifica se o paciente com o ID indicado existe.
        /// </summary>
        /// <param name="id">Usado para buscar um paciente no Database.</param>
        /// <returns>Seleciona um paciente ou gera uma exceção.</returns>
        public Paciente SelecionarPorId(int id)
        {
            var obj = _pacienteRepositorio.SelecionarPorId(id);

            if(obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum paciente com o ID {id}!");
            return obj;
        }


        /// <summary>
        /// Verifica se existe o paciente com o CPF indicado.
        /// </summary>
        /// <param name="cpf">Usado para buscar um paciente no Database.</param>
        /// <returns>Seleciona um paciente ou gera uma exceção.</returns>
        public Paciente SelecionarPorCpf(string cpf)
        {
            var obj = _pacienteRepositorio.SelecionarPorCpf(cpf);

            if(obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum paciente com o CPF {cpf}!");
            return obj;
        }

        /// <summary>
        /// Verifica se o CPF já não esta cadastrado e se ele é válido, e se existem campos obrigatórios
        /// sem serem preenchidos. Antes de inserir um paciente.
        /// </summary>
        /// <param name="entity">Objeto com os dados do paciente a ser inserido.</param>
        /// <returns>ID do paciente inserido no Database ou exceção.</returns>
        public int Inserir(Paciente entity)
        {

            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException($"Os seguintes campos são obrigatórios:" +
                                                $"Nome, CPF, Telefone Movel, Gênero e Data de Nascimento");
            }

            if (VerificarCPF(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF {entity.Cpf} é invalido");
            }
            else
            {
                var cpfExistente = _pacienteRepositorio.SelecionarPorCpf(entity.Cpf);

                if (cpfExistente != null)
                    throw new ConflitoException($"Já existe cadastrado o CPF {cpfExistente.Cpf}!");
            }

            if (_pacienteRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException("O email já foi cadastrado");
            }

            if (VerificarIdade(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos!!");
            }

            return _pacienteRepositorio.Inserir(entity);
        }


        /// <summary>
        /// Verifica se o CPF já não esta cadastrado e se ele é válido, e se existem campos obrigatórios
        /// sem serem preenchidos. Antes de alterar os dados sobre um paciente.
        /// </summary>
        /// <param name="id">Usado para buscar o paciente.</param>
        /// <param name="entity">Objeto com as informações a serem alteradas.</param>
        /// <returns>Paciente selecionado do Database ou exceção.</returns>
        public Paciente Alterar(int id, Paciente entity)
        {

            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException($"Os seguintes campos são obrigatórios:" +
                                                $"Nome, CPF, Telefone Movel, Gênero e Data de Nascimento");
            }

            if (VerificarCPF(entity.Cpf) == false)
            {
                throw new DadoInvalidoException($"O CPF {entity.Cpf} é invalido!");
            }
            else
            {
                var cpfExistente = _pacienteRepositorio.SelecionarPorCpf(entity.Cpf);

                if (cpfExistente != null)
                {
                    if (cpfExistente.Id != id)
                        throw new ConflitoException($"Já existe cadastrado o CPF {cpfExistente.Cpf}!");
                }
                           
            }

            if (_pacienteRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException("O email já foi cadastrado");
            }

            if (VerificarIdade(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos!!");
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

            if(obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado um Paciente com este ID {id}!");
            }
            _pacienteRepositorio.Deletar(obj.Id);
        }

    }
}
