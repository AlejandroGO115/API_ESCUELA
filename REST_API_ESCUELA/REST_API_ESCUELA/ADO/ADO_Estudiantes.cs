using MySql.Data.MySqlClient;
using REST_API_ESCUELA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST_API_ESCUELA.ADO
{
    public class ADO_Estudiantes
    {
        private static MySqlConnection con = Conexion.GetInstance().GetConnection();

        #region CONSULTAS
        private static readonly string INSERT_ALUMNOS = "INSERT INTO alumnos (Nombre,Edad) VALUES (@Nombre,@Edad);";
        //private static readonly string DELETE_ALUMNOS = "INSERT INTO MARCAS (NOMBRE_MARCAS) VALUES (@NOMBRE_MARCAS);";
        //private static readonly string UPDATE_ALUMNOS = "UPDATE alumnos SET (NOMBRE_MARCAS) VALUES (@NOMBRE_MARCAS);";
        //private static readonly string SELECT_ID = "SELECT * FROM MARCAS WHERE idMARCAS = @idMARCAS;";
        private static readonly string SELECT_ALL = "SELECT * FROM alumnos;";
        #endregion

        public static void Insertar(Estudiante e)
        {
            MySqlCommand cmd = new MySqlCommand(INSERT_ALUMNOS , con);
            cmd.Parameters.AddWithValue("@Nombre" , e.Nombre);
            cmd.Parameters.AddWithValue("@Edad" , e.Edad);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public static List<Estudiante> Obtener_Todo()
        {
            List<Estudiante> lista = null;
            MySqlCommand cmd = new MySqlCommand(SELECT_ALL , con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    lista = new List<Estudiante>();
                    while (reader.Read())
                    {
                        Estudiante e = new Estudiante()
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
    }
}