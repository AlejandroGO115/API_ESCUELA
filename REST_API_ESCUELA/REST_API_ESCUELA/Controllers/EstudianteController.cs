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
        public List<Estudiante> GetEstudiantes()
        {
            return ADO_Estudiantes.Obtener_Todo();
        }

        [HttpPost]
        [Route("Insert")]
        public void InsertEstudiante(Estudiante e)
        {
            ADO_Estudiantes.Insertar(e);
            Console.Write("Estudiante Insertado");
        }
    }
}
