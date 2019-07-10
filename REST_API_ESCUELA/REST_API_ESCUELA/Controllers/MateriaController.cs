using REST_API_ESCUELA.ADO;
using REST_API_ESCUELA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST_API_ESCUELA.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Materia")]
    public class MateriaController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetMaterias()
        {
            return Ok(ADO_Materias.Obtener_Todo());
        }

        [HttpPost]
        [Route("Insert")]
        public IHttpActionResult InsertMateria(Materia m)
        {
            if (m == null)
                return BadRequest("Error");

            Materia ma = ADO_Materias.Insertar(m);

            if (ma != null)
                return Ok(ma);
            else
                return BadRequest("Error");
        }
    }
}
