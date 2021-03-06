﻿using System;
using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Médico
    /// </summary>
    public class MedicoNegocio
    {
        /// <summary>
        /// Declara o repositório do Médico.
        /// </summary>
        private readonly MedicoRepositorio _medicoRepositorio;

        /// <summary>
        /// Declara e instancia o repositório do Paciente.
        /// </summary>
        private readonly PacienteRepositorio _pacienteRepositorio = new PacienteRepositorio();

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public MedicoNegocio()
        {
            _medicoRepositorio = new MedicoRepositorio();
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
            var obj = _medicoRepositorio.Login(email, senha);
            if (obj == null)
            {
                throw new NaoEncontradoException("Usúario não encontrado!");
            }

            var id = obj.Id;

            return id;
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
        public IEnumerable<MedicoDto> SelecionarPorEspecialidade(int id)
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
        /// Verifica se existe algum médico com o email indicado.
        /// </summary>
        /// <param name="email">Usado para buscar um médico no Database.</param>
        /// <returns>Seleciona um médico ou gera uma exceção.</returns>
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
        /// Verifica se o CPF, o CRM e o email já não estão cadastrados, se o CPF é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database e se o
        /// médico é maior de idade. Antes de inserir um médico.
        /// </summary>
        /// <param name="entity">Objeto com os dados do médico.</param>
        /// <returns>ID do médico inserido no Database ou gera alguma exceção.</returns>
        public int Inserir(Medico entity)
        {
            //Atera o status do médico que será enserido para TRUE.
            entity.StatusAtividade = true;

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
                if (_medicoRepositorio.SelecionarPorCpf(entity.Cpf) != null)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
                else if (_pacienteRepositorio.SelecionarPorCpf(entity.Cpf) != null)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
            }

            //Verifica se o email já foi casatrado.
            if (_medicoRepositorio.SelecionarPorEmail(entity.Email) != null)
            {
                throw new ConflitoException($"O email: \"{entity.Email}\", já foi cadastrado!");
            }

            //Verifica se o médico é maior de idade.
            if (Maioridade.Verificar(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos podem se cadastrar");
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
            Medico obj;

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

            //Verifica se o CRM já não foi cadastrado.
            obj = _medicoRepositorio.SelecionarPorCrm(entity.Crm);
            if (obj != null && obj.Id != id)
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
                obj = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);
                if (obj != null && obj.Id != id)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
                else if (_pacienteRepositorio.SelecionarPorCpf(entity.Cpf) != null)
                {
                    throw new ConflitoException($"O CPF: \"{entity.Cpf}\", já foi cadastrado!");
                }
            }

            //Verifica se o email já foi casatrado.
            obj = _medicoRepositorio.SelecionarPorEmail(entity.Email);
            if (obj != null && id != obj.Id)
            {
                throw new ConflitoException($"O email: \"{entity.Email}\", já foi cadastrado!");
            }

            //Verifica se o médico é maior de idade.
            if (Maioridade.Verificar(entity.DataNasc) == false)
            {
                throw new DadoInvalidoException("Idade inválida - Apenas maiores de 18 anos podem se cadastrar");
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
