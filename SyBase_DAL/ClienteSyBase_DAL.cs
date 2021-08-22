﻿using ProgramaIntermedioPackinMicroplus.MySQL;
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
        public string insertarClienteFacturaSyBase(ClienteMySqlBL obj)
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

                    // seleccionar código del cliente
                   
                    String SQL_query_codCliente = "select cast( cast( max(codcli) +1 as float ) as int ) codigoCliente FROM clientes ";
                    OdbcCommand cmdCodCliente = new OdbcCommand(SQL_query_codCliente, connection);
                    codCliente = cmdCodCliente.ExecuteScalar().ToString();


                    String SQL_query = "INSERT INTO clientes " +
                                        " ( codemp              , codcli                             , codcla ,apliva   ,nomcli               ,    rucced                   ,dircli                 ,      telcli             ,        contac          , ciucli                , mail                ,codcre ) " +
                                        " VALUES " +
                                        " ( '"+ codEmpresa + "', '"+ codCliente.PadLeft(5, '0') + "', 'ANDAS', '3'     ,'"+ obj.CNOMBRE+ "'  ,'" + obj.identificacion + "' ,'" + obj.DIRECCION + "' ,'" + obj.CTELEFONO1 + "' ,'" + obj.CCONTACTO + "' ,'" + obj.CLOCALIZA + "','" + obj.E_MAIL + "' ,'" + obj.COD_CLIENT + "'  ) ";
                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    resultado = cmd.ExecuteNonQuery().ToString();
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
            return codCliente;
        }
    }
}
