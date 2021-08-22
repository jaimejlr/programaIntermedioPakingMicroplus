using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProgramaIntermedioPackinMicroplus.MySQL_DAL;
using ProgramaIntermedioPackinMicroplus.SyBase_DAL;

namespace ProgramaIntermedioPackinMicroplus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("inicio");

            ClienteMySQL_DAL dalC = new ClienteMySQL_DAL();

            ClienteSyBase_DAL dalCM = new ClienteSyBase_DAL();

            dalCM.insertarClienteFacturaSyBase(dalC.mtdoSeleccionarTodoclientes());
            //if(dal.ProvarConexion())
            //{
            //    Console.WriteLine("CONEXION MYSQL CORRECTO");
            //}
            //else
            //{
            //    Console.WriteLine("ERROR CONEXION MYSQL");
            //}

            SeleccionarDatosSybaseDAL dalSb = new SeleccionarDatosSybaseDAL();
            if (dalSb.probarConexionSybase())
            {
                Console.WriteLine("CONEXION SYBASE CORRECTO");
            }
            else
            {
                Console.WriteLine("ERROR CONEXION SYBASE");
            }


            // insertar facura en sybase
            string resultado = dalSb.insertarEncabezadoFacturaSyBase();
            Console.WriteLine("Facturas insertadas: "+ resultado);

            Console.ReadKey();
        }
    }
}
