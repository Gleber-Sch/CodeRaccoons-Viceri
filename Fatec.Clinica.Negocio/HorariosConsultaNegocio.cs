using System;
using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o HorariosConsulta
    /// </summary>
    public class HorariosConsultaNegocio : INegocioBase<HorariosConsulta>
    {
        /// <summary>
        /// Declara o repositório do HorariosConsulta.
        /// </summary>
        private readonly HorariosConsultaRepositorio _horariosConsultaRepositorio;

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public HorariosConsultaNegocio()
        {
            _horariosConsultaRepositorio = new HorariosConsultaRepositorio();
        }

        /// <summary>
        /// Seleciona todas os horarios consulta.
        /// </summary>
        /// <returns>Lista de Consultas.</returns>
        public IEnumerable<HorariosConsulta> Selecionar()
        {
            return _horariosConsultaRepositorio.Selecionar();
        }


        /// <summary>
        /// Verifica se existe algum HorarioConsulta com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioConsulta no Database.</param>
        /// <returns>Seleciona um HorarioConsulta ou gera uma exceção.</returns>
        public HorariosConsulta SelecionarPorId(int id)
        {
            var obj = _horariosConsultaRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado um Horario de Consulta com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se existe algum HorarioConsulta com o Dia indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioConsulta no Database.</param>
        /// <returns>Seleciona um HorarioConsulta ou gera uma exceção.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorDia(DateTime DiaHora)
        {
            var lista = _horariosConsultaRepositorio.SelecionarPorDia(DiaHora);

            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Consulta com o seguinte dia: {DiaHora.ToShortDateString()}");

            return lista;
        }

        /// <summary>
        /// Verifica se existe algum HorarioConsulta com a Hora indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioConsulta no Database.</param>
        /// <returns>Seleciona um HorarioConsulta ou gera uma exceção.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorHora(DateTime DiaHora)
        {
            var lista = _horariosConsultaRepositorio.SelecionarPorHora(DiaHora);

            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Consulta com o seguinte dia: {DiaHora.ToShortDateString()}");

            return lista;
        }



        /// <summary>
        /// Verifica se existe algum HorarioConsulta com a Hora indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioConsulta no Database.</param>
        /// <returns>Seleciona um HorarioConsulta ou gera uma exceção.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorValor(Decimal Valor)
        {
            var lista = _horariosConsultaRepositorio.SelecionarPorValor(Valor);

            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Consulta com o seguinte Valor: {Valor}");

            return lista;
        }

        /// <summary>
        /// Verifica se existe algum HorarioConsulta com o IDAtendimento indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioConsulta no Database.</param>
        /// <returns>Seleciona um HorarioConsulta ou gera uma exceção.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorIdAtendimento(int IdAtendimento)
        {
            var lista = _horariosConsultaRepositorio.SelecionarPorIdAtendimento(IdAtendimento);


            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Consulta com o seguinte IdAtendimento: {IdAtendimento}");


            return lista;
        }

        /// <summary>
        /// Verifica se existe algum HorarioConsulta com o NomeClinica indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioConsulta no Database.</param>
        /// <returns>Seleciona um HorarioConsulta ou gera uma exceção.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorNomeClinica(string NomeClinica)
        {
            var lista = _horariosConsultaRepositorio.SelecionarPorNomeClinica(NomeClinica);


            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Consulta com o seguinte NomeClinica: '{NomeClinica}'");

            return lista;
        }

        /// <summary>
        /// Verifica o horario ja existe um horario cadastrado no mesmo dia e horario, se o Dia e o horario é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do médico.</param>
        /// <returns>ID do médico inserido no Database ou gera alguma exceção.</returns>
        public int Inserir(HorariosConsulta entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }
            List<HorariosConsulta> obj = _horariosConsultaRepositorio.SelecionarPorDiaNegocio(entity.DiaHora);
            if (obj.Count >0)
            {
                var Obj = _horariosConsultaRepositorio.SelecionarPorHoraNegocio(entity.DiaHora);
                if (Obj.Count>0)
                    throw new ConflitoException("O horario ja foi marcado como Disponivel!");
            }

            return _horariosConsultaRepositorio.Inserir(entity);
        }


        /// <summary>
        /// Verifica o horario ja existe um horario cadastrado no mesmo dia e horario, se o Dia e o horario é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do médico.</param>
        /// <returns>Seleciona um HorarioConsulta do Database ou gera alguma exceção.</returns>
        public HorariosConsulta Alterar(int id, HorariosConsulta entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            List<HorariosConsulta> obj = _horariosConsultaRepositorio.SelecionarPorDiaNegocio(entity.DiaHora);
            if (obj.Count > 0)
            {
                var Obj = _horariosConsultaRepositorio.SelecionarPorHoraNegocio(entity.DiaHora);
                if (Obj.Count > 0)
                    throw new ConflitoException("O horario ja foi marcado como Disponivel!");
            }

            entity.Id = id;
            _horariosConsultaRepositorio.Alterar(entity);
            return _horariosConsultaRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o Horario de Consulta existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o Horario de Consulta no Database.</param>
        public void Deletar(int id)
        {
            //Verifica se o ID do médico existe.
            var obj = SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");
            }

            _horariosConsultaRepositorio.Deletar(obj.Id);
        }

    }
}