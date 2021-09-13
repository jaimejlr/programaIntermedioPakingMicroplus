using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus
{
    class EnviarCorreoBL
    {
        public static string email { get; set; }
        public static string nombreEmail { get; set; }
        public static string hosts { get; set; }
        public static int puerto { get; set; }
        public static bool ssl { get; set; }
        public static string passwordEmail { get; set; }
        public static string emailDestinatario { get; set; }
        public static string NombreDestinatario { get; set; }
        public static string CorreoDeRespuesta { get; set; }

        public static bool EnvioCorreo { get; set; } //si es si se envia los correos caso contrario no
        public static bool CopiarA_jefeSuperior { get; set; } //si es si se envia los correos con copia al jefe caso contrario no
        public static bool MostrarNotificaciones { get; set; } //si es si se emuestran las notificaciones en la web
        public static bool GuardarBitacora { get; set; } //si es si se guardan las bitacoras de envios
        public static string Para { get; set; } 
    }
}
