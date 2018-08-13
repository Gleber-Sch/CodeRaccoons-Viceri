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
    [Route("api/HorariosExame")]
    public class HorariosExameController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio do Horarios de Consulta.
        /// </summary>
        private HorariosExameNegocio _horariosExameNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public HorariosExameController()
        {
            _horariosExameNegocio = new HorariosExameNegocio();
        }

        /// <summary>
        /// Método que obtêm uma lista com todos os Horarios de Exame.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_horariosExameNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um Horario de Exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "HorariosExameGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_horariosExameNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que seleciona um Horario de Exame.
        /// </summary>
        /// <param name="dia">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Dia/{dia}", Name = "HorariosExameGetDia")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetDia(DateTime dia)
        {
            return Ok(_horariosExameNegocio.SelecionarPorDia(dia));
        }

        /// <summary>
        /// Método que seleciona um Horario de Exame.
        /// </summary>
        /// <param name="hora">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Hora/{hora}", Name = "HorariosExameGetHora")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetHora(DateTime hora)
        {
            return Ok(_horariosExameNegocio.SelecionarPorHora(hora));
        }

        /// <summary>
        /// Método que seleciona um Horario de Exame.
        /// </summary>
        /// <param name="valor">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Valor/{valor}", Name = "HorariosExameGetValor")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetValor(Decimal valor)
        {
            return Ok(_horariosExameNegocio.SelecionarPorValor(valor));
        }

        /// <summary>
        /// Método que seleciona um Horario de Exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Atendimento/{id}", Name = "HorariosExameGetIdAtendimento")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetIdAtendimento(int id)
        {
            return Ok(_horariosExameNegocio.SelecionarPorIdAtendimento(id));
        }

        /// <summary>
        /// Método que seleciona um Horario de Exame.
        /// </summary>
        /// <param name="nome">Usado para selecionar o Horario.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Clinica/{nome}", Name = "HorariosExameGetNomeClinica")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(HorariosExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetNomeClinica(string nome)
        {
            return Ok(_horariosExameNegocio.SelecionarPorNomeClinica(nome));
        }

        /// <summary>
        /// Método que insere um novo Horario de Exame.
        /// </summary>
        /// <param name="input">Objeto com os dados do Horario de Exame.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(HorariosExame), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] HorariosExameInput input)
        {
            var obj = new HorariosExame()
            {
                DiaHora = input.DiaHora,
                Valor = input.Valor,
                IdAtendimento = input.IdAtendimento,
                IdTipoExame = input.IdTipoExame
            };

            var idHorarioExame = _horariosExameNegocio.Inserir(obj);
            obj.Id = idHorarioExame;
            return CreatedAtRoute("HorariosExameGetId", new { id = idHorarioExame }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um Horario de Exame.
        /// </summary>
        /// <param name="id">Usado para buscar o Horario.</param>
        /// <param name="input">Objeto que contêm os dados a serem alteradas.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(HorariosExame), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]HorariosExameInput input)
        {
            var obj = new HorariosExame()
            {
                DiaHora = input.DiaHora,
                Valor = input.Valor,
                IdAtendimento = input.IdAtendimento,
                IdTipoExame = input.IdTipoExame
            };
            var Obj = _horariosExameNegocio.Alterar(id, obj);
            return Accepted(Obj);
        }


        /// <summary>
        /// Método que deleta um Horario de Exame
        /// </summary>
        /// <param name="id">Usado para buscar o Horario de Exame.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _horariosExameNegocio.Deletar(id);
            return Ok();
        }

    }
}
