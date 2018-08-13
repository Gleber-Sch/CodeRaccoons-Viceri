using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fatec.Clinica.Negocio;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dado;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Fatec.Clinica.Api.Model;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/HorariosConsulta")]
    public class HorariosConsultaController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio do Horarios de Consulta.
        /// </summary>
        private HorariosConsultaNegocio _horariosConsultaNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public HorariosConsultaController()
        {
            _horariosConsultaNegocio = new HorariosConsultaNegocio();
        }

        /// <summary>
        /// Método que obtêm uma lista com todos os Horarios de Consulta.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_horariosConsultaNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um Horario de Consulta.
        /// </summary>
        /// <param name="id">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "HorariosConsultaGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_horariosConsultaNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que seleciona um Horario de Consulta.
        /// </summary>
        /// <param name="dia">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Dia/{dia}", Name = "HorariosConsultaGetDia")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetDia(DateTime dia)
        {
            return Ok(_horariosConsultaNegocio.SelecionarPorDia(dia));
        }

        /// <summary>
        /// Método que seleciona um Horario de Consulta.
        /// </summary>
        /// <param name="hora">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Hora/{hora}", Name = "HorariosConsultaGetHora")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetHora(DateTime hora)
        {
            return Ok(_horariosConsultaNegocio.SelecionarPorHora(hora));
        }

        /// <summary>
        /// Método que seleciona um Horario de Consulta.
        /// </summary>
        /// <param name="valor">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Valor/{valor}", Name = "HorariosConsultaGetValor")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetValor(Decimal valor)
        {
            return Ok(_horariosConsultaNegocio.SelecionarPorValor(valor));
        }

        /// <summary>
        /// Método que seleciona um Horario de Consulta.
        /// </summary>
        /// <param name="id">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Atendimento/{id}", Name = "HorariosConsultaGetIdAtendimento")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetIdAtendimento(int id)
        {
            return Ok(_horariosConsultaNegocio.SelecionarPorIdAtendimento(id));
        }

        /// <summary>
        /// Método que seleciona um Horario de Consulta.
        /// </summary>
        /// <param name="nome">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Clinica/{nome}", Name = "HorariosConsultaGetNomeClinica")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetNomeClinica(string nome)
        {
            return Ok(_horariosConsultaNegocio.SelecionarPorNomeClinica(nome));
        }

        /// <summary>
        /// Método que insere um novo Horario de Consulta.
        /// </summary>
        /// <param name="input">Objeto com os dados do Horario de Consulta.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(HorariosConsulta), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] HorariosConsultaInput input)
        {
            var obj = new HorariosConsulta()
            {
                DiaHora = input.DiaHora,
                Valor = input.Valor,
                IdAtendimento = input.IdAtendimento
            };

            var idHorarioConsulta = _horariosConsultaNegocio.Inserir(obj);
            obj.Id = idHorarioConsulta;
            return CreatedAtRoute("AtendimentoGetId", new { id = idHorarioConsulta }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um Horario de Consulta.
        /// </summary>
        /// <param name="id">Usado para buscar o Horario.</param>
        /// <param name="input">Objeto que contêm os dados a serem alteradas.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(HorariosConsulta), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]HorariosConsultaInput input)
        {
            var obj = new HorariosConsulta()
            {
                DiaHora = input.DiaHora,
                Valor = input.Valor,
                IdAtendimento = input.IdAtendimento
            };
            var Obj = _horariosConsultaNegocio.Alterar(id, obj);
            return Accepted(Obj);
        }

        /// <summary>
        /// Método que deleta um Horario de Consulta
        /// </summary>
        /// <param name="id">Usado para buscar o Horario de Consulta.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _horariosConsultaNegocio.Deletar(id);
            return Ok();
        }
    }
}