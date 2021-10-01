using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
   public class CuentasPorCobrarSyBase_DAL
    {
        public static string insertarCuentasPorCobrarSyBase(CuentasPorCobrar_SyBase_BL obj)
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


                        //String SQL_query = "INSERT INTO cuentasporcobrar " +
                        //                    " ( codemp              , numcpc              , tipdoc                 ,numtra                  ,codcli               ,    codven            ,fecemi             ,fecven               ,  fectra             , concep,             , valcob            ,tiporg               , numorg             , codapu             ,codap1               ,codmon              ,codusu              ,fecult              , referen) " +
                        //                    " VALUES " +
                        //                    " ( '" + codEmpresa + "', '" + obj.numcpc + ", '" + obj.tipdoc + "'    ,'" + obj.numtra + "'  ,'" + obj.codcli + "'   ,'" + obj.codven + "' ,'" + obj.fecemi + "', '" + obj.fecven + "', '" + obj.fectra + "', '" + obj.concep + "', " + obj.valcob + ", '" + obj.tiporg + "','" + obj.numorg + "','" + obj.codapu + "','" + obj.codap1 + "' ,'" + obj.codmon + "','" + obj.codusu + "','" + obj.codusu + "','" + obj.referen + "' ) ";

                    String SQL_query = "INSERT INTO cuentasporcobrar " +
                                           " ( codemp, numcpc, tipdoc ,numtra ,codcli ,codven  , fecemi ,fecven                    ,  fectra    , concep, valcob ,tiporg  , numorg, codapu ,codap1 ,codmon ,codusu ,fecult   ,   referen  , valcot , totnet  , totiva , ncuota , serie    , cerrado ,  fecfac , canmul , establ , porinter , totalinter , codforpag , hora                                 , cajapccob ) " +
                                           " VALUES " +
                                           " ( ?     , ?     , ?      ,?       ,?     ,?       ,      ?,  dateadd(mm, 1,getdate() ) ,      ?    ,       ?,      ?,     ?  ,      ?,      ?,     ? ,       ?,   ?   , getdate(),          ? ,   ?    ,     0  ,    0   ,   1     , ?        , 'N'    ,    ?    , 'N'   ,  '001'  ,  0       ,   0        ,  '20'     ,  CONVERT( CHAR( 20 ), getdate(), 8 ) , '001' ) ";

                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    cmd.Parameters.Add("codemp", OdbcType.VarChar).Value = codEmpresa;
                    cmd.Parameters.Add("numcpc", OdbcType.VarChar).Value = obj.numcpc;
                    cmd.Parameters.Add("tipdoc", OdbcType.VarChar).Value = obj.tipdoc;
                    cmd.Parameters.Add("numtra", OdbcType.VarChar).Value = obj.numtra;
                    cmd.Parameters.Add("codcli", OdbcType.VarChar).Value = obj.codcli;
                    cmd.Parameters.Add("codven", OdbcType.VarChar).Value = obj.codven;
                    cmd.Parameters.Add("fecemi", OdbcType.DateTime).Value = obj.fecemi;
                    cmd.Parameters.Add("fectra", OdbcType.DateTime).Value = obj.fectra;
                    cmd.Parameters.Add("concep", OdbcType.VarChar).Value = obj.concep;
                    cmd.Parameters.Add("valcob", OdbcType.Decimal).Value = obj.valcob;
                    cmd.Parameters.Add("tiporg", OdbcType.VarChar).Value = obj.tiporg;
                    cmd.Parameters.Add("numorg", OdbcType.VarChar).Value = obj.numorg;
                    cmd.Parameters.Add("codapu", OdbcType.VarChar).Value = obj.codapu;
                    cmd.Parameters.Add("codap1", OdbcType.VarChar).Value = obj.codap1;
                    cmd.Parameters.Add("codmon", OdbcType.VarChar).Value = obj.codmon;
                    cmd.Parameters.Add("codusu", OdbcType.VarChar).Value = obj.codusu;
                   // cmd.Parameters.Add("fecult", OdbcType.DateTime).Value = obj.fecult;
                    cmd.Parameters.Add("referen", OdbcType.VarChar).Value = obj.referen;
                    cmd.Parameters.Add("valcot", OdbcType.Decimal).Value = obj.valcot;
                    cmd.Parameters.Add("serie", OdbcType.VarChar).Value = obj.serie;
                    cmd.Parameters.Add("fecfac", OdbcType.DateTime).Value = obj.fecfac;

                    resultado = cmd.ExecuteNonQuery().ToString();
                    
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS CXC", ex.Message +" "+ex.StackTrace);

                   // throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS CXC", ex.Message +" "+ex.StackTrace);

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
