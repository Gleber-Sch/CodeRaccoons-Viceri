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
    /// Regras de Negócio sobre o HorariosNegocio
    /// </summary>
    public class HorariosExameNegocio : INegocioBase<HorariosExame>
    {
        /// <summary>
        /// Declara o repositório do HorariosExame.
        /// </summary>
        private readonly HorariosExameRepositorio _horariosExameRepositorio;

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public HorariosExameNegocio()
        {
            _horariosExameRepositorio = new HorariosExameRepositorio();
        }


        /// <summary>
        /// Seleciona todas os horarios Exame.
        /// </summary>
        /// <returns>Lista de Exames.</returns>
        public IEnumerable<HorariosExame> Selecionar()
        {
            return _horariosExameRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe algum HorarioExame com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioExame no Database.</param>
        /// <returns>Seleciona um HorarioExame ou gera uma exceção.</returns>
        public HorariosExame SelecionarPorId(int id)
        {
            var obj = _horariosExameRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado um horario de exame com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se existe algum HorarioConsulta com o Dia indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioConsulta no Database.</param>
        /// <returns>Seleciona um HorarioConsulta ou gera uma exceção.</returns>
        public IEnumerable<HorariosExame> SelecionarPorDia(DateTime DiaHora)
        {
            var lista = _horariosExameRepositorio.SelecionarPorDia(DiaHora);

            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Exame com o seguinte dia: {DiaHora.ToShortDateString()}");

            return lista;
        }

        /// <summary>
        /// Verifica se existe algum HorarioExame com a Hora indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioExame no Database.</param>
        /// <returns>Seleciona um HorarioExame ou gera uma exceção.</returns>
        public IEnumerable<HorariosExame> SelecionarPorHora(DateTime DiaHora)
        {
            var lista = _horariosExameRepositorio.SelecionarPorHora(DiaHora);

            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Exame com o seguinte dia: {DiaHora.ToShortDateString()}");

            return lista;
        }

        /// <summary>
        /// Verifica se existe algum HorarioExame com o valor indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioExame no Database.</param>
        /// <returns>Seleciona um HorarioExame ou gera uma exceção.</returns>
        public IEnumerable<HorariosExame> SelecionarPorValor(Decimal Valor)
        {
            var lista = _horariosExameRepositorio.SelecionarPorValor(Valor);

            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Exame com o seguinte Valor: {Valor}");

            return lista;
        }

        /// <summary>
        /// Verifica se existe algum HorarioExame com o IDAtendimento indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioExame no Database.</param>
        /// <returns>Seleciona um HorarioExame ou gera uma exceção.</returns>
        public IEnumerable<HorariosExame> SelecionarPorIdAtendimento(int IdAtendimento)
        {
            var lista = _horariosExameRepositorio.SelecionarPorIdAtendimento(IdAtendimento);


            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Exame com o seguinte IdAtendimento: {IdAtendimento}");


            return lista;
        }

        /// <summary>
        /// Verifica se existe algum HorarioExame com o NomeClinica indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um HorarioExame no Database.</param>
        /// <returns>Seleciona um HorarioExame ou gera uma exceção.</returns>
        public IEnumerable<HorariosExame> SelecionarPorNomeClinica(string NomeClinica)
        {
            var lista = _horariosExameRepositorio.SelecionarPorNomeClinica(NomeClinica);


            if (lista == null)
                throw new NaoEncontradoException($"Não foi encontrado Horario de Exame com o seguinte NomeClinica: '{NomeClinica}'");

            return lista;
        }

        /// <summary>
        /// Verifica se o horario ja existe um horario cadastrado no mesmo dia e horario, se o Dia e o horario é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do médico.</param>
        /// <returns>ID do médico inserido no Database ou gera alguma exceção.</returns>
        public int Inserir(HorariosExame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }
            List<HorariosExame> obj = _horariosExameRepositorio.SelecionarPorDiaNegocio(entity.DiaHora);
            
            if (obj.Count!=0)
            {
                List<HorariosExame> Obj = _horariosExameRepositorio.SelecionarPorHoraNegocio(entity.DiaHora);
                if (Obj.Count != 0)
                    throw new ConflitoException("O horario ja foi marcado como Disponivel!");
            }
            return _horariosExameRepositorio.Inserir(entity);

        }



        /// <summary>
        /// Verifica o horario ja existe um horario cadastrado no mesmo dia e horario, se o Dia e o horario é válido, se existem campos obrigatórios
        /// que não estão preenchidos, se os campos respeitam os limites de caracteres especificados no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do médico.</param>
        /// <returns>Seleciona um HorarioExame do Database ou gera alguma exceção.</returns>
        public HorariosExame Alterar(int id, HorariosExame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }
            List<HorariosExame> obj = _horariosExameRepositorio.SelecionarPorDiaNegocio(entity.DiaHora);

            if (obj.Count != 0)
            {
                List<HorariosExame> Obj = _horariosExameRepositorio.SelecionarPorHoraNegocio(entity.DiaHora);
                if (Obj.Count != 0)
                    throw new ConflitoException("O horario ja foi marcado como Disponivel!");
            }
            entity.Id = id;
            _horariosExameRepositorio.Alterar(entity);
            return _horariosExameRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o Horario de Exame existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o Horario de Exame no Database.</param>
        public void Deletar(int id)
        {
            //Verifica se o ID do médico existe.
            var obj = SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");
            }

            _horariosExameRepositorio.Deletar(obj.Id);
        }

    }
}