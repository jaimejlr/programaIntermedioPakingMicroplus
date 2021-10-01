using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
   public class DatosVehiculosSyBase_DAL
    {
        public static string insertarVehiculoSyBase(DatosVehiculosSyBase_BL obj)
        {
            string resultado = "";
            string codVehiculo = "";
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


                    // verificar si existe cliente
                    String SQL_query_verificar_transpotista = "SELECT codveh FROM datosvehiculos where codemp = '" + codEmpresa + "'  and codveh = '" + obj.codveh + "' and nomveh= '" + obj.nomveh + "'  ";
                    OdbcCommand cmdVerificarTransportista = new OdbcCommand(SQL_query_verificar_transpotista, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificarTransportista.ExecuteReader();
                    while (dr.Read())
                    {
                        codVehiculo = dr["codveh"].ToString();
                    }

                    if (codVehiculo == "")
                    {


                        String SQL_query = "INSERT INTO datosvehiculos " +
                                            " ( codemp              , codveh             , nomveh ) " +
                                            " VALUES " +
                                            " ( '" + codEmpresa + "', '" + obj.codveh + "', '" + obj.nomveh + "'  ) ";
                        OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                        resultado = cmd.ExecuteNonQuery().ToString();
                    }
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS VEHI", ex.Message +" "+ex.StackTrace);

                  //  throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS VEHI", ex.Message +" "+ex.StackTrace);

                  //  throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return obj.codveh;
        }
    }
}
