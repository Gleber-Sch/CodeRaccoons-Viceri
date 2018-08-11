using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Atendimento.
    /// </summary>
    public class AtendimentoNegocio : INegocioBase<Atendimento>
    {
        /// <summary>
        /// Declara o repositório do Atendimento.
        /// </summary>
        AtendimentoRepositorio _atendimentoRepositorio;

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public AtendimentoNegocio()
        {
            _atendimentoRepositorio = new AtendimentoRepositorio();
        }

        /// <summary>
        /// Seleciona todos os Atendimentos do Database.
        /// </summary>
        /// <returns>Lista de atendimentos.</returns>
        public IEnumerable<Atendimento> Selecionar()
        {
            return _atendimentoRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe algum atendimento com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um atendimento no Database.</param>
        /// <returns>Seleciona um atendimento ou gera uma exceção.</returns>
        public Atendimento SelecionarPorId(int id)
        {
            var obj = _atendimentoRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nehum atendimento com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se o ID do médico e da clínica são válidos e se os campos foram preenchidos.
        /// Antes de inserir os dados do atendimento no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do atendimento.</param>
        /// <returns>ID do atendimento inserido no Databse ou gera uma exceção.</returns>
        public int Inserir(Atendimento entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }
            
            //Verifica se o ID do médico é válido.
            var RepositorioMedico = new MedicoRepositorio();
            if (RepositorioMedico.SelecionarPorId(entity.IdMedico) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum médico " +
                                                $"com o ID: {entity.IdMedico}");
            }

            //Verifica se o ID da clínica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma clínica " +
                                                $"com o ID: {entity.IdClinica}");
            }

            return _atendimentoRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o ID do médico e da clínica são válidos e se os campos foram preenchidos.
        /// Antes de alterar os dados do atendimento no Database.
        /// </summary>
        /// <param name="id">Usado para buscar um atendimento no Database.</param>
        /// <param name="entity">Objeto com os dados do atendimento.</param>
        /// <returns>Seleciona o atendimento alterado no Databse ou gera uma exceção.</returns>
        public Atendimento Alterar(int id, Atendimento entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se o ID do médico é válido.
            var RepositorioMedico = new MedicoRepositorio();
            if (RepositorioMedico.SelecionarPorId(entity.IdMedico) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum médico " +
                                                $"com o ID: {entity.IdMedico}");
            }

            //Verifica se o ID da clínica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma clínica " +
                                                $"com o ID: {entity.IdMedico}");
            }

            entity.Id = id;
            _atendimentoRepositorio.Alterar(entity);

            return _atendimentoRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o atendimento existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o atendimento no Database.</param>
        public void Deletar(int id)
        {
            var obj = _atendimentoRepositorio.SelecionarPorId(id);

            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: {id}, não foi encontrado");
            }
                
            _atendimentoRepositorio.Deletar(id);
        }
    }
}
