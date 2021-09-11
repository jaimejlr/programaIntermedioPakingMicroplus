using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.MySQL_Negocio
{
   public class FacturaDetalleMySQLBL
    {
        public string cod_producto { get; set; }
        public string nom_producto { get; set; }
        public string invoice { get; set; }
        public string tipo_caja { get; set; }
        public string caja { get; set; }
        public string variedad { get; set; }
        public string tallos_bunche { get; set; }
        public string tallos { get; set; }
        public string bunches2 { get; set; }
        public string precio { get; set; }
        public string valor { get; set; }
        public string largo { get; set; }
    }
}
