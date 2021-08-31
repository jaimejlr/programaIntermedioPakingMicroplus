using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.MySQL_DAL
{
  public class FacturaMySQL_DAL
    {
        // Metodo Generado automáticamente Para seleccionar facturas retornando una lista  

        // **** NOTA: Eliminar la última coma antes del from en la sentencia SQL *** 

        public List<FacturasMySqlBL> mtdoSeleccionarTodofacturas(int maxFacturas)
        {
            List<FacturasMySqlBL> listaDatos = new List<FacturasMySqlBL>();
            using (MySqlConnection conex = new MySqlConnection(SettingsConexion.Default.conexionMySql))
            {
                try
                {
                    MySqlCommand cmd = null;
                    string sql = " select " +
                     " INVOICE, " +
                     " Fecha_facturacion, " +
                     " guia_aerea, " +
                     " NUM_PACK, " +
                     " cod_proveedor, " +
                     " OBSERVA, " +
                     " cotizacion, " +
                     " SUCRES, " +
                     " USD, " +
                     " PAGADA, " +
                     " CONTADO, " +
                     " ANULADA, " +
                     " DEBE, " +
                     " FUE, " +
                     " lineaaerea, " +
                     " precio_short, " +
                     " precio_long, " +
                     " NUM_FACTURA, " +
                     " marca, " +
                     " notificar, " +
                     " tipoventa, " +
                     " vendedor, " +
                     " extra, " +
                     " anuladas, " +
                     " f.cod_client, " +
                     " establecimiento, " +
                     " emision, " +
                     " autorizacion, " +
                     " iva, " +
                     " descuento, " +
                     " guia_remision, " +
                     " fautorizacion, " +
                     " generado, " +
                     " ventas, " +
                     " mercado, " +
                     " fecha_vuelo, " +
                     " c.cconsigna as cliente, c.cnombre as subcliente, " +
                     " l.la_codigo as codigoAerolinea "+
                    " from facturas f " +
                    " left outer join clientes c on c.cod_client=f.cod_client " +
                    " left outer join laerea l on f.lineaaerea = l.la_nombre  " +
                    " where INVOICE > @INVOICE  ";
                    cmd = new MySqlCommand(sql, conex);
                    cmd.Parameters.Add("@INVOICE", MySqlDbType.Int32).Value = maxFacturas;
                    MySqlDataReader dr = null;
                    conex.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        FacturasMySqlBL obj = new FacturasMySqlBL();
                        obj.INVOICE = dr["INVOICE"].ToString();
                        obj.Fecha_facturacion = dr["Fecha_facturacion"].ToString();
                        obj.guia_aerea = dr["guia_aerea"].ToString();
                        obj.NUM_PACK = dr["NUM_PACK"].ToString();
                        obj.cod_proveedor = dr["cod_proveedor"].ToString();
                        obj.OBSERVA = dr["OBSERVA"].ToString();
                        obj.cotizacion = dr["cotizacion"].ToString();
                        obj.SUCRES = dr["SUCRES"].ToString();
                        obj.USD = dr["USD"].ToString();
                        obj.PAGADA = dr["PAGADA"].ToString();
                        obj.CONTADO = dr["CONTADO"].ToString();
                        obj.ANULADA = dr["ANULADA"].ToString();
                        obj.DEBE = dr["DEBE"].ToString();
                        obj.FUE = dr["FUE"].ToString();
                        obj.lineaaerea = dr["lineaaerea"].ToString();
                        obj.precio_short = dr["precio_short"].ToString();
                        obj.precio_long = dr["precio_long"].ToString();
                        obj.NUM_FACTURA = dr["NUM_FACTURA"].ToString();
                        obj.marca = dr["marca"].ToString();
                        obj.notificar = dr["notificar"].ToString();
                        obj.tipoventa = dr["tipoventa"].ToString();
                        obj.vendedor = dr["vendedor"].ToString();
                        obj.extra = dr["extra"].ToString();
                        obj.anuladas = dr["anuladas"].ToString();
                        obj.cod_client = dr["cod_client"].ToString();
                        obj.establecimiento = dr["establecimiento"].ToString();
                        obj.emision = dr["emision"].ToString();
                        obj.autorizacion = dr["autorizacion"].ToString();
                        obj.iva = dr["iva"].ToString();
                        obj.descuento = dr["descuento"].ToString();
                        obj.guia_remision = dr["guia_remision"].ToString();
                        obj.fautorizacion = dr["fautorizacion"].ToString();
                        obj.generado = dr["generado"].ToString();
                        obj.ventas = dr["ventas"].ToString();
                        obj.mercado = dr["mercado"].ToString();
                        obj.fecha_vuelo = dr["fecha_vuelo"].ToString();
                        obj.cliente = dr["cliente"].ToString();
                        obj.subcliente = dr["subcliente"].ToString();
                        obj.codigoAerolinea = dr["codigoAerolinea"].ToString();
                        listaDatos.Add(obj);
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
            return listaDatos;
        }


        public List<FacturaDetalleMySQLBL> mtdoSeleccionarDetallefacturas(string numFactura)
        {
            List<FacturaDetalleMySQLBL> listaDatos = new List<FacturaDetalleMySQLBL>();
            using (MySqlConnection conex = new MySqlConnection(SettingsConexion.Default.conexionMySql))
            {
                try
                {
                    MySqlCommand cmd = null;
                    string sql = " Select f.invoice, b.tipo_caja, b.caja, fl.variedad, b.num_tallos as tallos_bunche,  " +
                                 "(b.NUM_TALLOS) as tallos, count(b.NUM_BUNCH) As bunches2, b.precio, (b.num_tallos*b.PRECIO) as valor " +
                                 "From bunche b " +
                                 "inner join flores fl on fl.codigo=mid(b.cod_varie,1,3) " +
                                 "Left join facturas f on f.num_pack=b.num_pack " +
                                 "Where f.invoice = @INVOICE " +
                                 "Group by b.caja,b.tipo_caja, b.cod_varie,b.num_tallos,b.precio ";
                    cmd = new MySqlCommand(sql, conex);
                    cmd.Parameters.Add("@INVOICE", MySqlDbType.VarChar).Value = numFactura;
                    MySqlDataReader dr = null;
                    conex.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        FacturaDetalleMySQLBL obj = new FacturaDetalleMySQLBL();
                        obj.invoice = dr["invoice"].ToString();
                        obj.tipo_caja = dr["tipo_caja"].ToString();
                        obj.caja = dr["caja"].ToString().Replace(",", ".");
                        obj.variedad = dr["variedad"].ToString();
                        obj.tallos_bunche = dr["tallos_bunche"].ToString();
                        obj.tallos = dr["tallos"].ToString().Replace(",", ".");
                        obj.bunches2 = dr["bunches2"].ToString().Replace(",", ".");
                        obj.precio = dr["precio"].ToString();
                        obj.valor = dr["valor"].ToString().Replace(",", ".");

                        listaDatos.Add(obj);
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
            return listaDatos;
        }
    }
}
