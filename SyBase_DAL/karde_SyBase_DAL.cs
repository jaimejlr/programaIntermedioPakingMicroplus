using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
   public class karde_SyBase_DAL
    {
        public static string insertarKardex(Karde_SyBase_BL obj)
        {
            string resultado = "";
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


                    // seleccionar código del cliente

                    //String SQL_query_codCliente = "select cast( cast( max(codcli) +1 as float ) as int ) codigoCliente FROM clientes ";
                    //OdbcCommand cmdCodCliente = new OdbcCommand(SQL_query_codCliente, connection);
                    //codCliente = cmdCodCliente.ExecuteScalar().ToString();
                    //codCliente = codCliente.PadLeft(5, '0');

                    //String SQL_query = "INSERT INTO kardex " +
                    //                  " ( codemp              , tiporg              , numdoc                 ,codart                  ,tipdoc               ,    codalm            ,fecdoc             ,cantot               ,  cosuni             , costot,             , totven            ,codcli               , codven             , codusu             ,fecult               ,cancaja              ,numren              ,hora              , establ) " +
                    //                  " VALUES " +
                    //                  " ( '" + codEmpresa + "', '" + obj.tiporg + ", '" + obj.numdoc + "'    ,'" + obj.codart + "'  ,'" + obj.tipdoc + "'   ,'" + obj.codalm + "' ,'" + obj.fecdoc + "', '" + obj.cantot + "', '" + obj.cosuni + "', '" + obj.costot + "', " + obj.totven + ", '" + obj.codcli + "','" + obj.codven + "','" + obj.codusu + "','" + obj.fecult + "' ,'" + obj.cancaja + "','" + obj.numren + "','" + obj.codusu + "','" + obj.establ + "' ) ";

                    String SQL_query = "INSERT INTO kardex " +
                                        " ( codemp, tiporg, numdoc,codart ,tipdoc, codalm , coduni,fecdoc ,cantot,  cosuni , costot,  totven ,codcli, codven, codusu  ,fecult ,cancaja,  numren, establ , referen) " +
                                        " VALUES " +
                                        " ( ?     ,      ?,      ?,      ?,     ?,       ?,      ?,     ? ,      ?,       ?,      ?,        ?,      ?,      ?,     ?  ,     ? ,      ?,        ?,     ?  ,    ? ) ";
                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    cmd.Parameters.Add("codemp", OdbcType.NVarChar).Value = codEmpresa;
                    cmd.Parameters.Add("tiporg", OdbcType.NVarChar).Value = obj.tiporg;
                    cmd.Parameters.Add("numdoc", OdbcType.NVarChar).Value = obj.numdoc;
                    cmd.Parameters.Add("codart", OdbcType.NVarChar).Value = obj.codart;
                    cmd.Parameters.Add("tipdoc", OdbcType.NVarChar).Value = obj.tipdoc;
                    cmd.Parameters.Add("codalm", OdbcType.NVarChar).Value = obj.codalm;
                    cmd.Parameters.Add("coduni", OdbcType.NVarChar).Value = obj.coduni;
                    cmd.Parameters.Add("fecdoc", OdbcType.DateTime).Value = obj.fecdoc;
                    cmd.Parameters.Add("cantot", OdbcType.Decimal).Value = obj.cantot;
                    cmd.Parameters.Add("cosuni", OdbcType.Decimal).Value = obj.cosuni;
                    cmd.Parameters.Add("costot", OdbcType.Decimal).Value = obj.costot;
                    cmd.Parameters.Add("totven", OdbcType.Decimal).Value = obj.totven;
                    cmd.Parameters.Add("codcli", OdbcType.NVarChar).Value = obj.codcli;
                    cmd.Parameters.Add("codven", OdbcType.NVarChar).Value = obj.codven;
                    cmd.Parameters.Add("codusu", OdbcType.NVarChar).Value = obj.codusu;
                    cmd.Parameters.Add("fecult", OdbcType.DateTime).Value = obj.fecult;
                    cmd.Parameters.Add("cancaja", OdbcType.Decimal).Value = obj.cancaja;
                    cmd.Parameters.Add("numren", OdbcType.Numeric).Value = obj.numren;
                    cmd.Parameters.Add("establ", OdbcType.NVarChar).Value = obj.establ;
                    cmd.Parameters.Add("referen", OdbcType.NVarChar).Value = obj.referen;
                    resultado = cmd.ExecuteNonQuery().ToString();

                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR KARD", ex.Message +" "+ex.StackTrace);

                   // throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR KARD", ex.Message +" "+ex.StackTrace);

                    //throw new Exception(ex.Message);
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
