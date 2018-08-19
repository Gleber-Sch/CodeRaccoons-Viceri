using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Valor de Exame.
    /// </summary>
    public class ValorExameNegocio : INegocioBase<ValorExame>
    {
        /// <summary>
        /// Declara o repositório do valor de exame.
        /// </summary>
        ValorExameRepositorio _valorExameRepositorio;

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public ValorExameNegocio()
        {
            _valorExameRepositorio = new ValorExameRepositorio();
        }

        /// <summary>
        /// Seleciona todos os valores de exames do Database.
        /// </summary>
        /// <returns>Lista de atendimentos.</returns>
        public IEnumerable<ValorExame> Selecionar()
        {
            return _valorExameRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe algum valor de exame com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um valor de exame no Database.</param>
        /// <returns>Seleciona um valor de exame ou gera uma exceção.</returns>
        public ValorExame SelecionarPorId(int id)
        {
            var obj = _valorExameRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nehum valor de exame com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se o ID do TipoExame e da clínica são válidos e se os campos foram preenchidos.
        /// Antes de inserir os dados do valor de exame no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do valor de exame.</param>
        /// <returns>ID do valorExame inserido no Databse ou gera uma exceção.</returns>
        public int Inserir(ValorExame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se o ID do TipoExame é válido.
            var RepositorioTipoExame = new TipoExameRepositorio();
            if (RepositorioTipoExame.SelecionarPorId(entity.IdTipoExame) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Tipo de Exame " +
                                                $"com o ID: {entity.IdTipoExame}");
            }

            //Verifica se o ID da Clinica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma Clinica " +
                                                $"com o ID: {entity.IdClinica}");
            }

            //Verifica se o valor valor do exame é válido.
            if (entity.Valor <= 0)
            {
                throw new DadoInvalidoException($"O valor: \"{entity.Valor}\", é inválido!");
            }

            return _valorExameRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o ID do TipoExame e da clínica são válidos e se os campos foram preenchidos.
        /// Antes de alterar os dados do valor de exame no Database.
        /// </summary>
        /// <param name="id">Usado para buscar um valor de exame no Database.</param>
        /// <param name="entity">Objeto com os dados do valor de exame.</param>
        /// <returns>Seleciona o valor de exame alterado no Database ou gera uma exceção.</returns>
        public ValorExame Alterar(int id, ValorExame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se o ID do TipoExame é válido.
            var RepositorioTipoExame = new TipoExameRepositorio();
            if (RepositorioTipoExame.SelecionarPorId(entity.IdTipoExame) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Tipo de Exame " +
                                                $"com o ID: {entity.IdTipoExame}");
            }

            //Verifica se o ID da Clinica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma Clinica " +
                                                $"com o ID: {entity.IdClinica}");
            }

            //Verifica se o valor valor do exame é válido.
            if (entity.Valor <= 0)
            {
                throw new DadoInvalidoException($"O valor: \"{entity.Valor}\", é inválido!");
            }

            entity.Id = id;
            _valorExameRepositorio.Alterar(entity);

            return _valorExameRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o valor de exame existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o atendimento no Database.</param>
        public void Deletar(int id)
        {
            var obj = _valorExameRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"O ID: {id}, não foi encontrado");

            _valorExameRepositorio.Deletar(id);
        }
    }
}