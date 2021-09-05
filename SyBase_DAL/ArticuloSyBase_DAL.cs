using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
    public class ArticuloSyBase_DAL
    {
        public static string insertarArticuloSyBase(ArticuloSyBase_BL obj)
        {
            string resultado = "";
            string codArticulo = "";
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


                    // verificar si existe articulo
                    String SQL_query_verificar_articulo = "SELECT codart FROM articulos where codemp = '" + codEmpresa + "'  and nomart = '" + obj.nomart + "'  ";
                    OdbcCommand cmdVerificararticulo = new OdbcCommand(SQL_query_verificar_articulo, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificararticulo.ExecuteReader();
                    while (dr.Read())
                    {
                        codArticulo = dr["codart"].ToString();
                    }

                    if (codArticulo == "")
                    {

                        // seleccionar código del cliente

                        //String SQL_query_codCliente = "select cast( cast( max(codcli) +1 as float ) as int ) codigoCliente FROM clientes ";
                        //OdbcCommand cmdCodCliente = new OdbcCommand(SQL_query_codCliente, connection);
                        //codCliente = cmdCodCliente.ExecuteScalar().ToString();
                        //codCliente = codCliente.PadLeft(5, '0');

                        codArticulo = obj.codart;

                        String SQL_query = "INSERT INTO articulos " +
                                            " ( codemp              , nomart              , codcla    ,codart                  ,codiva               ,    coduni           ) " +
                                            " VALUES " +
                                            " ( '" + codEmpresa + "', '" + obj.nomart + "', 'PT001', '" + obj.codart + "'    ,'" + obj.codiva + "'  ,'" + obj.coduni + "'  ) ";
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
            return codArticulo;
        }
    }
}
