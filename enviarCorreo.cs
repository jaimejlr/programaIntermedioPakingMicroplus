using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramaIntermedioPackinMicroplus
{
  public class enviarCorreo
    {
        public static string EnvioEmail(string para, string copia, string copiaOculta, string EnviarComo, string asunto, string mensaje, string proceso, string ID_EN_PROCESO)
        {
            string resultado = "";
          //  BitacoraEnvioCorreos bitacora = new BitacoraEnvioCorreos();
            try
            {
                char[] MyChar = { ';' };
                string ParaCorrecto = para.TrimStart(MyChar);
                string ParaCorrecto1 = ParaCorrecto.TrimEnd(MyChar);
                if (ParaCorrecto1 == "")
                {
                    ParaCorrecto1 = ParaCorrecto1 + EnviarCorreoBL.email;
                }

                string CopiaCorrecto = copia.TrimStart(MyChar);
                string CopiaCorrecto1 = CopiaCorrecto.TrimEnd(MyChar);

                string CopiaOcultaCorrecto = copiaOculta.TrimStart(MyChar);
                string CopiaOcultaCorrecto1 = CopiaOcultaCorrecto.TrimEnd(MyChar);


                //var fromAddress = new MailAddress(EnviarCorreoBL.email, EnviarCorreoBL.nombreEmail);
                //string fromPassword = EnviarCorreoBL.passwordEmail;
                var smtp = new SmtpClient
                {
                    Host = EnviarCorreoBL.hosts,
                    Port = EnviarCorreoBL.puerto,
                    EnableSsl = EnviarCorreoBL.ssl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(EnviarCorreoBL.email, EnviarCorreoBL.passwordEmail)

                };
                char[] delimit = new char[] { ';' };
                var msg = new MailMessage();
                foreach (string enviar_a in ParaCorrecto1.Split(delimit))
                {
                    if (ValidarFormatoCorreo(enviar_a))
                        msg.To.Add(new MailAddress(enviar_a));
                    //else
                     //   bitacora.guardarEmails(proceso, asunto, enviar_a, "", "", mensaje, "NO_ENVIADO", "CORREO INCORRECTO", ID_EN_PROCESO);
                }
                if (CopiaCorrecto1 != "")
                {
                    foreach (string copiar_a in CopiaCorrecto1.Split(delimit))
                    {
                        if (ValidarFormatoCorreo(copiar_a))
                            msg.CC.Add(new MailAddress(copiar_a));
                        //else
                          //  bitacora.guardarEmails(proceso, asunto, "", copiar_a, "", mensaje, "NO_ENVIADO", "CORREO INCORRECTO", ID_EN_PROCESO);
                    }
                }

                if (CopiaOcultaCorrecto1 != "")
                {
                    foreach (string copiarOculta_a in CopiaOcultaCorrecto1.Split(delimit))
                    {
                        if (ValidarFormatoCorreo(copiarOculta_a))
                            msg.Bcc.Add(new MailAddress(copiarOculta_a));
                       // else
                        //    bitacora.guardarEmails(proceso, asunto, "", "", copiarOculta_a, mensaje, "NO_ENVIADO", "CORREO INCORRECTO", ID_EN_PROCESO);
                    }
                }

                var fromAddress = new MailAddress(EnviarCorreoBL.email, EnviarComo); /// envia con el nombre del correo jlopez@auotmekano.com <pagos@automekano.com>
                msg.ReplyToList.Add(new MailAddress(EnviarComo));
                msg.From = fromAddress;
                msg.Subject = asunto;
                msg.Body = mensaje;
                msg.IsBodyHtml = true;
                smtp.Send(msg);
                resultado = "Correo enviado";
                //bitacora.guardarEmails(proceso, asunto, para, copia, copiaOculta, mensaje, "ENVIADO", "", ID_EN_PROCESO);

            }
            catch (SmtpException ex)
            {
                resultado = ex.Message;
                throw new Exception(ex.Message);
                //bitacora.guardarEmails(proceso, asunto, para, copia, copiaOculta, mensaje, "NO_ENVIADO", ex.Message, ID_EN_PROCESO);
            }
            catch (ArgumentNullException ex)
            {
                resultado = ex.Message;
                throw new Exception(ex.Message);
                //bitacora.guardarEmails(proceso, asunto, para, copia, copiaOculta, mensaje, "NO_ENVIADO", ex.Message, ID_EN_PROCESO);

            }
            catch (InvalidOperationException ex)
            {
                resultado = ex.Message;
                throw new Exception(ex.Message);
                //bitacora.guardarEmails(proceso, asunto, para, copia, copiaOculta, mensaje, "NO_ENVIADO", ex.Message, ID_EN_PROCESO);
            }

            catch (Exception ex)
            {
                resultado = ex.Message;
                throw new Exception(ex.Message);
                //bitacora.guardarEmails(proceso, asunto, para, copia, copiaOculta, mensaje, "NO_ENVIADO", ex.Message, ID_EN_PROCESO);

            }
            return resultado;

        }

        public static Boolean ValidarFormatoCorreo(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        //       CREATE TABLE AK_PARAMETROS_GENERALES
        //  (PG_ID_PARAMETRO DECIMAL(15,6) NOT NULL,
        //   PG_ID_ID_PARAMETRO DECIMAL(15,6), 
        //PG_CODIGO VARCHAR(100), 
        //PG_DESCRIPCION VARCHAR(100), 
        //PG_VALOR VARCHAR(200), 
        //PG_ESTADO CHAR(1) DEFAULT 'A', 
        //PG_ORDEN DECIMAL(15,6)
        //  )


        //        insert into ak_parametros_generales
        //  (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (1, null, 'CADENA_CONEXION_MYSQL', 'CADENA CONEXION', 'server=82.163.176.103;user id=smartbu2_jl;password=gu)1!0i4Sp;database=smartbu2_jl;SSL Mode=None', 'A', 1);

        //        insert into ak_parametros_generales
        //  (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (2, null, 'ENVIO_MAIL', 'PARAMETROS PARA ENVIO DE CORREO Y NOTIFICACIONES', '', 'A', NULL);

        //        insert into ak_parametros_generales
        //        (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (3, 2, 'EMAIL_ORIGEN', 'EMAIL DE ORIEGEN DE ENVIO', 'lrsoftsolution.com@gmail.com', 'A', NULL);

        //        insert into ak_parametros_generales
        //      (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (4, 2, 'NOMBRE_EMAIL', 'NOMBRE QUE APARECE AL DESTINATARIO', 'lrsoftsolution.com@gmail.com', 'A', NULL);

        //        insert into ak_parametros_generales
        //       (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (5, 2, 'PASSWORD', 'CONTRASENIA CORREO', '5hA2cnwMmasazgF', 'A', NULL);

        //        insert into ak_parametros_generales
        //     (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (6, 2, 'HOST', 'SERVIDOR DE CORREO', 'smtp.gmail.com', 'A', NULL);

        //        insert into ak_parametros_generales
        //   (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (7, 2, 'PUERTO', 'PUERTO DEL CORREO', '587', 'A', NULL);

        //        insert into ak_parametros_generales
        //    (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (8, 2, 'SSL', 'ENABLE SSL', '1', 'A', NULL);

        //        insert into ak_parametros_generales
        //      (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (9, 2, 'ENVIO_CORREOS', 'SI ESTA EN 1 SE ENVÍAN CORREOS DE NOTIFICACION CASO CONTRARIO NO', '1', 'A', NULL);

        //        insert into ak_parametros_generales
        //         (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (10, 2, 'EMAIL_RESPUESTA', 'EMAIL DE ORIEGEN DE RESPUESTA', 'lrsoftsolution.com@gmail.com', 'A', NULL);


        //        insert into ak_parametros_generales
        //  (pg_id_parametro, pg_id_id_parametro, pg_codigo, pg_descripcion, pg_valor, pg_estado, pg_orden)
        //values
        //  (11, 2, 'CORREOS_PARA', 'CORREO DESTINATARIO, SEPARAR CON ; PARA MAS MAILS', 'jaime_lopez16@outlook.com', 'A', NULL);

    }
}
