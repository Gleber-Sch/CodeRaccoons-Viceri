using Microsoft.AspNetCore.Mvc;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;


namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Consulta")]
    public class ConsultaController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio da Consulta. 
        /// </summary>
        ConsultaNegocio _consultaNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public ConsultaController()
        {
            _consultaNegocio = new ConsultaNegocio();
        }

        /// <summary>
        /// Método que obtêm todas as consultas.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_consultaNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm uma consulta.
        /// </summary>
        /// <param name="id">Usado para selecionar a consulta.</param>
        /// <returns></returns>
        /// <remarks>Obtêm uma consulta pelo Id da consulta.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "ConsultaGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm todas as consultas agendadas ou realizadas por um paciente.
        /// </summary>
        /// <param name="id">Usado para selecionar as consultas.</param>
        /// <returns></returns>
        /// <remarks>Obtêm consultas pelo Id do paciente.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Paciente/{id}", Name = "ConsultaGetIdPaciente")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetPaciente(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorPaciente(id));
        }

        /// <summary>
        /// Método que obtêm todas as consultas agendadas ou realizadas com um médico.
        /// </summary>
        /// <param name="id">Usado para selecionar as consultas.</param>
        /// <returns></returns>
        /// <remarks>Obtêm consultas pelo Id do médico.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Medico/{id}", Name = "ConsultaGetIdMedico")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetMedico(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorMedico(id));
        }

        /// <summary>
        /// Método que obtêm todas as consultas agendadas ou realizadas numa clínica.
        /// </summary>
        /// <param name="id">Usado para selecionar as consultas.</param>
        /// <returns></returns>
        /// <remarks>Obtêm consultas pelo Id da clínica.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Clinica/{id}", Name = "ConsultaGetIdClinica")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetClinica(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorClinica(id));
        }

        /// <summary>
        /// Método que insere uma consulta.
        /// </summary>
        /// <param name="input">Objeto com os dados da consulta.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Consulta), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] ConsultaInput input)
        {
            var obj = new Consulta()
            {
                IdAtendimento = input.IdAtendimento,
                DataHora = input.DataHora,
                Historico = input.Historico,
                Nota = input.Nota,
                IdPaciente = input.IdPaciente
            };

            var idConsulta = _consultaNegocio.Inserir(obj);
            obj.Id = idConsulta;
            return CreatedAtRoute("ConsultaGetId", new { id = idConsulta }, obj);
        }

        /// <summary>
        /// Método que altera os dados de uma consulta.
        /// </summary>
        /// <param name="id">Usado para selecionar a consulta.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Consulta), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]ConsultaInput input)
        {
            var objConsulta = new Consulta()
            {
                IdAtendimento = input.IdAtendimento,
                DataHora = input.DataHora,
                Historico = input.Historico,
                Nota = input.Nota,
                IdPaciente = input.IdPaciente
            };

            var obj = _consultaNegocio.Alterar(id, objConsulta);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta uma clínica.
        /// </summary>
        /// <param name="id">Usado para selecionar o endereço.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _consultaNegocio.Deletar(id);
            return Ok();
        }
    }
}