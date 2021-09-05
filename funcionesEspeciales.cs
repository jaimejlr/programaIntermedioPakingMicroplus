using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus
{
    public class funcionesEspeciales
    {
        public static string convertirFecha(String fechaOriginal)
        {

            // convertir de fecha 18/8/2021 a 2021-08-18
            var fecha = fechaOriginal.Split(' ');
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
            return fecha3;
        }
    }
}
