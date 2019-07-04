using MySql.Data.MySqlClient;
using REST_API_ESCUELA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST_API_ESCUELA.ADO
{
    public class ADO_Alumnos
    {
        private static MySqlConnection con = Conexion.GetInstance().GetConnection();

        #region CONSULTAS
        private static readonly string INSERT_ALUMNOS = "INSERT INTO alumnos (Nombre,Edad) VALUES (@Nombre,@Edad);";
        //private static readonly string DELETE_ALUMNOS = "INSERT INTO MARCAS (NOMBRE_MARCAS) VALUES (@NOMBRE_MARCAS);";
        //private static readonly string UPDATE_ALUMNOS = "UPDATE alumnos SET (NOMBRE_MARCAS) VALUES (@NOMBRE_MARCAS);";
        //private static readonly string SELECT_ID = "SELECT * FROM MARCAS WHERE idMARCAS = @idMARCAS;";
        private static readonly string SELECT_ALL = "SELECT * FROM alumnos;";
        private static readonly string SELECT_ALL_ID = "select * from alumnosmaterias join alumnos on idAlumnos = idAlumno join materias on idMaterias = idMateria where idAlumno = @idAlumno";
        #endregion

        public static Alumno Insertar(Alumno e)
        {
            MySqlCommand cmd = new MySqlCommand(INSERT_ALUMNOS , con);
            cmd.Parameters.AddWithValue("@Nombre" , e.Nombre);
            cmd.Parameters.AddWithValue("@Edad" , e.Edad);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                e.IdAlumnos = (int)cmd.LastInsertedId;
            }
            catch (Exception)
            {
                e = null;
            }
            finally
            {
                con.Close();
            }

            return e;
        }

        public static List<Alumno> Obtener_Todo()
        {
            List<Alumno> lista = null;
            MySqlCommand cmd = new MySqlCommand(SELECT_ALL , con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    lista = new List<Alumno>();
                    while (reader.Read())
                    {
                        Alumno e = new Alumno()
                        {
                            IdAlumnos = reader["idAlumnos"] != DBNull.Value ? reader.GetInt32("idAlumnos") : 0,
                            Nombre = reader["Nombre"] != DBNull.Value ? reader.GetString("Nombre") : string.Empty,
                            Edad = reader["Edad"] != DBNull.Value ? reader.GetString("Edad") : string.Empty
                        };
                        lista.Add(e);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            return lista;
        }

        public static Alumno GetAlumno(int ID)
        {
            List<Materia> ListMaterias = null;
            Alumno alu = null;

            MySqlCommand cmd = new MySqlCommand(SELECT_ALL_ID , con);
            cmd.Parameters.AddWithValue("@idAlumno" , ID);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    ListMaterias = new List<Materia>();
                    while (reader.Read())
                    {
                        Materia m = new Materia()
                        {
                            IdMaterias = reader["idMateria"] != DBNull.Value ? reader.GetInt32("idMateria") : 0 ,
                            Nombre_Materia = reader["Nombre_Materia"] != DBNull.Value ? reader.GetString("Nombre_Materia") : string.Empty 
                        };
                        ListMaterias.Add(m);
                    }
                    alu = new Alumno()
                    {
                        IdAlumnos = reader["idAlumno"] != DBNull.Value ? reader.GetInt32("idAlumnos") : 0 ,
                        Nombre = reader["Nombre"] != DBNull.Value ? reader.GetString("Nombre") : string.Empty ,
                        Edad = reader["Edad"] != DBNull.Value ? reader.GetString("Edad") : string.Empty,
                        materias = ListMaterias
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            return alu;
        }
    }
}