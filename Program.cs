using System;
using System.Collections.Generic;
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
            // crear tabla para guadar las facturas que se realizó la migración de mysql a sybase

            // seleccionar el máximo id de factura del sistema guardado en la tabla log
            SeleccionarDatosSybaseDAL dalSyBase = new SeleccionarDatosSybaseDAL();
            String maximoFactura = dalSyBase.mtdoSeleccionarMaximaFacturaMYSQL();
            // consultar las facturas mayores al máximo id guardado en la tabla log

            FacturaMySQL_DAL dalFacturasMySQL = new FacturaMySQL_DAL();
            List<FacturasMySqlBL> listaFacturasMySQL = dalFacturasMySQL.mtdoSeleccionarTodofacturas(Convert.ToInt32(maximoFactura));
            foreach (var item in listaFacturasMySQL)
            {
                EncabezadoFactura_SB_BL obj = new EncabezadoFactura_SB_BL();
                obj.codemp = NumeroFacturaSiguienteDAL.seleccionarCodigoEmpresa();
                obj.numfac = NumeroFacturaSiguienteDAL.seleccionarSiguienteFactura();
                obj.codven = "";   // varchar(5) en packin es mas de 10
                obj.codalm = "01";
                obj.codcli = ClienteSyBase_DAL.insertarClienteSyBase(ClienteMySQL_DAL.mtdoSeleccionarTodoclientes(item.cod_client));
                obj.fecfac = item.Fecha_facturacion;
                obj.lispre = "1";
                obj.observ = "INVOICE No. " + item.INVOICE + " - PACKING No. " + item.NUM_PACK;
                obj.poriva = 1;
                obj.totnet = 0;
                obj.totdes = Convert.ToDouble(item.descuento =="" ? "0": item.descuento);
                obj.totbas = 0;
                obj.conpag = "C";
                obj.tipefe = "X";
                obj.nomcli = item.cliente;
                obj.referen = item.INVOICE;
                obj.codtrans =  item.codigoAerolinea;
                obj.claveaccesofegui = item.guia_aerea;
                // obj.autorizacion = // guía hija
                obj.totfac = item.USD;
                obj.desinv = "S";
                insertarFacturaSybase_DAL.insertarEncabezadoFacturaSyBase(obj);
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
