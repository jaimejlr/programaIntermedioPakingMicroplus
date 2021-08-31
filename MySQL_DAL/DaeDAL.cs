﻿using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.MySQL_DAL
{
    public class DaeDAL
    {
        // Metodo Generado automáticamente Para seleccionar fue retornando una lista  

        // **** NOTA: Eliminar la última coma antes del from en la sentencia SQL *** 

        public static DaeMYSQL_BL mtdoSeleccionarTodofue(String Dae)
        {
            DaeMYSQL_BL obj = new DaeMYSQL_BL();
            using (MySqlConnection conex = new MySqlConnection(SettingsConexion.Default.conexionMySql))
            {
                try
                {
                    MySqlCommand cmd = null;
                    string sql = " select " +
                     " fue_id, " +
                     " fue_numero, " +
                     " fue_pais, " +
                     " fue_caduca, " +
                     " fue_manifiesto, " +
                     " fue_distrito, " +
                     " fue_anio, " +
                     " fue_codigo, " +
                     " fue_ingreso " +
                    " from fue " +
                    " where fue_manifiesto = @fue_manifiesto ";
                    cmd = new MySqlCommand(sql, conex);
                    cmd.Parameters.Add("@fue_manifiesto", MySqlDbType.VarChar).Value = Dae;
                    MySqlDataReader dr = null;
                    conex.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj.fue_id = dr["fue_id"].ToString();
                        obj.fue_numero = dr["fue_numero"].ToString();
                        obj.fue_pais = dr["fue_pais"].ToString();
                        obj.fue_caduca = dr["fue_caduca"].ToString();
                        obj.fue_manifiesto = dr["fue_manifiesto"].ToString();
                        obj.fue_distrito = dr["fue_distrito"].ToString();
                        obj.fue_anio = dr["fue_anio"].ToString();
                        obj.fue_codigo = dr["fue_codigo"].ToString();
                        obj.fue_ingreso = dr["fue_ingreso"].ToString();
                    }
                    conex.Close();
                }
                catch (Exception ex)
                {
                    conex.Close();
                    conex.Dispose();
                    throw ex;
                }
                finally
                {
                    conex.Close();
                    conex.Dispose();
                }
            }
            return obj;
        }
    }
}