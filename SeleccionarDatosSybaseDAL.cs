using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus
{
    public class SeleccionarDatosSybaseDAL
    {
        public bool probarConexionSybase()
        {
            bool resultado = false;
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();
                    connection.Close();
                    connection.Dispose();

                    resultado = true;
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
            return resultado;
        }


        public static string mtdoSeleccionarMaximaFacturaMYSQL()
        {
            string resultado = "";
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();

                    // SLECCIONAR EMPRESA

                    String SQL_query_empresa = "SELECT cast(isnull(max(s.lm_factura_mysql),0) as int) maxFacturaMySQL FROM log_migracion_mysql_sybase s  where s.lm_estado ='FINALIZADO' ";
                    OdbcCommand cmdCodEmpresa = new OdbcCommand(SQL_query_empresa, connection);
                    resultado = cmdCodEmpresa.ExecuteScalar().ToString();
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
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
            return resultado;
        }


        public static void insertarLogMigracionFactura(string lm_factura_mysql, string lm_factura_sybase, string lm_estado, string lm_mensaje)
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();
               
                    String SQL_query = "INSERT INTO log_migracion_mysql_sybase " +
                                        " ( lm_factura_mysql, lm_factura_sybase, lm_fecha_genera, lm_estado ,lm_mensaje) " +
                                        " VALUES " +
                                        " ( ?              ,                  ?,   getdate()    ,      ?    ,     ?  ) ";
                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    cmd.Parameters.Add("lm_factura_mysql", OdbcType.NVarChar).Value = lm_factura_mysql;
                    cmd.Parameters.Add("lm_factura_sybase", OdbcType.NVarChar).Value = lm_factura_sybase;
                    cmd.Parameters.Add("lm_estado", OdbcType.NVarChar).Value = lm_estado;
                    cmd.Parameters.Add("lm_mensaje", OdbcType.NVarChar).Value = lm_mensaje;
                    resultado = cmd.ExecuteNonQuery().ToString();
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
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


        public static void actualizarLogMigracionFactura(string lm_factura_mysql, string lm_factura_sybase, string lm_estado, string lm_mensaje)
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();

                    String SQL_query = "UPDATE  log_migracion_mysql_sybase " +
                                        "   SET lm_estado = ?,     " +
                                        "    lm_mensaje = ?     " +
                                        "  where lm_factura_mysql = ? " +
                                        "  and   lm_factura_sybase = ? ";
                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    cmd.Parameters.Add("lm_estado", OdbcType.NVarChar).Value = lm_estado;
                    cmd.Parameters.Add("lm_mensaje", OdbcType.NVarChar).Value = lm_mensaje;
                    cmd.Parameters.Add("lm_factura_mysql", OdbcType.NVarChar).Value = lm_factura_mysql;
                    cmd.Parameters.Add("lm_factura_sybase", OdbcType.NVarChar).Value = lm_factura_sybase;
                    resultado = cmd.ExecuteNonQuery().ToString();
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
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
    }
}
