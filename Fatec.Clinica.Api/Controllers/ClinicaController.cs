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
    [Route("api/Clinica")]
    public class ClinicaController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio da Clínica. 
        /// </summary>
        private ClinicaNegocio _clinicaNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public ClinicaController()
        {
            _clinicaNegocio = new ClinicaNegocio();
        }

        /// <summary>
        /// Método que obtêm todos as clínicas.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Clinicas), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_clinicaNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm uma clínica.
        /// </summary>
        /// <param name="id">Usado para selecionar a clínica.</param>
        /// <returns></returns>
        /// <remarks>Obtêm uma clíca pelo Id da clínica.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "ClinicaGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Clinicas), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_clinicaNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm uma clínica através de um CNPJ.
        /// </summary>
        /// <param name="cnpj">Usado para selecionar a clínica.</param>
        /// <returns></returns>
        /// <remarks>Obtêm uma clíca pelo CNPJ da clínica.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("CNPJ/{cnpj}", Name = "ClinicaGetCnpj")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Clinicas), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get(string cnpj)
        {
            return Ok(_clinicaNegocio.SelecionarPorCnpj(cnpj));
        }

        // <summary>
        /// Método que obtêm o Id da Clínica para realizar o Login.
        /// </summary>
        /// <param name="email">Usado para selecionar a clínica.</param>
        /// <param name="senha">Usado para selecionar a clínica.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um paciente pelo Id do paciente.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Login/{senha}/{email}", Name = "ClinicaGetLogin")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Clinicas), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetLogin(string email, string senha)
        {
            return Ok(_clinicaNegocio.Login(email, senha));
        }

        /// <summary>
        /// Método que insere uma clínica.
        /// </summary>
        /// <param name="input">Objeto com os dados da clínica.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Clinicas), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]ClinicaInput input)
        {
            var obj = new Clinicas()
            {
                Email = input.Email,
                Senha = input.Senha,
                Nome = input.Nome,
                Cnpj = input.Cnpj,
                TelefoneCom = input.TelefoneCom,
                StatusAtividade = true,
                IdEndereco = input.IdEndereco
            };

            var idClinica = _clinicaNegocio.Inserir(obj);
            obj.Id = idClinica;
            return CreatedAtRoute("ClinicaGetId", new { id = idClinica }, obj);
        }

        /// <summary>
        /// Método que altera os dados de uma clínica.
        /// </summary>
        /// <param name="id">Usado para selecionar a clínica.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Clinicas), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]ClinicaInput input)
        {
            var obj = new Clinicas()
            {
                Email = input.Email,
                Senha = input.Senha,
                Nome = input.Nome,
                Cnpj = input.Cnpj,
                TelefoneCom = input.TelefoneCom,
                StatusAtividade = input.StatusAtividade,
                IdEndereco = input.IdEndereco
            };

            var objReturn = _clinicaNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// Método que deleta uma clínica.
        /// </summary>
        /// <param name="id">Usado para selecionar a clínica.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _clinicaNegocio.Deletar(id);
            return Ok();
        }

    }
}