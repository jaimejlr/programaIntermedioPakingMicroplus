using ProgramaIntermedioPackinMicroplus.MySQL;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
    public class ClienteSyBase_DAL
    {

        public static string insertarClaseClienteSyBase(ClienteMySqlBL obj)
        {
            string resultado = "";
            string codClase = "";
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
                    String SQL_query_verificar_cliente = "SELECT codcla FROM clasesclientes where codemp = '" + codEmpresa + "'  and nomcla = '" + obj.CCONSIGNA + "' ";
                    OdbcCommand cmdVerificarCliente = new OdbcCommand(SQL_query_verificar_cliente, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificarCliente.ExecuteReader();
                    while (dr.Read())
                    {
                        codClase = dr["codcla"].ToString();
                    }

                    if (codClase == "")
                    {
                        // seleccionar código del cliente

                        //**** CODCLA = Código del cliente padre.

                        //String SQL_query_codCliente = "select cast( cast( max(codcli) +1 as float ) as int ) codigoCliente FROM clientes ";
                        //OdbcCommand cmdCodCliente = new OdbcCommand(SQL_query_codCliente, connection);
                        //codCliente = cmdCodCliente.ExecuteScalar().ToString();
                        //codCliente = codCliente.PadLeft(5, '0');

                        String SQL_query = "INSERT INTO clasesclientes " +
                                            " ( codemp              , codcla                                 ,nomcla                   ) " +
                                            " VALUES " +
                                            " ( '" + codEmpresa + "', '" + obj.CCONSIGNA.Substring(1, 5) + "', '"+ obj.CCONSIGNA +"'   ) ";
                        OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                        resultado = cmd.ExecuteNonQuery().ToString();

                        codClase = obj.CCONSIGNA.Substring(1, 5);
                    }
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS CLA CLI", ex.Message +" "+ex.StackTrace);

                   // throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS CLA CLI", ex.Message +" "+ex.StackTrace);

                    // throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return codClase;
        }
        public static string insertarClienteSyBase(ClienteMySqlBL obj, string codClase, string codzona)
        {
            string resultado = "";
            string codCliente = "";
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
                    String SQL_query_verificar_cliente = "SELECT codcli FROM clientes where codemp = '" + codEmpresa + "'  and nomcli = '" + obj.CNOMBRE + "' and rucced= '" + obj.identificacion + "'  ";
                    OdbcCommand cmdVerificarCliente = new OdbcCommand(SQL_query_verificar_cliente, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificarCliente.ExecuteReader();
                    while (dr.Read())
                    {
                        codCliente = dr["codcli"].ToString();
                    }

                    if(codCliente =="")
                    {

                            // seleccionar código del cliente

                        //**** CODCLA = Código del cliente padre.

                            String SQL_query_codCliente = "select cast( cast( max(codcli) +1 as float ) as int ) codigoCliente FROM clientes ";
                            OdbcCommand cmdCodCliente = new OdbcCommand(SQL_query_codCliente, connection);
                            codCliente = cmdCodCliente.ExecuteScalar().ToString();
                            codCliente = codCliente.PadLeft(5, '0');

                            String SQL_query = "INSERT INTO clientes " +
                                                " ( codemp              , codcli            , codcla            ,apliva   ,nomcli               ,    rucced                   ,dircli                 ,      telcli             ,        contac          , ciucli                , mail                ,codcre                    , codcta , lispre ,  codusu       ,  dircli2                   , repcli                 , descu , cupo  , salact , tiprucced , forpag , estado,  especial, docrec02,  datcon01 , ivapres, parterel, codzona) " +
                                                " VALUES " +
                                                " ( '"+ codEmpresa + "', '"+ codCliente + "', '"+ codClase + "', '3'     ,'"+ obj.CNOMBRE+ "'  ,'" + obj.identificacion + "' ,'" + obj.DIRECCION + "' ,'" + obj.CTELEFONO1 + "' ,'" + obj.CCONTACTO + "' ,  'N'                  ,'" + obj.E_MAIL + "' ,'" + obj.COD_CLIENT + "'   , ''     , '1'  ,   'IMPORTACION' ,  '" + obj.CLOCALIZA + "'  , '" + obj.CCONSIGNA + "' ,  0    , 0     ,0      , 'P'       , 'A'     , 'S'   ,  'R'     , '01'   ,    0       , 'N'   , 'N0'     ,'"+ codzona + "' ) ";
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
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS CLI", ex.Message +" "+ex.StackTrace);

                   // throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS CLI", ex.Message +" "+ex.StackTrace);

                   // throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return codCliente;
        }


        public static bool comparaSiEisteClienteSyBase(String codigoCliente)
        {
            bool resultado = false;
            string codCliente = "";
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
                    String SQL_query_verificar_cliente = "SELECT codcli FROM clientes where codemp = '" + codEmpresa + "'  and codcli = '" + codigoCliente + "' ";
                    OdbcCommand cmdVerificarCliente = new OdbcCommand(SQL_query_verificar_cliente, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificarCliente.ExecuteReader();
                    while (dr.Read())
                    {
                        resultado = true;
                    }

                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR COMPARAR CLI", ex.Message + " " + ex.StackTrace);

                    // throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR COMPARAR CLI", ex.Message + " " + ex.StackTrace);

                    // throw new Exception(ex.Message);
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
