using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ValorConsulta")]
    public class ValorConsultaController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio do ValorConsulta.
        /// </summary>
        private ValorConsultaNegocio _valorConsultaNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public ValorConsultaController()
        {
            _valorConsultaNegocio = new ValorConsultaNegocio();
        }

        /// <summary>
        /// Método que obtêm todos os valores de consultas.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ValorConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_valorConsultaNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um valor de consulta.
        /// </summary>
        /// <param name="id">Usado para selecionar o valor de uma consulta.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um atendimento pelo Id do valor da consulta.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "ValorConsultaGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ValorConsulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_valorConsultaNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere um valor de consulta.
        /// </summary>
        /// <param name="input">Objeto com os dados do valor de consulta.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(ValorConsulta), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] ValorConsultaInput input)
        {
            var obj = new ValorConsulta()
            {
                IdClinica = input.IdClinica,
                IdEspecialidade = input.IdEspecialidade,
                Valor = input.Valor
            };

            var idValorConsulta = _valorConsultaNegocio.Inserir(obj);
            obj.Id = idValorConsulta;
            return CreatedAtRoute("ValorConsultaGetId", new { id = idValorConsulta }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um valor de consulta.
        /// </summary>
        /// <param name="id">Usado para selecionar o valor da consulta.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(ValorConsulta), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody] ValorConsultaInput input)
        {
            var objAtendimento = new ValorConsulta()
            {
                IdClinica = input.IdClinica,
                IdEspecialidade = input.IdEspecialidade,
                Valor = input.Valor
            };
            var obj = _valorConsultaNegocio.Alterar(id, objAtendimento);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um valor de consulta.
        /// </summary>
        /// <param name="id">Usado para selecionar o valor de consulta.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _valorConsultaNegocio.Deletar(id);
            return Ok();
        }
    }
}