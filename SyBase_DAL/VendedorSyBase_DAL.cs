using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
   public class VendedorSyBase_DAL
    {
        public static string insertarVendedoresSyBase(VendedorSybase_BL obj)
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

                    // VERIFICAR SI vendedor

                    String SQL_query_verificar_vendedor = "SELECT codven FROM vendedorescob where codemp = '" + codEmpresa + "'  and nomven = '" + obj.nomven+"' ";
                    OdbcCommand cmdVerificarVendedor = new OdbcCommand(SQL_query_verificar_vendedor, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificarVendedor.ExecuteReader();
                    while(dr.Read())
                    {
                        codVendedor = dr["codven"].ToString();
                    }

                    // seleccionar código del cliente

                    if(codVendedor =="")
                    {
                   
                        String SQL_query_codCliente = "select cast( cast( max(codven) +1 as float ) as int ) codigoVendedor FROM vendedorescob ";
                        OdbcCommand cmdCodCliente = new OdbcCommand(SQL_query_codCliente, connection);
                        codVendedor = cmdCodCliente.ExecuteScalar().ToString();
                        codVendedor = codVendedor.PadLeft(5, '0');

                        String SQL_query = "INSERT INTO vendedorescob " +
                                            " ( codemp              , codven                , nomven              , codusu             , codzona            , direcven            , fecult) " +
                                            " VALUES " +
                                            " ( '" + codEmpresa + "', '" + codVendedor + "', '" + obj.nomven + "' , '"+ obj.codusu +"'  , '"+ obj.codzona+"', '"+ obj.direcven+ "', getdate()) ";
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
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS VEND", ex.Message);
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
