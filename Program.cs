using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL_DAL;
using ProgramaIntermedioPackinMicroplus.MySQL_Negocio;
using ProgramaIntermedioPackinMicroplus.SyBase_DAL;
using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;

namespace ProgramaIntermedioPackinMicroplus
{
    class Program
    {
        static void Main(string[] args)
        {
            FacturaMySQL_DAL dalFacturasMySQL = new FacturaMySQL_DAL();

          
            // crear tabla para guadar las facturas que se realizó la migración de mysql a sybase

            // seleccionar el máximo id de factura del sistema guardado en la tabla log
            SeleccionarDatosSybaseDAL dalSyBase = new SeleccionarDatosSybaseDAL();
            String maximoFactura = dalSyBase.mtdoSeleccionarMaximaFacturaMYSQL();
            // consultar las facturas mayores al máximo id guardado en la tabla log

            //insertar el detalle de la factura.

            
            List<FacturasMySqlBL> listaFacturasMySQL = dalFacturasMySQL.mtdoSeleccionarTodofacturas(Convert.ToInt32(maximoFactura));
            foreach (var item in listaFacturasMySQL)
            {
                EncabezadoFactura_SB_BL obj = new EncabezadoFactura_SB_BL();
                obj.codemp = NumeroFacturaSiguienteDAL.seleccionarCodigoEmpresa();
                obj.numfac = NumeroFacturaSiguienteDAL.seleccionarSiguienteFactura();
                // con los datos del dae insertar el vendedor y el id se debe insertar en codven  *********
               var datosDae= DaeDAL.mtdoSeleccionarTodofue(item.FUE);
                VendedorSybase_BL vendedor = new VendedorSybase_BL();
                vendedor.nomven = datosDae.fue_pais + " " + datosDae.fue_caduca;
                // varchar(5) en packin dae es mas de 10    055-2021-40-12345678  EEUU-31-AGO-21
                obj.codven = VendedorSyBase_DAL.insertarVendedoresSyBase(vendedor);
                obj.codalm = "01"; // bodega estática
                obj.codcli = ClienteSyBase_DAL.insertarClienteSyBase(ClienteMySQL_DAL.mtdoSeleccionarTodoclientes(item.cod_client));
                obj.fecfac = item.Fecha_facturacion;
                obj.lispre = "1";
                obj.observ = "INVOICE No. " + item.INVOICE + " - PACKING No. " + item.NUM_PACK;
                obj.poriva = (Convert.ToDouble(item.extra == "" ? "0" : item.extra) > 0 ? 1 : 0); // si en extra >0 cambia a 1 ****
                obj.totnet = Convert.ToDouble(item.USD); 
                obj.totdes = Convert.ToDouble(item.descuento =="" ? "0": item.descuento);
                obj.totbas = Convert.ToDouble(item.USD);
                obj.conpag = "C";
                obj.tipefe = "X";
                obj.nomcli = item.cliente;
                obj.referen = item.INVOICE;
               var proveedores = ProveedorMysql_DAL.mtdoSeleccionarTodoproveedores(item.cod_proveedor);
                // insertar en datosvehículos para obtener un código del mismo *****
                DatosVehiculosSyBase_BL datosVehiculo = new DatosVehiculosSyBase_BL();
                datosVehiculo.codveh = proveedores.COD_PROVE;
                datosVehiculo.nomveh = proveedores.NOMBRE;

                obj.codveh = DatosVehiculosSyBase_DAL.insertarVehiculoSyBase(datosVehiculo);   //

                // insertar en la tabla transportista la aerolinea tabla mysql laerea
                transportistaSyBase_BL transportista = new transportistaSyBase_BL();
                transportista.codtrans = item.codigoAerolinea;
                transportista.nomtrans = item.lineaaerea;

                obj.codtrans = transportistaSyBase_DAL.insertarTransportistaSyBase(transportista);
                obj.claveaccesofegui = item.guia_aerea;
                // obj.autorizacion = // guía hija
                obj.totfac = item.USD;
                obj.desinv = "S"; // desceunta en inventario
                insertarFacturaSybase_DAL.insertarEncabezadoFacturaSyBase(obj);

                List<FacturaDetalleMySQLBL> listaDetalleFacturasMySQL = dalFacturasMySQL.mtdoSeleccionarDetallefacturas(item.INVOICE);

                // insertar el detalle de la factura en sybase
                int contadorReglon = 1;
                foreach (var detalle in listaDetalleFacturasMySQL)
                {
                    DetalleFacturaSyBaseBL objDet = new DetalleFacturaSyBaseBL();
                    //System.IFormatProvider cultureUS =  new System.Globalization.CultureInfo("en-US");

                    //CultureInfo culture = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name, true);
                    //culture.NumberFormat.NumberDecimalSeparator = ".";
                    //System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                    //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                    objDet.codemp = obj.codemp;
                    objDet.numfac = obj.numfac;
                    objDet.numren = contadorReglon;
                    objDet.numite = Convert.ToInt32(detalle.caja);

                    // generar el código de artículos


                    objDet.codart = "TIF70"; // generar código del artículo.
                    objDet.nomart = detalle.variedad;
                    objDet.coduni = "UND";//detalle.tipo_caja; //hb
                    objDet.cantid = Convert.ToDecimal(detalle.tallos, CultureInfo.CreateSpecificCulture("fr-FR"));  // o tallos bunche
                    objDet.preuni = Convert.ToDecimal(detalle.precio, CultureInfo.CreateSpecificCulture("fr-FR")); //Convert.ToDouble(detalle.precio);  // o tallos bunche
                    objDet.totren = Convert.ToDecimal(detalle.valor, CultureInfo.CreateSpecificCulture("fr-FR")); //Convert.ToDouble(detalle.valor);  // valor total
                    objDet.codiva = "0"; 
                    objDet.codmon = "01";
                    objDet.totext = Convert.ToDecimal(detalle.valor, CultureInfo.CreateSpecificCulture("fr-FR"));  // valor total
                    objDet.codmed = detalle.tipo_caja;
                    objDet.cajas = Convert.ToDecimal(detalle.caja, CultureInfo.CreateSpecificCulture("fr-FR")); 

                    insertarFacturaSybase_DAL.insertarDetalleFacturaSyBase(objDet);


                     contadorReglon++;

                }
            
            
            }



             

           

            // insertar las facturas en sybase




            Console.WriteLine("inicio");

         

            
            //if(dal.ProvarConexion())
            //{
            //    Console.WriteLine("CONEXION MYSQL CORRECTO");
            //}
            //else
            //{
            //    Console.WriteLine("ERROR CONEXION MYSQL");
            //}

            if (dalSyBase.probarConexionSybase())
            {
                Console.WriteLine("CONEXION SYBASE CORRECTO");
            }
            else
            {
                Console.WriteLine("ERROR CONEXION SYBASE");
            }


            // insertar facura en sybase
            string resultado = dalSyBase.insertarEncabezadoFacturaSyBase();
            Console.WriteLine("Facturas insertadas: "+ resultado);

            Console.ReadKey();
        }
    }
}
