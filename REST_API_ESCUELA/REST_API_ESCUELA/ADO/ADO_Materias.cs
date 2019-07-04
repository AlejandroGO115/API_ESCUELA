using MySql.Data.MySqlClient;
using REST_API_ESCUELA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST_API_ESCUELA.ADO
{
    public class ADO_Materias
    {
        private static MySqlConnection con = Conexion.GetInstance().GetConnection();

        #region CONSULTAS
        private static readonly string INSERT_MATERIAS = "INSERT INTO MATERIAS (nombre_materia) VALUES (@nombre_materia);";
        //private static readonly string DELETE_ALUMNOS = "INSERT INTO MARCAS (NOMBRE_MARCAS) VALUES (@NOMBRE_MARCAS);";
        //private static readonly string UPDATE_ALUMNOS = "UPDATE alumnos SET (NOMBRE_MARCAS) VALUES (@NOMBRE_MARCAS);";
        //private static readonly string SELECT_ID = "SELECT * FROM MARCAS WHERE idMARCAS = @idMARCAS;";
        private static readonly string SELECT_ALL = "SELECT * FROM MATERIAS;";
        
        #endregion

        public static Materia Insertar(Materia m)
        {
            MySqlCommand cmd = new MySqlCommand(INSERT_MATERIAS , con);
            cmd.Parameters.AddWithValue("@nombre_materia" , m.Nombre_Materia);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                m.IdMaterias = (int)cmd.LastInsertedId;
            }
            catch (Exception)
            {
                m = null;
            }
            finally
            {
                con.Close();
            }

            return m;
        }

        public static List<Materia > Obtener_Todo()
        {
            List<Materia> lista = null;
            MySqlCommand cmd = new MySqlCommand(SELECT_ALL , con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    lista = new List<Materia>();
                    while (reader.Read())
                    {
                        Materia m = new Materia()
                        {
                            IdMaterias = reader["idMaterias"] != DBNull.Value ? reader.GetInt32("idMaterias") : 0 ,
                            Nombre_Materia = reader["nombre_materia"] != DBNull.Value ? reader.GetString("nombre_materia") : string.Empty
                        };
                        lista.Add(m);
                    }
                }
            }
            catch (Exception)
            {
                //Error
            }
            finally
            {
                con.Close();
            }
            return lista;
        }

        
    }
}