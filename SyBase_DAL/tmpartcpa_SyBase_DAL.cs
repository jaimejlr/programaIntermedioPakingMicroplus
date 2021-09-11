using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
   public static class tmpartcpa_SyBase_DAL
    {

        public static string insertartmpartcpaSyBase(tmpartcpa_SyBase_BL obj)
        {
            string resultado = "";
            string codVendedor = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();
                    // SLECCIONAR EMPRESA

                    string codEmpresa = "";
                    String SQL_query_empresa = "SELECT  codemp FROM EMPRESA ";
                    OdbcCommand cmdCodEmpresa = new OdbcCommand(SQL_query_empresa, connection);
                    codEmpresa = cmdCodEmpresa.ExecuteScalar().ToString();


                        String SQL_query = "INSERT INTO tmpartcpa " +
                                            " ( codemp               ,numfac                 ,codart             ,nomart               ,numren              ,codpro               ,peso               ,tiporg ) " +
                                            " VALUES " +
                                            " ( '" + codEmpresa + "', '" + obj.numfac + "', '" + obj.codart + "' , '" + obj.nomart + "','" + obj.numren + "', '" + obj.codpro + "', '" + obj.peso + "','" + obj.tiporg + "') ";
                        OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                        resultado = cmd.ExecuteNonQuery().ToString();
                    

                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS tmpartcpa ", ex.Message);
                    // throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS VEND", ex.Message);
                    // throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return codVendedor;
        }
    }
}
