using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.MySQL_DAL
{
  public  class ProveedorMysql_DAL
    {
        // Metodo Generado automáticamente Para seleccionar proveedores retornando una lista  

        // **** NOTA: Eliminar la última coma antes del from en la sentencia SQL *** 

        public static ProveedoresMysql_BL mtdoSeleccionarTodoproveedores(String codProveedor)
        {
            ProveedoresMysql_BL obj = new ProveedoresMysql_BL();
            using (MySqlConnection conex = new MySqlConnection(SettingsConexion.Default.conexionMySql))
            {
                try
                {
                    MySqlCommand cmd = null;
                    string sql = " select " +
                     " COD_PROVE, " +
                     " NOMBRE, " +
                     " DIRECCION, " +
                     " TELEFONO1, " +
                     " TELEFONO2, " +
                     " FAX, " +
                     " CONTACTO, " +
                     " OBSERVA, " +
                     " prove_aduana, " +
                     " CUARTO_FRIO, " +
                     " PAGINA_WEB, " +
                     " identificacion, " +
                     " tipo_identificacion_id, " +
                     " razon_social, " +
                     " email " +
                    " from proveedores " +
                    "where COD_PROVE = @COD_PROVE ";
                     cmd = new MySqlCommand(sql, conex);
                    cmd.Parameters.Add("@COD_PROVE", MySqlDbType.VarChar).Value = codProveedor;
                    MySqlDataReader dr = null;
                    conex.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                       
                        obj.COD_PROVE = dr["COD_PROVE"].ToString();
                        obj.NOMBRE = dr["NOMBRE"].ToString();
                        obj.DIRECCION = dr["DIRECCION"].ToString();
                        obj.TELEFONO1 = dr["TELEFONO1"].ToString();
                        obj.TELEFONO2 = dr["TELEFONO2"].ToString();
                        obj.FAX = dr["FAX"].ToString();
                        obj.CONTACTO = dr["CONTACTO"].ToString();
                        obj.OBSERVA = dr["OBSERVA"].ToString();
                        obj.prove_aduana = dr["prove_aduana"].ToString();
                        obj.CUARTO_FRIO = dr["CUARTO_FRIO"].ToString();
                        obj.PAGINA_WEB = dr["PAGINA_WEB"].ToString();
                        obj.identificacion = dr["identificacion"].ToString();
                        obj.tipo_identificacion_id = dr["tipo_identificacion_id"].ToString();
                        obj.razon_social = dr["razon_social"].ToString();
                        obj.email = dr["email"].ToString();
                    }
                    conex.Close();
                }
                catch (Exception ex)
                {
                    conex.Close();
                    conex.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR SELE PROV", ex.Message);

                  //  throw ex;
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
