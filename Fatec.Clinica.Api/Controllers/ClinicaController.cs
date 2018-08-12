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
        /// Contêm as regras de negócio da Clínica. 
        /// </summary>
        private ClinicaNegocio _clinicaNegocio;

        /// <summary>
        /// Construtor para instaciar as regras de negócio da Clínica.
        /// </summary>
        public ClinicaController()
        {
            _clinicaNegocio = new ClinicaNegocio();
        }

        /// <summary>
        /// Método que obtêm todos as clínicas.
        /// </summary>
        /// <returns>Todos as clínicas registradas.</returns>
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
        /// <param name="id">Usado para buscar a clínica.</param>
        /// <returns>Clínica selecionada.</returns>
        [HttpGet]
        [Route("{id}", Name = "ClinicaGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Clinicas), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_clinicaNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm uma clínica.
        /// </summary>
        /// <remarks>Obtêm uma clína através de um cnpj.</remarks>
        /// <param name="cnpj">Usado para buscar a clínica.</param>
        /// <returns>Clínica selecionada.</returns>
        [HttpGet]
        [Route("{cnpj}", Name = "ClinicaGetCnpj")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Clinicas), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get(string cnpj)
        {
            return Ok(_clinicaNegocio.SelecionarPorCnpj(cnpj));
        }

        /// <summary>
        /// Método que insere uma clínica.
        /// </summary>
        /// <param name="input">Objeto com os dados da clínica.</param>
        /// <returns>ID criado para a clínica inserido.</returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Clinicas), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]ClinicaInput input)
        {
            var obj = new Clinicas()
            {
                Email = input.Email,
                Nome = input.Nome,
                Cnpj = input.Cnpj,
                TelefoneCom = input.TelefoneCom,
                StatusAtividade = true
            };

            var idClinica = _clinicaNegocio.Inserir(obj);
            obj.Id = idClinica;
            return CreatedAtRoute("ClinicaGetId", new { id = idClinica }, obj);
        }

        /// <summary>
        /// Método que altera os dados de uma clínica.
        /// </summary>
        /// <param name="id">Usado para buscar a clínica.</param>
        /// <param name="input">Objeto com os dados da clínica.</param>
        /// <returns></returns>
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
                Nome = input.Nome,
                Cnpj = input.Cnpj,
                TelefoneCom = input.TelefoneCom,
                StatusAtividade = input.StatusAtividade
            };

            var objReturn = _clinicaNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// Método que deleta uma clínica.
        /// </summary>
        /// <param name="id">Usado para selecionar a clínica a ser deletado.</param>
        /// <returns></returns>
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