using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus
{
    public class ParametrosGeneralesDAL
    {
        public bool mtdoSeleccionarDatosEmail()
        {
            OdbcDataReader dr = null;
            bool resultado = false;
            OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase);
            try
            {
                connection.Open();
                string queryString = " SELECT P.PG_CODIGO, P.PG_VALOR " +
                                     " FROM ak_parametros_generales P " +
                                     " WHERE P.PG_ESTADO = 'A' " +
                                     " AND P.PG_ID_ID_PARAMETRO IN (SELECT PP.PG_ID_PARAMETRO " +
                                     "							FROM ak_parametros_generales PP " +
                                     "                           WHERE PP.PG_CODIGO = 'ENVIO_MAIL' " +
                                     "                          AND P.PG_ESTADO = 'A' ) " +
                                     " ORDER BY  P.PG_ORDEN  ASC ";

                OdbcCommand cmd = new OdbcCommand(queryString, connection);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["PG_CODIGO"].ToString() == "EMAIL_ORIGEN")
                    {
                        EnviarCorreoBL.email = dr["PG_VALOR"].ToString();
                    }
                    if (dr["PG_CODIGO"].ToString() == "EMAIL_RESPUESTA")
                    {
                        EnviarCorreoBL.CorreoDeRespuesta = dr["PG_VALOR"].ToString();
                    }
                    if (dr["PG_CODIGO"].ToString() == "NOMBRE_EMAIL")
                    {
                        EnviarCorreoBL.nombreEmail = dr["PG_VALOR"].ToString();
                    }
                    if (dr["PG_CODIGO"].ToString() == "PASSWORD")
                    {
                        EnviarCorreoBL.passwordEmail = dr["PG_VALOR"].ToString();
                    }
                    if (dr["PG_CODIGO"].ToString() == "HOST")
                    {
                        EnviarCorreoBL.hosts = dr["PG_VALOR"].ToString();
                    }
                    if (dr["PG_CODIGO"].ToString() == "PUERTO")
                    {
                        EnviarCorreoBL.puerto = Convert.ToInt16(dr["PG_VALOR"].ToString());
                    }
                    if (dr["PG_CODIGO"].ToString() == "SSL")
                    {
                        EnviarCorreoBL.ssl = ((dr["PG_VALOR"].ToString()) == "1") ? true : false;
                    }
                    if (dr["PG_CODIGO"].ToString() == "ENVIO_CORREOS")
                    {
                        EnviarCorreoBL.EnvioCorreo = ((dr["PG_VALOR"].ToString()) == "1") ? true : false;
                    }
                    if (dr["PG_CODIGO"].ToString() == "CORREOS_PARA")
                    {
                        EnviarCorreoBL.Para = dr["PG_VALOR"].ToString();
                    }

                }
                resultado = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                resultado = false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return resultado;
        }

        public string mtdoSeleccionarDatosCadenaConexion()
        {
            OdbcDataReader dr = null;
            string resultado = "";
            OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase);
            try
            {
                connection.Open();
                string queryString = " SELECT P.PG_CODIGO, P.PG_VALOR " +
                                     " FROM ak_parametros_generales P " +
                                     " WHERE P.PG_ESTADO = 'A' " +
                                     " AND P.PG_CODIGO = 'CADENA_CONEXION_MYSQL' ";

                OdbcCommand cmd = new OdbcCommand(queryString, connection);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    resultado = (dr["PG_VALOR"].ToString());
                }
            }
            catch (Exception ex)
            {
              //  throw new Exception(ex.Message);
                resultado = "";
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
