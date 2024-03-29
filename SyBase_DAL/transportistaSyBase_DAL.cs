﻿using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
   public class transportistaSyBase_DAL
    {
        public static string insertarTransportistaSyBase(transportistaSyBase_BL obj)
        {
            string resultado = "";
            string codTransportista = "";
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
                    String SQL_query_verificar_transpotista = "SELECT codtrans FROM transportistas where codemp = '"+ codEmpresa +"'  and nomtrans = '"+ obj.nomtrans +"'  ";
                    OdbcCommand cmdVerificarTransportista = new OdbcCommand(SQL_query_verificar_transpotista, connection);
                    OdbcDataReader dr = null;
                    dr = cmdVerificarTransportista.ExecuteReader();
                    while (dr.Read())
                    {
                        codTransportista = dr["codtrans"].ToString();
                        resultado = codTransportista;
                    }

                    if (codTransportista == "")
                    {

                       
                        String SQL_query = "INSERT INTO transportistas " +
                                            " ( codemp              , codtrans             , nomtrans ) " +
                                            " VALUES " +
                                            " ( '" + codEmpresa + "', '" + obj.codtrans + "', '" + obj.nomtrans + "'  ) ";
                        OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                        resultado = cmd.ExecuteNonQuery().ToString();
                        resultado = obj.codtrans;
                    }
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS TRANS", ex.Message +" "+ex.StackTrace);

                  //  throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INS TRANS", ex.Message +" "+ex.StackTrace);

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
