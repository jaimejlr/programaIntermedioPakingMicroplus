using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProgramaIntermedioPackinMicroplus
{
    class SeleccionarDatosMysqlDAL
    {
        public bool ProvarConexion()
        {
            bool resultado = false;
            using (MySqlConnection connection = new MySqlConnection(SettingsConexion.Default.conexionMySql))
            {
                try
                {
                    connection.Open();
                    string queryString = "select 1 from dual";
                    MySqlCommand cmd = new MySqlCommand(queryString, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    resultado = true;
                }
                catch(MySqlException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return resultado;
        }

        // Metodo Generado automáticamente Para seleccionar clientes retornando una lista  

        // **** NOTA: Eliminar la última coma antes del from en la sentencia SQL *** 

      

    }
}
