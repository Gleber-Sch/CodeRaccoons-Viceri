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
    [Route("api/Exame")]
    public class ExameController : Controller
    {
        /// <summary>
        /// Contêm as regras de negócio do Exame. 
        /// </summary>
        private ExameNegocio _exameNegocio;

        /// <summary>
        /// Construtor para instaciar as regras de negócio do Exame.
        /// </summary>
        public ExameController()
        {
            _exameNegocio = new ExameNegocio();
        }

        /// <summary>
        /// Método que obtêm todos os exames.
        /// </summary>
        /// <returns>Todos os exames registrados.</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_exameNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um exame.
        /// </summary>
        /// <param name="id">Usado para buscar o exame.</param>
        /// <returns>Exame selecionado.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_exameNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames de um paciente.
        /// </summary>
        /// <param name="id">Usado para buscar o paciente.</param>
        /// <returns>Todos os exames selecionado.</returns>
        [HttpGet]
        [Route("Paciente/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetPaciente(int id)
        {
            return Ok(_exameNegocio.SelecionarPorPaciente(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames solicitados por um médico.
        /// </summary>
        /// <param name="id">Usado para buscar o médico.</param>
        /// <returns>Todos os exames selecionado.</returns>
        [HttpGet]
        [Route("Solicitados/Medico/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetMedicoQueSolicitou(int id)
        {
            return Ok(_exameNegocio.SelecionarPorMedicoQueSolicitou(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames realizados por um médico.
        /// </summary>
        /// <param name="id">Usado para buscar o médico.</param>
        /// <returns>Todos os exames selecionado.</returns>
        [HttpGet]
        [Route("Realizados/Medico/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetMedicoQueRealizou(int id)
        {
            return Ok(_exameNegocio.SelecionarPorMedicoQueRealizou(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames realizados numa clínica.
        /// </summary>
        /// <param name="id">Usado para buscar a clínica.</param>
        /// <returns>Todos os exames selecionado.</returns>
        [HttpGet]
        [Route("Realizados/Clinica/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetClinica(int id)
        {
            return Ok(_exameNegocio.SelecionarPorClinica(id));
        }

        /// <summary>
        /// Método que insere um exame.
        /// </summary>
        /// <param name="input">Objeto com os dados do exame.</param>
        /// <returns>ID criado para o exame inserido.</returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Exame), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]ExameInput input)
        {
            var obj = new Exame()
            {
                DataHora = input.DataHora,
                IdAtendimento = input.IdAtendimento,
                IdConsulta = input.IdConsulta,
                IdTipoExame = input.IdTipoExame
            };

            var idExame = _exameNegocio.Inserir(obj);
            obj.Id = idExame;
            return CreatedAtRoute("GetId", new { id = idExame }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um endreço.
        /// </summary>
        /// <param name="id">Usado para buscar o endereço.</param>
        /// <param name="input">Objeto com os dados do endereço.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Exame), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]ExameInput input)
        {
            var obj = new Exame()
            {
                DataHora = input.DataHora,
                IdAtendimento = input.IdAtendimento,
                IdConsulta = input.IdConsulta,
                IdTipoExame = input.IdTipoExame
            };

            var objReturn = _exameNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// Método que deleta um exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o exame a ser deletado.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _exameNegocio.Deletar(id);
            return Ok();
        }
    }
}