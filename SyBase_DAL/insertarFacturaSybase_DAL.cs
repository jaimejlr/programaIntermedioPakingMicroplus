using ProgramaIntermedioPackinMicroplus.SyBase_Negocio;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus.SyBase_DAL
{
  public  class insertarFacturaSybase_DAL
    {

        public static string insertarEncabezadoFacturaSyBase(EncabezadoFactura_SB_BL obj)
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    String SQL_query = "INSERT INTO encabezadofacturas " +
                         "(codemp               ,       numfac    ,    codven      ,codalm           ,codcli            , fecfac      ,lispre          , observ           , poriva       ,  totnet       ,  totdes       , totbas        , totfac         ,    fecven                   ,conpag            , tipefe,             valefe, tipche,numche,  valche, tiptar,    " + " numtar, valtar,   tipdep, numdep,  valdep, abofac, numpag, plapag, pordes,  tiptra,   numtra,  totiva,  codapu      , valcot ,codmon, codusu        ,       fecult        , estado,   nomcli            , referen          ,  codtrans         , desinv, otrcar,     " + "descuen,totaldesc,subtot, fchche, rucced              , dircli             , codsec,  fchdep  ,tipvar, basea,  doctrans,tiptrans, totret, totfac2, totiva2,  recibe,   serie        ,     codcajero,   tipchep, valchep,  excen           ,  moivab, moivas, ser_a,       ser_b,    " + "  inv_a,             inv_b,       estadow,  codcom,  numdoc, fechaemision,  fecharegistro, tipocomprobante, idcliente            , tpidcli, referen2, codveh               , totice,  totint,   porint,  descargado, numgui              , fecinigui,  fecfingui, coment, descargado1, impreso, establ, totdev, facelec, " + " codDocModificado, numDocModificado ,fechaEmisionDocSustento,estabmodificado,ptoemimodificado,desguia,autorizacion,totiva3 ,claveaccesofe  ,fecautfe ,msgerrorfe ,valcom   ,porcentajeiva   ,totirbpn,  " + "   hora                                ,cajapc  ,descuadre  ,estadoconta  ,numpro  ,descargapro ,doctranspro ,numtradev ,codsecgui ,facelecgui ,claveaccesofegui           ,autorizaciongui,fecautfegui  ,serieguia ,referen3 ,motivo  ,ptopartida  ,ptollegada   ,idcliguia              ,nomcliguia                 )   " +
                         " VALUES " +
                         " ('" + obj.codemp + "',  '"+obj.numfac+"','"+obj.codven+"', '"+obj.codalm+"', '"+obj.codcli+ "', getdate()  ,'"+obj.lispre+"', '"+obj.observ+"' ,"+obj.poriva+",       ?       ,     ?         , "+obj.totbas+",      ?         , dateadd(mm, 1,getdate() )  , '" + obj.conpag+"', '"+obj.tipefe+"'    ,0.0000,'X',   NULL,    0.0000, 'X',         " + " NULL,  0.0000,    'X',      NULL,  0.0000,  'X'    ,1     , 0    ,  0.0000,   NULL    ,NULL,    0, '"+ obj.codapu+ "',1.0000, '01',   'IMPORTACION',        getdate()  ,      'P',  '" + obj.nomcli+"' , '"+obj.referen+"', '"+obj.codtrans+"' ,     'S',   0.0000, " + " NULL,  0.0000,    ?    , NULL, '"+ obj.rucced + "', '" + obj.dircli + "',   'F0',     NULL,   'X',      0,  NULL      ,NULL   ,  0.0000,  ?     , 0.0000 ,  0.0000, '"+obj.serie+"',    NULL      ,    'X',     0.0000,    ?              , 0.0000 ,      0,      0,     0.0000,     " + "   0.0000,             ?,      'D',       null,   null,    getdate() ,      getdate(),   '18'          ,  '" + obj.idcliente + "','06',     NULL,    '"+obj.codveh + "',0.0000,   0.0000,  0.0000, 'N'         ,'"+ obj.numgui + "'  ,   getdate(), getdate(),  NULL,  NULL      ,    NULL,   NULL,  0.0000, 'N'   , " + "  NULL,             NULL            ,NULL                    ,NULL           ,NULL           ,'D'     ,null        ,0.0000  ,null           ,NULL     ,NULL       ,0.0000   ,NULL            ,NULL,       " + " CONVERT( CHAR( 20 ), getdate(), 8 ) ,'001'    ,0.0000     ,null        ,NULL    ,NULL         ,NULL       ,NULL       ,'00'        ,'N'  ,'" + obj.claveaccesofegui+ "'  ,NULL             ,NULL    ,'001001'  ,NULL      ,NULL   ,NULL        ,NULL         ,'"+ obj.idcliguia + "'  , '"+ obj.nomcliguia + "'  )";
                    connection.Open();
                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    cmd.Parameters.Add("totnet", OdbcType.Decimal).Value = obj.totnet;
                    cmd.Parameters.Add("totdes", OdbcType.Decimal).Value = obj.totdes;
                    cmd.Parameters.Add("totfac", OdbcType.Decimal).Value = obj.totfac;
                    cmd.Parameters.Add("subtot", OdbcType.Decimal).Value = obj.totnet; //debe ser el mismo valor que total neto
                    cmd.Parameters.Add("totfac2", OdbcType.Decimal).Value = obj.totfac; // debe ser igual al total de la factura
                    cmd.Parameters.Add("excen", OdbcType.Decimal).Value = obj.excen;
                    cmd.Parameters.Add("inv_b", OdbcType.Decimal).Value = obj.inv_b;

                    resultado = cmd.ExecuteNonQuery().ToString();
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR ENCAB", ex.Message);

                   // throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR ENCAB", ex.Message);

                    //throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            return resultado;
        }


        public static string insertarDetalleFacturaSyBase(DetalleFacturaSyBaseBL obj)
        {
            string resultado = "";
            // EJECUTAR C:\Microplus\sourceplus\savemicro_plus.exe
            using (OdbcConnection connection = new OdbcConnection(SettingsConexion.Default.conexionSybase))
                try
                {
                    //String SQL_query = "INSERT INTO renglonesfacturas " +
                    //     "(codemp            , numfac         ,numren          ,numite     ,codart          ,nomart          , coduni         , cantid       ,  preuni      ,  totren       , codiva        , codmon         ,    totext    ,codmed           , cajas)   " +
                    //     " VALUES " +  
                    //     " ('"+obj.codemp+"','"+obj.numfac+"',"+obj.numren+","+obj.numite+",'"+obj.codart+"','"+obj.nomart+"','"+obj.coduni+"',"+obj.cantid+","+obj.preuni+","+obj.totren+",'"+obj.codiva+"','"+obj.codmon+"',"+obj.totext+",'"+obj.codmed+"',"+obj.cajas+")";

                    String SQL_query = "INSERT INTO renglonesfacturas " +
                        "(codemp  , numfac ,numren , numite ,codart, nomart , coduni, cantid, preuni, totren , codiva , codmon , totext  , codmed , cajas, desren , valcot, fecfac , cancaja  , basea,  excen  , bienser,  hora                              , desinv  , iceren , coduni1, valuni ,  compon , codcen ,  desren2 , valcargo , bloquea , codalm,  codcli, codusu     ,  fecult    ,  establ , codcla   ,codcolor , codven, multica, seriesdoc, irbpnren , promo )   " +
                        " VALUES " +
                        " (?      ,      ?,      ?,       ?,      ?,      ? ,     ?,   ?     ,     ?,     ?   , ?     , ?      ,  ?       ,  ?    , ?     , ?     ,  ?     , ?      ,   1    ,    0   ,   ?     , 'B'   , CONVERT( CHAR( 20 ), getdate(), 8 ) ,  'S'  ,     0    , 'UND' ,   1    ,    'N'  ,   ?    ,     0    ,    ?     ,  0      ,   '01',   ?    , 'MIGRACION',   getdate(),  '001'   , 'PT001' ,  'G0001',  ?     , 'S'   ,   '12'   ,  0       ,   'N' )";

                    connection.Open();
                    OdbcCommand cmd = new OdbcCommand(SQL_query, connection);
                    cmd.Parameters.Add("codemp", OdbcType.NVarChar).Value = obj.codemp;
                    cmd.Parameters.Add("numfac", OdbcType.NVarChar).Value = obj.numfac;
                    cmd.Parameters.Add("numren", OdbcType.NVarChar).Value = obj.numren;
                    cmd.Parameters.Add("numite", OdbcType.Int).Value = obj.numite;
                    cmd.Parameters.Add("codart", OdbcType.NVarChar).Value = obj.codart;
                    cmd.Parameters.Add("nomart", OdbcType.NVarChar).Value = obj.nomart;
                    cmd.Parameters.Add("coduni", OdbcType.NVarChar).Value = obj.coduni;
                    cmd.Parameters.Add("cantid", OdbcType.Decimal).Value = obj.cantid;
                    cmd.Parameters.Add("preuni", OdbcType.Decimal).Value = obj.preuni;
                    cmd.Parameters.Add("totren", OdbcType.Decimal).Value = obj.totren;
                    cmd.Parameters.Add("codiva", OdbcType.NVarChar).Value = obj.codiva;
                    cmd.Parameters.Add("codmon", OdbcType.NVarChar).Value = obj.codmon;
                    cmd.Parameters.Add("totext", OdbcType.Decimal).Value = obj.totext;
                    cmd.Parameters.Add("codmed", OdbcType.NVarChar).Value = obj.codmed;
                    cmd.Parameters.Add("cajas", OdbcType.Decimal).Value = obj.cajas;
                    cmd.Parameters.Add("cajdesrenas", OdbcType.Decimal).Value = obj.desren;
                    cmd.Parameters.Add("valcot", OdbcType.Decimal).Value = obj.valcot;
                    cmd.Parameters.Add("fecfac", OdbcType.DateTime).Value = obj.fecfac;
                    cmd.Parameters.Add("excen", OdbcType.Decimal).Value = obj.excen;
                    cmd.Parameters.Add("codcen", OdbcType.NVarChar).Value = obj.codcen;
                    cmd.Parameters.Add("valcargo", OdbcType.Decimal).Value = obj.valcargo;
                    cmd.Parameters.Add("codcli", OdbcType.NVarChar).Value = obj.codcli;
                    cmd.Parameters.Add("codven", OdbcType.NVarChar).Value = obj.codven;
                    resultado = cmd.ExecuteNonQuery().ToString();
                    connection.Close();
                    connection.Dispose();

                }
                catch (OdbcException ex)
                {
                    connection.Close();
                    connection.Dispose();

                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR REGLON FAC", ex.Message);

                    //throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    connection.Dispose();
                    SeleccionarDatosSybaseDAL.actualizarLogMigracionFactura(numerosFacturas.lm_factura_mysql, numerosFacturas.lm_factura_sybase, "ERROR INSR REGLON FAC", ex.Message);

                    //throw new Exception(ex.Message);
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
