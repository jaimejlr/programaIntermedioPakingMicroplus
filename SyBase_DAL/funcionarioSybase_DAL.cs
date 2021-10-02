using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
    class funcionarioSybase_DAL
    {
        public static string insertarfuncinario(string nombreFuncionario)
        {
            string codFuncionarioSeleccionado = "";
            string codFuncionario = "";
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
                    String SQL_query_verificar_articulo = "SELECT codfun FROM funcionario where nomfun = '" + nombreFuncionario + "'  ";
                    OdbcCommand cmdVerificararticulo = new OdbcCommand(SQL_query_verificar_articulo, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificararticulo.ExecuteReader();
                    while (dr.Read())
                    {
                        codFuncionario = dr["codfun"].ToString();
                    }

                    if (codFuncionario == "")
                    {

                        // seleccionar código del cliente

                        String SQL_query_codCliente = "select cast( cast( max(codfun) +1 as float ) as int ) codigofun FROM funcionario ";
                        OdbcCommand cmdCodCliente = new OdbcCommand(SQL_query_codCliente, connection);
                        codFuncionarioSeleccionado = cmdCodCliente.ExecuteScalar().ToString();


                        String SQL_query = "INSERT INTO funcionario " +
                                            " (codemp              ,codfun                               ,nomfun                      ,codusu       ,fecult) " +
                                            " VALUES " +
                                            " ('" + codEmpresa + "', '" + codFuncionarioSeleccionado + "', '" + nombreFuncionario + "', 'MIGRACION', NULL   ) ";
                        OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                        cmd.ExecuteNonQuery().ToString();
                    }
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR FUNCIONARIO", ex.Message + " " + ex.StackTrace);

                    //throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR ART", ex.Message + " " + ex.StackTrace);

                    //  throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return codFuncionarioSeleccionado;
        }
    }
}
