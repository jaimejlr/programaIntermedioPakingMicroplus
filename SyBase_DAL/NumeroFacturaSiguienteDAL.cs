using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
   public  class NumeroFacturaSiguienteDAL
    {
        public static string seleccionarSiguienteFactura()
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();
                    // SLECCIONAR EMPRESA
                    string resul = "";
                    String SQL_query_empresa = "SELECT cast(cast (max(cast(SUBSTRING(numfac,2) as float)) +1 as float) as int) FROM encabezadofacturas  ";
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

        public static string seleccionarCodigoEmpresa()
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();

                    // SLECCIONAR EMPRESA

                    String SQL_query_empresa = "SELECT  codemp FROM EMPRESA ";
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

        public static string seleccionarSiguienteSecuencialCXC()
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();
                    // SLECCIONAR EMPRESA
                    string resul = "";
                    String SQL_query_empresa = "SELECT cast(cast (max(cast(SUBSTRING(seccue,2) as float)) +1 as float) as int) FROM secuencias where codsec= 'VC_CXC' ";
                    OdbcCommand cmdCodEmpresa = new OdbcCommand(SQL_query_empresa, connection);
                    resul = cmdCodEmpresa.ExecuteScalar().ToString();
                    resultado = resul.PadLeft(8, '0');

                   // actualizar el secuencial de CXC
                   String SQL_query_updateSecuencia = "UPDATE secuencias SET seccue = '" + resultado + "'  where codsec= 'VC_CXC' ";
                    OdbcCommand cmdUpdateSecuencia = new OdbcCommand(SQL_query_updateSecuencia, connection);
                    cmdUpdateSecuencia.ExecuteNonQuery();

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
    }
}
