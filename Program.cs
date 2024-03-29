﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL;
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

            ParametrosGeneralesDAL parametrosDAL = new ParametrosGeneralesDAL();
            parametrosDAL.mtdoSeleccionarDatosEmail();

          //  enviarCorreo.EnvioEmail(EnviarCorreoBL.Para, "", "", EnviarCorreoBL.nombreEmail, "ERRO MIGRACION " + numerosFacturas.lm_factura_mysql + " " + numerosFacturas.lm_factura_sybase, "Prueba de envío mail", "", "");

            numerosFacturas.lm_cadena_conexion_MySQL = parametrosDAL.mtdoSeleccionarDatosCadenaConexion();

            String numeroFacturaSybase = "";

            Console.WriteLine("inicio");


            Console.WriteLine("Ingrese el número de Inicio del invoice");
            int invoceInicio = Int32.Parse(Console.ReadLine());


            Console.WriteLine("Ingrese el número fin del invoice");
            int invoceFin = Int32.Parse(Console.ReadLine());

            FacturaMySQL_DAL dalFacturasMySQL = new FacturaMySQL_DAL();


            // crear tabla para guadar las facturas que se realizó la migración de mysql a sybase

            //Console.WriteLine("***Seleccionando el numero máximo de factura***");
            //// seleccionar el máximo id de factura del sistema guardado en la tabla log
            //String maximoFactura = SeleccionarDatosSybaseDAL.mtdoSeleccionarMaximaFacturaMYSQL();
            
            //// consultar las facturas mayores al máximo id guardado en la tabla log
            //Console.WriteLine("Número de factura mysql: "+ maximoFactura);
            //insertar el detalle de la factura.


            List<FacturasMySqlBL> listaFacturasMySQL = dalFacturasMySQL.mtdoSeleccionarTodofacturas(Convert.ToInt32(invoceInicio), Convert.ToInt32(invoceFin));
            foreach (var item in listaFacturasMySQL)
            {
                numerosFacturas.lm_factura_mysql = item.INVOICE;

                Console.WriteLine(" ");
                Console.WriteLine(" ......**********......");
                Console.WriteLine(" ");
                EncabezadoFactura_SB_BL obj = new EncabezadoFactura_SB_BL();
                obj.codemp = NumeroFacturaSiguienteDAL.seleccionarCodigoEmpresa();

                numeroFacturaSybase = NumeroFacturaSiguienteDAL.seleccionarSiguienteFactura();




                obj.numfac = "F" + numeroFacturaSybase.PadLeft(8, '0');

                // para migrar con facturació electrónica

               // obj.numfac = item.NUM_FACTURA;
               // obj.claveaccesofe = item.autorizacion;


                numerosFacturas.lm_factura_sybase = obj.numfac;
                Console.WriteLine("..........Guardar en log");
                SeleccionarDatosSybaseDAL.insertarLogMigracionFactura(item.INVOICE, obj.numfac, "PENDIENTE", "");

                Console.WriteLine("Seleccionar numero siguiente de facutura: "+ obj.numfac  +"  --- Número invoice: "+ item.INVOICE);
                // con los datos del dae insertar el vendedor y el id se debe insertar en codven  *********
                var datosDae= DaeDAL.mtdoSeleccionarTodofue(item.FUE);
                
                Console.WriteLine("Seleccionar datos DAE: " + datosDae.fue_pais + " " + datosDae.fue_caduca);
                VendedorSybase_BL vendedor = new VendedorSybase_BL();
                vendedor.nomven = datosDae.fue_pais + " " + datosDae.fue_caduca;
                vendedor.codusu = "MIGRACION";
                vendedor.codzona = datosDae.codigoPais;
                vendedor.direcven = item.FUE;
                vendedor.codcla = "003";
                // varchar(5) en packin dae es mas de 10    055-2021-40-12345678  EEUU-31-AGO-21
                obj.codven = VendedorSyBase_DAL.insertarVendedoresSyBase(vendedor);
                Console.WriteLine("Seleccionar COD VEN: " + obj.codven);
                obj.codalm = "01"; // bodega estática

                // cliente hijo
                Console.WriteLine("Gestionar clientes");
                ClienteMySqlBL clientehijo = new ClienteMySqlBL();
                clientehijo = ClienteMySQL_DAL.mtdoSeleccionarTodoclientes(item.cod_client);
                var codClaseCliente = ClienteSyBase_DAL.insertarClaseClienteSyBase(clientehijo);
                if(clientehijo.ALFA_CLIENT == "" || clientehijo.ALFA_CLIENT == null )
                {
                  obj.codcli = ClienteSyBase_DAL.insertarClienteSyBase(clientehijo, codClaseCliente, datosDae.codigoPais);

                }
                else if (ClienteSyBase_DAL.comparaSiEisteClienteSyBase(clientehijo.ALFA_CLIENT))
                {
                    obj.codcli = clientehijo.ALFA_CLIENT;
                }
                else
                {
                    obj.codcli = ClienteSyBase_DAL.insertarClienteSyBase(clientehijo, codClaseCliente, datosDae.codigoPais);
                }

                obj.rucced = clientehijo.identificacion;
                obj.idcliente = clientehijo.identificacion;
                obj.idcliguia = clientehijo.identificacion;
                obj.nomcliguia = clientehijo.CNOMBRE;
                obj.dircli = clientehijo.DIRECCION;
                // CREAR COMO CLIENTE AL CLIENTE PADRE
                // CREAR COMO CLASE AL CLIENTE PADRE, PERO EL CÓDIGO DE CLASE TIENE QUE SER LOS 5 PRIMEROS CARACTERES DEL NOMBRE
                //obj.codcla = ClienteSyBase_DAL.insertarClienteSyBase(ClienteMySQL_DAL.mtdoSeleccionarTodoclientes(clientehijo.CCONSIGNA));// CÓDIGO DE CLASE GENERADO.
                obj.fecfac = item.Fecha_facturacion;
                obj.lispre = "1";
                obj.observ = "INVOICE No. " + item.INVOICE + " - PACKING No. " + item.NUM_PACK;
                obj.poriva = (Convert.ToDouble(item.extra == "" ? "0" : item.extra) > 0 ? 1 : 0); // si en extra >0 cambia a 1 ****
                obj.totnet = Convert.ToDecimal(item.USD.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                obj.excen = Convert.ToDecimal(item.USD.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                obj.inv_b = Convert.ToDecimal(item.USD.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                obj.totdes = Convert.ToDecimal((item.descuento =="" ? "0": item.descuento).ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                obj.totbas = 0; //Convert.ToDouble(item.USD);
                obj.abofac = "X";
                obj.conpag = "C";
                obj.tipefe = "X";
                obj.nomcli = clientehijo.CNOMBRE;
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
                
                obj.totfac = Convert.ToDecimal(item.USD.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                obj.desinv = "S"; // desceunta en inventario
                obj.codapu = "FCF0" + obj.numfac.Substring(1, 7);
                //**************debe ser igual a la serie de la secuencia F0, viene de la tabla tiposecuencias. La consulta es  (     SELECT serie FROM "dbo"."tiposecuencias" where codsec = 'F0' and codemp = '01' and tiposec = 'VC_FAC'  )
                obj.serie = NumeroFacturaSiguienteDAL.seleccionarSiguienteSerieParaEncabezadoFactura();
                obj.numgui = obj.numfac;
                obj.establ = "001";

                Console.WriteLine("Insertar encabezado factura");
                insertarFacturaSybase_DAL.insertarEncabezadoFacturaSyBase(obj);

                List<FacturaDetalleMySQLBL> listaDetalleFacturasMySQL = dalFacturasMySQL.mtdoSeleccionarDetallefacturas(item.INVOICE);

                // insertar el detalle de la factura en sybase
                int contadorReglon = 1;
                int codCen2 = 0;
                foreach (var detalle in listaDetalleFacturasMySQL)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("..........Reglones facura de la factura: "+ obj.numfac);
                    DetalleFacturaSyBaseBL objDet = new DetalleFacturaSyBaseBL();
                    //System.IFormatProvider cultureUS =  new System.Globalization.CultureInfo("en-US");

                    //CultureInfo culture = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name, true);
                    //culture.NumberFormat.NumberDecimalSeparator = ".";
                    //System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                    //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                    objDet.codemp = obj.codemp;
                    objDet.numfac = obj.numfac;
                    objDet.numren = contadorReglon;
                    //objDet.numite = Convert.ToInt32(detalle.caja);
                    objDet.numite = Convert.ToInt32(detalle.bunches2);
                    // generar el código de artículos
                    ArticuloSyBase_BL articuloBL = new ArticuloSyBase_BL();

                    articuloBL.nomart = detalle.nom_producto;
                    articuloBL.codart = detalle.cod_producto;
                    articuloBL.codiva = "0";
                    articuloBL.coduni = "UND";
                    articuloBL.deta02 = "ROSAS";
                    articuloBL.deta03 = "ROSES";
                    articuloBL.deta04 = "ROSAEA";
                    articuloBL.deta05 = "0603.11.0060"; //CODIGO ATPA (CÓDIGO PARA ADUANA USA)
                    articuloBL.deta06 = "0603.11.00.00"; // CODIGO REGION ANDINA(CÓDIGO PARA REGIÓN LATINOAMÉRICA)
                    articuloBL.deta07 = detalle.largo; //LARGO EN CENTIMETROS DE LA FLOR

                    objDet.codart = ArticuloSyBase_DAL.insertarArticuloSyBase(articuloBL); // generar código del artículo.
                    objDet.nomart = detalle.nom_producto;
                    objDet.coduni = "UND";//detalle.tipo_caja; //hb
                    objDet.cantid = Convert.ToDecimal(detalle.tallos.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));  // o tallos bunche
                    objDet.cantid = objDet.numite * Convert.ToInt32(detalle.tallos_bunche);
                     objDet.preuni = Convert.ToDecimal(detalle.precio.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR")); //Convert.ToDouble(detalle.precio);  // o tallos bunche
                    objDet.totren = Convert.ToDecimal(detalle.valor.ToString().Replace('.',','), CultureInfo.CreateSpecificCulture("fr-FR")); //Convert.ToDouble(detalle.valor);  // valor total
                    objDet.totren = objDet.cantid * objDet.preuni;
                     objDet.codiva = "0"; 
                    objDet.codmon = "01";
                    objDet.totext = Convert.ToDecimal(detalle.valor.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));  // valor total
                    objDet.codmed = detalle.tipo_caja;
                    // Convert.ToDecimal(detalle.caja.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                    objDet.desren = 0;
                    objDet.valcot = 1;
                    objDet.fecfac = funcionesEspeciales.convertirFecha(obj.fecfac);
                    objDet.excen = objDet.cantid * objDet.preuni;
                    objDet.valcargo = Convert.ToDecimal(detalle.bunches2.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                    objDet.codcli = obj.codcli;
                    objDet.codven = obj.codven;
                    //*************************************************************************************
                    var codcen = "";
                    if (Convert.ToInt32(detalle.caja) < 10)
                        codcen = "0" + Convert.ToInt32(detalle.caja);
                    else
                        codcen =Convert.ToInt32(detalle.caja).ToString();

                    var auxiliarCodCen = 0;


                    ///--------------------------
                    ///
                    tmpartcpa_SyBase_BL tmpartcpa = new tmpartcpa_SyBase_BL();
                    tmpartcpa.numfac = obj.numfac;
                    tmpartcpa.codart = objDet.codart;
                    tmpartcpa.nomart = objDet.nomart;
                    tmpartcpa.numren = objDet.numren.ToString();
                    tmpartcpa.codpro = objDet.codcli;
                    tmpartcpa.peso = "1";
                    tmpartcpa.tiporg = "FAC";


                    if (codCen2 == 0)
                    {
                     objDet.cajas = 1;
                        tmpartcpa_SyBase_DAL.insertartmpartcpaSyBase(tmpartcpa);
                    }
                    else if(codCen2 == Convert.ToInt32(detalle.caja))
                    {
                        objDet.cajas = 0;
                    }
                    else
                    {
                        objDet.cajas = 1;
                        tmpartcpa_SyBase_DAL.insertartmpartcpaSyBase(tmpartcpa);
                    }
                    codCen2 = Convert.ToInt32(detalle.caja);

                    objDet.codcen = codcen;
                    objDet.seriesdoc = detalle.tallos_bunche.ToString();
                    objDet.codfun = "";
                    objDet.codfun = funcionarioSybase_DAL.insertarfuncinario(detalle.subclien);

                    insertarFacturaSybase_DAL.insertarDetalleFacturaSyBase(objDet);





                    Console.WriteLine(" ");
                    Console.WriteLine("..........Kardex de entrada");
                    // insertar en kardex
                    Karde_SyBase_BL objKardex = new Karde_SyBase_BL();
                    objKardex.tiporg = "BOD";
                    objKardex.numdoc = obj.numfac;
                    objKardex.codart = objDet.codart;
                    objKardex.tipdoc = "EN";
                    objKardex.codalm = "01";
                    objKardex.coduni = "UND";

                   

                    objKardex.fecdoc = funcionesEspeciales.convertirFecha(obj.fecfac); 
                    objKardex.cantot = objDet.cantid.ToString();
                    objKardex.cosuni = objDet.preuni.ToString();
                    objKardex.costot = (objDet.cantid * objDet.preuni).ToString();
                    objKardex.totven = objKardex.costot;
                    objKardex.codcli = obj.codcli;
                    objKardex.codven = obj.codven;
                    objKardex.codusu = "MIGRACION";
                    objKardex.fecult = funcionesEspeciales.convertirFecha(obj.fecfac); ;
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


                    Console.WriteLine("..........Kardex de salida");
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
                    objKardex.codusu = "MIGRACION";
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
                Console.WriteLine(" ");
                Console.WriteLine("..........Cuentas por cobrar de la factura: " + obj.numfac);
                CuentasPorCobrar_SyBase_BL objCxc = new CuentasPorCobrar_SyBase_BL();
                objCxc.numcpc = NumeroFacturaSiguienteDAL.seleccionarSiguienteSecuencialCXC();
                objCxc.tipdoc = "FC";
                objCxc.numtra = obj.numfac;
                objCxc.codcli = obj.codcli;
                objCxc.codven = obj.codven;
                objCxc.fecemi = funcionesEspeciales.convertirFecha(obj.fecfac);
                // objCxc.fecven = "";
                 objCxc.fectra = funcionesEspeciales.convertirFecha(obj.fecfac);
                objCxc.concep = "Cargo a su cuenta sg FC " + obj.numfac;
                objCxc.valcob = Convert.ToDecimal(item.USD.ToString().Replace('.', ','), CultureInfo.CreateSpecificCulture("fr-FR"));
                objCxc.tiporg = "FAC";
                objCxc.numorg = obj.numfac;
                objCxc.codapu = "FCF0" + obj.numfac.Substring(1, 7);
                objCxc.codap1 = "FC"+ objCxc.numcpc;
                objCxc.codmon = "01";
                objCxc.codusu = "MIGRACION";
                objCxc.valcot = 1;
                objCxc.fecfac = funcionesEspeciales.convertirFecha(obj.fecfac);
                // convertir de fecha 18/8/2021 a 2021-08-18

                objCxc.fecult = funcionesEspeciales.convertirFecha(obj.fecfac);
                objCxc.referen = obj.referen;
                objCxc.serie = obj.serie;
                CuentasPorCobrarSyBase_DAL.insertarCuentasPorCobrarSyBase(objCxc);

                Console.WriteLine("..........Guardar en log");
                SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "FINALIZADO", "");
            }


            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("-                   Migración finalizada                                                    -");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.ReadKey();




            // insertar las facturas en sybase




            //  Console.WriteLine("inicio");




            //if(dal.ProvarConexion())
            //{
            //    Console.WriteLine("CONEXION MYSQL CORRECTO");
            //}
            //else
            //{
            //    Console.WriteLine("ERROR CONEXION MYSQL");
            //}

            //if (dalSyBase.probarConexionSybase())
            //{
            //    Console.WriteLine("CONEXION SYBASE CORRECTO");
            //}
            //else
            //{
            //    Console.WriteLine("ERROR CONEXION SYBASE");
            //}


            //// insertar facura en sybase
            //string resultado = dalSyBase.insertarEncabezadoFacturaSyBase();
            //Console.WriteLine("Facturas insertadas: "+ resultado);

            // 
        }


       
    }
}
