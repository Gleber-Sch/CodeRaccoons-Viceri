using Fatec.Clinica.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Horarios.Negocio
{

    /// <summary>
    /// Regras de Negócio sobre os Horarios.
    /// </summary>
    public class HorariosNegocio : INegocioBase<Horario>
    {
        private readonly HorariosRepositorio _horariosRepositorio;

        /// <summary>
        /// Construtor para instaciar o repositório.
        /// </summary>
        public HorariosNegocio()
        {
            _horariosRepositorio = new HorariosRepositorio();
        }

        /// <summary>
        /// Seleciona todos os Horarios.
        /// </summary>
        /// <returns>Lista de Exames.</returns>
        public IEnumerable<Horario> Selecionar()
        {
            return _horariosRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se o Horario com o ID indicado existe.
        /// </summary>
        /// <param name="id">Usado para buscar Um Horario no Database.</param>
        /// <returns>Seleciona um Horario ou gera uma exceção.</returns>
        public Horario SelecionarPorId(int id)
        {
            var obj = _horariosRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum Horario com este ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se o Horario com o Dia indicado existe.
        /// </summary>
        /// <param name="id">Usado para buscar Um Horario no Database.</param>
        /// <returns>Seleciona um Horario ou gera uma exceção.</returns>
        public Horario SelecionarPorDia(DateTime DiaHora)
        {
            var obj = _horariosRepositorio.SelecionarPorDia(DiaHora);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum Horario com este Dia: {DiaHora.Day}");

            return obj;
        }

        /// <summary>
        /// Verifica se o Horario com a hora indicada existe.
        /// </summary>
        /// <param name="id">Usado para buscar Um Horario no Database.</param>
        /// <returns>Seleciona um Horario ou gera uma exceção.</returns>
        public Horario SelecionarPorHorario(DateTime DiaHora)
        {
            var obj = _horariosRepositorio.SelecionarPorHorario(DiaHora);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum Horario Com o informado: {DiaHora.Hour}");

            return obj;

        }

        /// <summary>
        /// Verifica se o Horario com a hora indicada existe.
        /// </summary>
        /// <param name="id">Usado para buscar Um Horario no Database.</param>
        /// <returns>Seleciona um Horario ou gera uma exceção.</returns>
        public Horario SelecionarPorClinica(int Id)
        {
            var obj = _horariosRepositorio.SelecionarPorClinica(Id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhum Horario com este Dia: {Id}");

            return obj;

        }


        /// <summary>
        ///Verifica se o horario não está cadastrado e se existem campos obrigatórios.
        /// sem serem preenchidos. Antes de inserir uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados do Horario a ser inserida.</param>
        /// <returns>ID da clínica inserida no Database ou exceção.</returns>
        public int Inserir(Horario entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Os seguintes campos são obrigatórios: Nome, Cnpj e Telefone Comercial");
            }


            var obj = _horariosRepositorio.SelecionarPorDia(entity.DiaHora);
            Boolean ValidacaoDia = false;

            if (obj != null)
            {
                ValidacaoDia = true;
            }

            var Obj = _horariosRepositorio.SelecionarPorHorario(entity.DiaHora);

            if (Obj != null && ValidacaoDia == true)
            {
                throw new ConflitoException("Ja existe um Horario cadastrado para o Horario cadastrado");
            }


            return _horariosRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o horario já não está cadastrado e se ele é válido e se existem campos obrigatórios
        /// sem serem preenchidos. Antes de inserir uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados do Horario a ser alterado.</param>
        /// <returns>ID do horario inserida no Database ou exceção.</returns>
        public Horario Alterar(int id, Horario entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Os seguintes campos são obrigatórios: Nome, Cnpj e Telefone Comercial");
            }

            var obj = _horariosRepositorio.SelecionarPorDia(entity.DiaHora);
            Boolean ValidacaoDia = false;

            if (obj != null)
            {
                ValidacaoDia = true;
            }

            var Obj = _horariosRepositorio.SelecionarPorHorario(entity.DiaHora);

            if (Obj != null && ValidacaoDia == true)
            {
                throw new ConflitoException("Ja existe um Horario cadastrado para o Horario cadastrado");
            }
            _horariosRepositorio.Alterar(entity);

            return _horariosRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o horario existe no Database antes de deleta-la.
        /// </summary>
        /// <param name="id">Usado para buscar o horario no Database.</param>
        public void Deletar(int id)
        {
            var obj = _horariosRepositorio.SelecionarPorId(id);

            if (obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado nenhum horario com este ID: {id}");
            }
            _horariosRepositorio.Deletar(obj.Id);
        }

        /// <summary>
        /// Verifica se o horario existe no Database antes de deleta-la.
        /// </summary>
        /// <param name="id">Usado para buscar o horario no Database.</param>
        public void Deletar(DateTime DiaHora)
        {
            var obj = _horariosRepositorio.SelecionarPorDia(DiaHora);
            var Obj = _horariosRepositorio.SelecionarPorHorario(DiaHora);

            if (obj == null && Obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado nenhum horario com o Dia: {DiaHora.Day} e horario: {DiaHora.Hour}");
            }
            _horariosRepositorio.Deletar(DiaHora);
        }


    }
}
