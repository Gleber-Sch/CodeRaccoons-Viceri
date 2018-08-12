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
        private PacienteNegocio _pacienteNegocio;

        public PacienteController()
        {
            _pacienteNegocio = new PacienteNegocio();
        }

        /// <summary>
        /// Método que obtem uma lista de pacientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Paciente), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_pacienteNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um paciente por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name ="PacienteGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Paciente), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_pacienteNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que seleciona um paciente por Cpf.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cpf/{cpf}", Name = "PacienteGetCpf")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Paciente), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetCpf(string cpf)
        {
            return Ok(_pacienteNegocio.SelecionarPorCpf(cpf));
        }

        /// <summary>
        /// Método que insere um novo paciente
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// Método que altera os dados de um paciente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
                Email = input.Email,
                Genero = input.Genero,
                Senha = input.Senha,
                TelefoneRes = input.TelefoneRes
            };

            var obj = _pacienteNegocio.Alterar(id, objPaciente);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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