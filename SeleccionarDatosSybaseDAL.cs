using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus
{
    public class SeleccionarDatosSybaseDAL
    {
        public bool probarConexionSybase()
        {
            bool resultado = false;
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    connection.Open();
                    connection.Close();
                    connection.Dispose();

                    resultado = true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return resultado;
        }

        public string insertarEncabezadoFacturaSyBase()
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    String SQL_query = "INSERT INTO encabezadofacturas " +
                         "(codemp, numfac,    codven,codalm,codcli, fecfac,                  lispre, observ, poriva,  totnet,  totdes, totbas, totfac,  fecven,                   conpag, tipefe, valefe, tipche,numche,  valche, tiptar,   "+" numtar, valtar,   tipdep, numdep,  valdep, abofac, numpag, plapag, pordes,  tiptra,   numtra,  totiva,  codapu,      valcot ,codmon, codusu,    fecult,                    estado,   nomcli,            referen,  codtrans, desinv, otrcar, " + "descuen,totaldesc,subtot, fchche, rucced,         dircli, codsec,  fchdep,tipvar, basea,  doctrans,tiptrans, totret, totfac2, totiva2,  recibe,   serie,     codcajero,   tipchep, valchep,  excen,  moivab, moivas, ser_a,       ser_b,    "+"  inv_a,      inv_b,       estadow,  codcom,  numdoc, fechaemision,  fecharegistro, tipocomprobante, idcliente,       tpidcli, referen2, codveh, totice,  totint,  porint,  descargado, numgui, fecinigui, fecfingui, coment, descargado1, impreso, establ, totdev, facelec, "+" codDocModificado, numDocModificado ,fechaEmisionDocSustento,estabmodificado,ptoemimodificado,desguia,autorizacion,totiva3 ,claveaccesofe  ,fecautfe ,msgerrorfe ,valcom   ,porcentajeiva   ,totirbpn,  "+"   hora         ,cajapc  ,descuadre  ,estadoconta  ,numpro  ,descargapro ,doctranspro ,numtradev ,codsecgui ,facelecgui ,claveaccesofegui  ,autorizaciongui,fecautfegui  ,serieguia ,referen3 ,motivo  ,ptopartida  ,ptollegada   ,idcliguia  ,nomcliguia)   " +
                         " VALUES "+
                         " ('01',  'E0000866','00004', '01', '00355', '2021-08-16 00:00:00.000','1',     NULL    ,1,     22.3200, 0.0000,  0.0000, 25.00,  '2021-08-16 00:00:00.000', 'C',     'X'    ,0.0000,'X',   NULL,    0.0000, 'X',     " + " NULL,  0.0000,    'X',      NULL,  0.0000,  'A'    ,1     , 0    ,  0.0000,   NULL    ,NULL,    2.6800, 'FCE0000866',1.0000, '01',   'TEAMPLUS','2021-08-16 00:00:00.000','P',       'CONSUMIDOR FINAL', NULL,     NULL,     'S',   0.0000, " + " NULL,  0.0000,   22.3200, NULL, '9999999999999', 'SN',   'E0',     NULL,  'X',   22.3200,  NULL      ,NULL,  0.0000, 25.0000, 0.0000,  0.0000,  '001103',    NULL,       'X',      0.0000,  0.0000, 0.0000, 2.6800, 22.3200,     0.0000,   "+"   0.0000,    0.0000,      'D',       null,   null,    '2021-08-17','2021-08-17',   '18',            '9999999999999','07',      NULL,    NULL,   0.0000,   0.0000,  0.0000, 'N',        NULL,   NULL,      NULL,       NULL,  NULL,         NULL,   '013',  0.0000, 'N',     "+"  NULL,             NULL            ,NULL                    ,NULL           ,NULL           ,NULL   ,null        ,0.0000  ,null           ,NULL     ,NULL       ,0.0000   ,NULL            ,NULL,       "+" '00:00:00.000','001'    ,0.0000     ,null        ,NULL    ,NULL         ,NULL       ,NULL       ,''        ,'N'       ,NULL              ,NULL             ,NULL       ,''        ,NULL      ,NULL   ,NULL        ,NULL         ,NULL         ,NULL  )";
                    connection.Open();
                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    resultado = cmd.ExecuteNonQuery().ToString();
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return resultado;
        }

      
    }
}
