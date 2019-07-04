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
    [RoutePrefix("api/Estudiante")]
    public class EstudianteController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetEstudiantes()
        {
            return Ok(ADO_Alumnos.Obtener_Todo());
        }

        [HttpPost]
        [Route("Insert")]
        public IHttpActionResult InsertEstudiante(Alumno e)
        {
            if (e == null)
                return BadRequest("Error");

            Alumno es = ADO_Alumnos.Insertar(e);

            if (es != null)
                return Ok(es);
            else
                return BadRequest("Error");
        }

        [HttpGet]
        [Route("GetID")]
        public IHttpActionResult GetAlumnosID(int ID)
        {
            return Ok(ADO_Alumnos.GetAlumno(ID));
        }

        [HttpGet]
        [Route("GetID")]
        public IHttpActionResult GetAlumnosID()
        {
            var headers = Request.Headers;
            if (headers.Contains("ID"))
            {
                return Ok(ADO_Alumnos.GetAlumno(Convert.ToInt32(headers.GetValues("ID").First())));
            }
            return BadRequest("Error");
        }
    }
}
