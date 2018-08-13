using Microsoft.AspNetCore.Mvc;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;


namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Paciente")]
    public class PacienteController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio do Paciente.
        /// </summary>
        private PacienteNegocio _pacienteNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public PacienteController()
        {
            _pacienteNegocio = new PacienteNegocio();
        }

        /// <summary>
        /// Método que obtêm todos os Pacientes.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Paciente), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_pacienteNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um Paciente.
        /// </summary>
        /// <param name="id">Usado para selecionar o paciente.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um paciente pelo Id do paciente.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name ="PacienteGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Paciente), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_pacienteNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm um Paciente.
        /// </summary>
        /// <param name="cpf">Usado para selecionar o paciente.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um paciente pelo Cpf do paciente.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Cpf/{cpf}", Name = "PacienteGetCpf")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Paciente), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetCpf(string cpf)
        {
            return Ok(_pacienteNegocio.SelecionarPorCpf(cpf));
        }

        /// <summary>
        /// Método que insere um paciente.
        /// </summary>
        /// <param name="input">Objeto com os dados do paciente.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Paciente), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] PacienteInput input)
        {
            var obj = new Paciente()
            {
                Nome = input.Nome,
                Cpf = input.Cpf,
                Celular = input.Celular,
                DataNasc = input.DataNasc,
                Email = input.Email,
                Genero = input.Genero,
                Senha = input.Senha,
                TelefoneRes = input.TelefoneRes
            };

            var idPaciente = _pacienteNegocio.Inserir(obj);
            obj.Id = idPaciente;
            return CreatedAtRoute("PacienteGetId", new { id = idPaciente }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um paciente.
        /// </summary>
        /// <param name="id">Usado para selecionar o paciente.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Paciente), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]PacienteInput input)
        {
            var objPaciente = new Paciente()
            {
                Nome = input.Nome,
                Cpf = input.Cpf,
                Celular = input.Celular,
                DataNasc = input.DataNasc,
                Email = input.Email,
                Genero = input.Genero,
                Senha = input.Senha,
                TelefoneRes = input.TelefoneRes
            };

            var obj = _pacienteNegocio.Alterar(id, objPaciente);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um paciente.
        /// </summary>
        /// <param name="id">Usado para selecionar o paciente.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _pacienteNegocio.Deletar(id);
            return Ok();
        }

    }
}