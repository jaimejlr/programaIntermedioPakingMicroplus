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
                // CREAR COMO CLIENTE AL CLIENTE PADRE

                // CREAR COMO CLASE AL CLIENTE PADRE, PERO EL CÓDIGO DE CLASE TIENE QUE SER LOS 5 PRIMEROS CARACTERES DEL NOMBRE
               // obj.codcla = // CÓDIGO DE CLASE GENERADO.
                // cliente hijo
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
                    ArticuloSyBase_BL articuloBL = new ArticuloSyBase_BL();

                    articuloBL.nomart = detalle.variedad;
                    articuloBL.codart = detalle.variedad;
                    articuloBL.codiva = "0";
                    articuloBL.coduni = "UND";

                    objDet.codart = ArticuloSyBase_DAL.insertarArticuloSyBase(articuloBL); // generar código del artículo.
                    objDet.nomart = detalle.variedad;
                    objDet.coduni = "UND";//detalle.tipo_caja; //hb
                    objDet.cantid = Convert.ToDecimal(detalle.tallos.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));  // o tallos bunche
                    objDet.preuni = Convert.ToDecimal(detalle.precio.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR")); //Convert.ToDouble(detalle.precio);  // o tallos bunche
                    objDet.totren = Convert.ToDecimal(detalle.valor.ToString().Replace('.',','), CultureInfo.CreateSpecificCulture("fr-FR")); //Convert.ToDouble(detalle.valor);  // valor total
                    objDet.codiva = "0"; 
                    objDet.codmon = "01";
                    objDet.totext = Convert.ToDecimal(detalle.valor.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));  // valor total
                    objDet.codmed = detalle.tipo_caja;
                    objDet.cajas = Convert.ToDecimal(detalle.caja.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR")); 

                    insertarFacturaSybase_DAL.insertarDetalleFacturaSyBase(objDet);


                    // insertar en kardex
                    Karde_SyBase_BL objKardex = new Karde_SyBase_BL();
                    objKardex.tiporg = "BOD";
                    objKardex.numdoc = obj.numfac;
                    objKardex.codart = objDet.codart;
                    objKardex.tipdoc = "EN";
                    objKardex.codalm = "01";
                    objKardex.coduni = "UND";

                   

                    objKardex.fecdoc = fecha3;
                    objKardex.cantot = objDet.cantid.ToString();
                    objKardex.cosuni = objDet.preuni.ToString();
                    objKardex.costot = (objDet.cantid * objDet.preuni).ToString();
                    objKardex.totven = "0";
                    objKardex.codcli = obj.codcli;
                    objKardex.codven = obj.codven;
                    objKardex.codusu = "TEAMPLUS";
                    objKardex.fecult = fecha3;
                    objKardex.feccad = null;
                    objKardex.cancaja = objDet.cajas.ToString();
                    objKardex.numren = contadorReglon;
                    //objKardex.hora = null;
                   // objKardex.numsec = null;
                    //objKardex.coditem = null;
                    //objKardex.canitem = null;
                    //objKardex.feccie = null;
                    objKardex.establ = "001";
                    //objKardex.codcla = null;
                    //objKardex.codcolor = null;
                    objKardex.referen = "MIGRACION";

                    karde_SyBase_DAL.insertarKardex(objKardex);

                    Karde_SyBase_BL objKardexSalida = new Karde_SyBase_BL();
                    objKardex.tiporg = "FAC";
                    objKardex.numdoc = obj.numfac;
                    objKardex.codart = objDet.codart;
                    objKardex.tipdoc = "SA";
                    objKardex.codalm = "01";
                    objKardex.coduni = "UND";
                    objKardex.fecdoc = funcionesEspeciales.convertirFecha(obj.fecfac); 
                    objKardex.cantot = objDet.cantid.ToString();
                    objKardex.cosuni = objDet.preuni.ToString();
                    objKardex.costot = (objDet.cantid * objDet.preuni).ToString();
                    objKardex.totven = "0";
                    objKardex.codcli = obj.codcli;
                    objKardex.codven = obj.codven;
                    objKardex.codusu = "TEAMPLUS";
                    objKardex.fecult = null;
                    objKardex.feccad = null;
                    objKardex.cancaja = objDet.cajas.ToString();
                    objKardex.numren = contadorReglon;
                    objKardex.hora = null;
                    objKardex.numsec = null;
                    objKardex.coditem = null;
                    objKardex.canitem = null;
                    objKardex.feccie = null;
                    objKardex.establ = "001";
                    objKardex.codcla = null;
                    objKardex.codcolor = null;
                    objKardex.referen = "MIGRACION";

                    karde_SyBase_DAL.insertarKardex(objKardex);

                    contadorReglon++;

                }


                // insertar en cuentas por cobrar


                CuentasPorCobrar_SyBase_BL objCxc = new CuentasPorCobrar_SyBase_BL();
                objCxc.numcpc = NumeroFacturaSiguienteDAL.seleccionarSiguienteSecuencialCXC();
                objCxc.tipdoc = "FC";
                objCxc.numtra = obj.numfac;
                objCxc.codcli = obj.codcli;
                objCxc.codven = obj.codven;
                objCxc.fecemi = obj.fecfac;
               // objCxc.fecven = "";
               // objCxc.fectra = obj.fecfac;
                objCxc.concep = obj.numfac;
                objCxc.valcob = Convert.ToDecimal(item.USD, CultureInfo.CreateSpecificCulture("fr-FR"));
                objCxc.tiporg = "FAC";
                objCxc.numorg = obj.numfac;
                objCxc.codapu = "FC" + obj.numfac.Substring(1, 7);
                objCxc.codap1 = "FC";
                objCxc.codmon = "01";
                objCxc.codusu = "MIGRACIONSM";
                // convertir de fecha 18/8/2021 a 2021-08-18
                var fecha = obj.fecfac.Split(' ');
                var fecha1 = fecha[0].Split('/');
                int dia = Convert.ToInt32(fecha1[0]);
                int mes = Convert.ToInt32(fecha1[1]);
                int anio = Convert.ToInt32(fecha1[2]);

                var dia2 = "";
                dia2 = dia.ToString();
                if (dia < 10 && dia.ToString().Length < 2)
                    dia2 = "0" + dia;

                var mes2 = "";
                mes2 = mes.ToString();
                if (mes < 10 && mes.ToString().Length < 2)
                    mes2 = "0" + mes;

                var fecha3 = anio + "-" + mes2 + "-" + dia2;
                objCxc.fecult = funcionesEspeciales.convertirFecha(obj.fecfac);
                objCxc.referen = obj.referen;


                CuentasPorCobrarSyBase_DAL.insertarCuentasPorCobrarSyBase(objCxc);

               


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
