using REST_API_ESCUELA.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST_API_ESCUELA.Models
{
    public class Alumno
    {
        public int IdAlumnos { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public List<Materia> materias { get; set; }
    }
}