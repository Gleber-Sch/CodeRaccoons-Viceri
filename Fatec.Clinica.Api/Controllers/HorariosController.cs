using Microsoft.AspNetCore.Mvc;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using Fatec.Horarios.Negocio;
using System;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Horario")]
    public class HorariosController : Controller
    {
        private HorariosNegocio _horariosNegocio;

        public HorariosController()
        {
           HorariosNegocio _horariosNegocio = new HorariosNegocio();
        }

        /// <summary>
        /// Método que obtêm todos os Horarios.
        /// </summary>
        /// <returns>Todos os Horarios registrados.</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Horario), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_horariosNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um Horario.
        /// </summary>
        /// <param name="id">Usado para buscar o Horario.</param>
        /// <returns>Horario selecionado.</returns>
        [HttpGet]
        [Route("{id}", Name = "HorarioGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Horario), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_horariosNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm todos os horarios de uma Clinica do dia informado.
        /// </summary>
        /// <param name="id">Usado para buscar o Horario.</param>
        /// <returns>Todos os Horarios selecionado.</returns>
        [HttpGet]
        [Route("{Dia}", Name = "HorariosGetDia")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Horario), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetHorarioDia(DateTime DiaHora)
        {
            return Ok(_horariosNegocio.SelecionarPorDia(DiaHora));
        }

        /// <summary>
        /// Método que obtêm os horarios de uma clinica apartir da hora informada.
        /// </summary>
        /// <param name="id">Usado para buscar o horario.</param>
        /// <returns>Todos os horarios selecionado.</returns>
        [HttpGet]
        [Route("{Hora}", Name = "HorariosGetHora")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Horario), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetHorarioHora(DateTime DiaHora)
        {
            return Ok(_horariosNegocio.SelecionarPorHorario(DiaHora));
        }

        /// <summary>
        /// Método que obtêm todos os horarios de uma clinica.
        /// </summary>
        /// <param name="id">Usado para buscar o horario.</param>
        /// <returns>Todos os horarios selecionado.</returns>
        [HttpGet]
        [Route("{IdClinica}", Name = "HorariosGetClinica")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Horario), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetHorarioClinica(int Id)
        {
            return Ok(_horariosNegocio.SelecionarPorClinica(Id));
        }

        /// <summary>
        /// Método que insere um exame.
        /// </summary>
        /// <param name="input">Objeto com os dados do exame.</param>
        /// <returns>ID criado para o exame inserido.</returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Horario), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]HorarioInput input)
        {
            var obj = new Horario()
            {
                DiaHora = input.DiaHora,
                IdClinica = input.IdCLinica
            };

            var idHorario = _horariosNegocio.Inserir(obj);
            obj.Id = idHorario;
            return CreatedAtRoute("HorarioGetId", new { id = idHorario }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um horario.
        /// </summary>
        /// <param name="id">Usado para buscar o horario.</param>
        /// <param name="input">Objeto com os dados do horario.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Horario), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]HorarioInput input)
        {
            var obj = new Horario()
            {
                DiaHora= input.DiaHora,
                IdClinica = input.IdCLinica

            };

            var objReturn = _horariosNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id para selecionar o horario a ser removido</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _horariosNegocio.Deletar(id);
            return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DiaHora">datetime para selecionar o dia e o horario a ser removido</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]DateTime DiaHora)
        {
            _horariosNegocio.Deletar(DiaHora);
            return Ok();
        }
    }

}
