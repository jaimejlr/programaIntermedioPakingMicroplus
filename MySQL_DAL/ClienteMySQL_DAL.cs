using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.MySQL_DAL
{
   public class ClienteMySQL_DAL
    {
        public static ClienteMySqlBL mtdoSeleccionarTodoclientes(String codCliente)
        {
            ClienteMySqlBL obj = new ClienteMySqlBL();
            using (MySqlConnection conex = new MySqlConnection(SettingsConexion.Default.conexionMySql))
            {
                try
                {
                    MySqlCommand cmd = null;
                    string sql = " select distinct " +
                     " COD_CLIENT, " +
                     " CNOMBRE, " +
                     " FECHA_ING, " +
                     " CZONA, " +
                     " CLOCALIZA, " +
                     " CTELEFONO1, " +
                     " CTELEFONO2, " +
                     " DIRECCION, " +
                     " CFAX, " +
                     " E_MAIL, " +
                     " CCONTACTO, " +
                     " E_MAIL_CONTACTO, " +
                     " COD_PROVE, " +
                     " MARCAR, " +
                     " NUMERAR, " +
                     " TIPO_CAJA, " +
                     " CATEGORIA, " +
                     " TAR, " +
                     " precio_referencial_alta, " +
                     " precio_referencial_baja, " +
                     " precio_referencial_especial, " +
                     " CCREDITO, " +
                     " ALFA_CLIENT, " +
                     " CCONSIGNA, " +
                     " CACTIVO, " +
                     " usu_id, " +
                     " moroso, " +
                     " dia_pago_1, " +
                     " dia_pago_2, " +
                     " dia_pago_3, " +
                     " c.tipo_identificacion_id, " +
                     " ti.nombre as tipoIdentificacion,  ti.valor as valorTipoIdentificacion, " +
                     " identificacion, " +
                     " c.tipo_cliente_id, " +
                     " tc.nombre as tipoCliente " +
                    " from clientes c " +
                    " left outer join tipo_cliente tc on c.tipo_cliente_id = tc.tipo_cliente_id " +
                    " left outer join tipo_identificacion ti on c.tipo_identificacion_id = ti.tipo_identificacion_id " +
                    " where COD_CLIENT = @COD_CLIENT ";
                    cmd = new MySqlCommand(sql, conex);
                    cmd.Parameters.Add("@COD_CLIENT", MySqlDbType.VarChar, 10).Value = codCliente;
                    MySqlDataReader dr = null;
                    conex.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj.COD_CLIENT = dr["COD_CLIENT"].ToString();
                        obj.CNOMBRE = dr["CNOMBRE"].ToString();
                        obj.FECHA_ING = dr["FECHA_ING"].ToString();
                        obj.CZONA = dr["CZONA"].ToString();
                        obj.CLOCALIZA = dr["CLOCALIZA"].ToString();
                        obj.CTELEFONO1 = dr["CTELEFONO1"].ToString();
                        obj.CTELEFONO2 = dr["CTELEFONO2"].ToString();
                        obj.DIRECCION = dr["DIRECCION"].ToString();
                        obj.CFAX = dr["CFAX"].ToString();
                        obj.E_MAIL = dr["E_MAIL"].ToString();
                        obj.CCONTACTO = dr["CCONTACTO"].ToString();
                        obj.E_MAIL_CONTACTO = dr["E_MAIL_CONTACTO"].ToString();
                        obj.COD_PROVE = dr["COD_PROVE"].ToString();
                        obj.MARCAR = dr["MARCAR"].ToString();
                        obj.NUMERAR = dr["NUMERAR"].ToString();
                        obj.TIPO_CAJA = dr["TIPO_CAJA"].ToString();
                        obj.CATEGORIA = dr["CATEGORIA"].ToString();
                        obj.TAR = dr["TAR"].ToString();
                        obj.precio_referencial_alta = dr["precio_referencial_alta"].ToString();
                        obj.precio_referencial_baja = dr["precio_referencial_baja"].ToString();
                        obj.precio_referencial_especial = dr["precio_referencial_especial"].ToString();
                        obj.CCREDITO = dr["CCREDITO"].ToString();
                        obj.ALFA_CLIENT = dr["ALFA_CLIENT"].ToString();
                        obj.CCONSIGNA = dr["CCONSIGNA"].ToString();
                        obj.CACTIVO = dr["CACTIVO"].ToString();
                        obj.usu_id = dr["usu_id"].ToString();
                        obj.moroso = dr["moroso"].ToString();
                        obj.dia_pago_1 = dr["dia_pago_1"].ToString();
                        obj.dia_pago_2 = dr["dia_pago_2"].ToString();
                        obj.dia_pago_3 = dr["dia_pago_3"].ToString();
                        obj.tipo_identificacion_id = dr["tipo_identificacion_id"].ToString();
                        obj.identificacion = dr["identificacion"].ToString();
                        obj.tipo_cliente_id = dr["tipo_cliente_id"].ToString();
                    }
                    conex.Close();
                }
                catch (Exception ex)
                {
                    conex.Close();
                    conex.Dispose();
                    throw ex;
                }
                finally
                {
                    conex.Close();
                    conex.Dispose();
                }
            }
            return obj;
        }
    }
}
