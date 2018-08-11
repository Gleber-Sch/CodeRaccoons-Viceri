using System.Collections.Generic;
using Fatec.Clinica.Negocio.Validacoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Excecoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o tipo de exame.
    /// </summary>
    public class TipoExameNegocio : INegocioBase<TipoExame>
    {
        /// <summary>
        /// Declara o repositório do Tipo de Exame.
        /// </summary>
        private readonly TipoExameRepositorio _tipoExameRepositorio;

        /// <summary>
        /// Construtor para instaciar o repositório.
        /// </summary>
        public TipoExameNegocio()
        {
            _tipoExameRepositorio = new TipoExameRepositorio();
        }

        /// <summary>
        /// Seleciona todas os Tipos de Exames.
        /// </summary>
        /// <returns>Lista de tipos de exame.</returns>
        public IEnumerable<TipoExame> Selecionar()
        {
            return _tipoExameRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe algum tipo de exame com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um tipo de exame no Database.</param>
        /// <returns>Seleciona um tipo de exame ou gera uma exceção.</returns>
        public TipoExame SelecionarPorId(int id)
        {
            var obj = _tipoExameRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new DadoInvalidoException($"Não foi encontrado nenhum Tipo de Exame com este ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se nome do tipo de exame foi preenchido, se ele respeita o limite de caracteres
        /// determinado no Database e se ele já foi cadastrado. Antes de inserir o tipo de exame.
        /// </summary>
        /// <param name="entity">Objeto com os dados do TipoExame.</param>
        /// <returns>ID da tipo de exame inserido no Databse ou gera alguma exceção.</returns>
        public int Inserir(TipoExame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo \"Nome\" é obrigatório e deve ser preenchido!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo \"Nome\" excedeu o limite de caracteres permitidos!");
            }

            //Verifica se o tipo de exame já não foi cadastrado.
            if (_tipoExameRepositorio.SelecionarPorNome(entity.Nome) != null)
            {
                throw new ConflitoException($"O tipo de exame: \"{entity.Nome}\", já foi cadastrado!");
            }

            return _tipoExameRepositorio.Inserir(entity);
        }

        /// <summary>
        ///Verifica se nome do tipo de exame foi preenchido, se ele respeita o limite de caracteres
        /// determinado no Database e se ele já foi cadastrado. Antes de alterar os dados do tipo de exame.
        /// </summary>
        /// <param name="entity">Objeto com os dados do TipoExame.</param>
        /// <param name="id">Usado para buscar o tipo de exame no Database.</param>
        /// <returns>Seleciona o tipo de exame alterado no Database ou gera uma exceção.</returns>
        public TipoExame Alterar(int id, TipoExame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo \"Nome\" é obrigatório e deve ser preenchido!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo \"Nome\" excedeu o limite de caracteres permitidos!");
            }

            //Verifica se o tipo de exame já não foi cadastrado.
            if (_tipoExameRepositorio.SelecionarPorNome(entity.Nome) != null)
            {
                throw new ConflitoException($"O tipo de exame: \"{entity.Nome}\", já foi cadastrado!");
            }

            entity.Id = id;
            _tipoExameRepositorio.Alterar(entity);

            return _tipoExameRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o Tipo de Exame existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o tipo de exame no Database.</param>
        public void Deletar(int id)
        {
            var obj = _tipoExameRepositorio.SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID {id} não foi encontrado");
            }

            _tipoExameRepositorio.Deletar(id);
        }

    }
}
