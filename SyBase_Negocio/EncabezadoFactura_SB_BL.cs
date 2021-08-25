using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_Negocio
{
   public class EncabezadoFactura_SB_BL
    {
        public string codemp { get; set; } // CODIGO EMPRESA
        public string numfac { get; set; } // NUMERO FACTURA
        public string codven { get; set; } // CODIGO DAE
        public string codalm { get; set; } // CODIGO ALMACEN
        public string codcli { get; set; } // CODIGO CLIENTE
        public string fecfac { get; set; } // FECHA FACTURA
        public string lispre { get; set; } // LISTA DE PRECIO
        public string observ { get; set; } // OBSERVACION
        public int poriva { get; set; } // PORCENTAJE IVA
        public double totnet { get; set; } // TOTAL NETO
        public double totdes { get; set; } // TOTAL DESCUENTO
        public double totbas { get; set; } // TOTAL BASE
        public string totfac { get; set; } // TOTAL FACTURA
        public string fecven { get; set; } // FECHA DE VENCIMIENTO DEUDA FACTURA
        public string conpag { get; set; } // FORMA DE PAGO (C=CREDITO)
        public string tipefe { get; set; } // TIPO PAGO EFECTIVO
        public string valefe { get; set; } // VALOR PAGO EFECTIVO
        public string tipche { get; set; } // TIPO PAGO CHEQUE
        public string numche { get; set; } // NUMERO DE CHEQUE
        public string valche { get; set; } // VALOR CHEQUE
        public string tiptar { get; set; } // TIPO PAGO TARJETA
        public string numtar { get; set; } // NUMERO TARJETA
        public string valtar { get; set; } // VALOR TARJETA
        public string tipdep { get; set; } // TIPO PAGO DEPOSITO
        public string numdep { get; set; } // NUMERO DE DEPOSITO
        public string valdep { get; set; } // VALOR DE DEPOSITO
        public string abofac { get; set; } // ABONO FACTURA
        public string numpag { get; set; } // NUMERO DE PAGOS
        public string plapag { get; set; } // PLAZO DE PAGO EN DIAS
        public string pordes { get; set; } // PORCENTAJE DESCUENTO
        public string tiptra { get; set; } // 
        public string numtra { get; set; } // 
        public string totiva { get; set; } // 
        public string codapu { get; set; } // 
        public string valcot { get; set; } // 
        public string codmon { get; set; } // 
        public string codusu { get; set; } // 
        public string fecult { get; set; } // 
        public string estado { get; set; } // 
        public string nomcli { get; set; } // 
        public string referen { get; set; } // 
        public string codtrans { get; set; } // 
        public string desinv { get; set; } // 
        public string otrcar { get; set; } // 
        public string descuen { get; set; } // 
        public string totaldesc { get; set; } // 
        public string subtot { get; set; } // 
        public string fchche { get; set; } // 
        public string rucced { get; set; } // 
        public string dircli { get; set; } // 
        public string codsec { get; set; } // 
        public string fchdep { get; set; } // 
        public string tipvar { get; set; } // 
        public string basea { get; set; } // 
        public string doctrans { get; set; } // 
        public string tiptrans { get; set; } // 
        public string totret { get; set; } // 
        public string totfac2 { get; set; } // 
        public string totiva2 { get; set; } // 
        public string recibe { get; set; } // 
        public string serie { get; set; } // 
        public string codcajero { get; set; } // 
        public string tipchep { get; set; } // 
        public string valchep { get; set; } // 
        public string excen { get; set; } // 
        public string moivab { get; set; } // 
        public string moivas { get; set; } // 
        public string ser_a { get; set; } // 
        public string ser_b { get; set; } // 
        public string inv_a { get; set; } // 
        public string inv_b { get; set; } // 
        public string estadow { get; set; } // 
        public string codcom { get; set; } // 
        public string numdoc { get; set; } // 
        public string fechaemision { get; set; } // 
        public string fecharegistro { get; set; } // 
        public string tipocomprobante { get; set; } // 
        public string idcliente { get; set; } // 
        public string tpidcli { get; set; } // 
        public string referen2 { get; set; } // 
        public string codveh { get; set; } // 
        public string totice { get; set; } // 
        public string totint { get; set; } // 
        public string porint { get; set; } // 
        public string descargado { get; set; } // 
        public string numgui { get; set; } // 
        public string fecinigui { get; set; } // 
        public string fecfingui { get; set; } // 
        public string coment { get; set; } // 
        public string descargado1 { get; set; } // 
        public string impreso { get; set; } // 
        public string establ { get; set; } // 
        public string totdev { get; set; } // 
        public string facelec { get; set; } // 
        public string codDocModificado { get; set; } // 
        public string numDocModificado { get; set; } // 
        public string fechaEmisionDocSustento { get; set; } // 
        public string estabmodificado { get; set; } // 
        public string ptoemimodificado { get; set; } // 
        public string desguia { get; set; } // 
        public string autorizacion { get; set; } // 
        public string totiva3 { get; set; } // 
        public string claveaccesofe { get; set; } // 
        public string fecautfe { get; set; } // 
        public string msgerrorfe { get; set; } // 
        public string valcom { get; set; } // 
        public string porcentajeiva { get; set; } // 
        public string totirbpn { get; set; } // 
        public string hora { get; set; } // 
        public string cajapc { get; set; } // 
        public string descuadre { get; set; } // 
        public string estadoconta { get; set; } // 
        public string numpro { get; set; } // 
        public string descargapro { get; set; } // 
        public string doctranspro { get; set; } // 
        public string numtradev { get; set; } // 
        public string codsecgui { get; set; } // 
        public string facelecgui { get; set; } // 
        public string claveaccesofegui { get; set; } // 
        public string autorizaciongui { get; set; } // 
        public string fecautfegui { get; set; } // 
        public string serieguia { get; set; } // 
        public string referen3 { get; set; } // 
        public string motivo { get; set; } // 
        public string ptopartida { get; set; } // 
        public string ptollegada { get; set; } // 
        public string idcliguia { get; set; } // 
        public string nomcliguia { get; set; } // 

    }
}
