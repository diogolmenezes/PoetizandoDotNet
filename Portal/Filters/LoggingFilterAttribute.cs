using DmgTools.Log;
using System;
using System.Configuration;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Poetizando.Portal.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var actionParameters = filterContext.ActionParameters;
            var parameters = "";

            if (actionParameters != null)
            {
                foreach (var parameter in filterContext.ActionParameters)
                    parameters += (String.IsNullOrEmpty(parameters)) ? String.Format("{0} - {1}", parameter.Key, parameter.Value) : String.Format(", {0} - {1}", parameter.Key, parameter.Value);
            }

            LogWrapper.Write(String.Format("(Logging Filter) User [{0}] Controller [{1}] Action [{2}] parameters[{3}]", "", controller, action, parameters), LogWrapper.LogType.Debug);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var action     = filterContext.ActionDescriptor.ActionName;

                var errorId = Guid.NewGuid().ToString();

                LogWrapper.Write(String.Format("(Logging Filter) User [{0}] Controller [{1}] Action [{2}], ErrorId [{3}]", "", controller, action, errorId), filterContext.Exception, LogWrapper.LogType.Error);

                SendMail(filterContext.Exception, errorId);
            }

            base.OnActionExecuted(filterContext);
        }

        private void SendMail(Exception exception, string errorId)
        {
            try
            {
                var mensagem = new StringBuilder();
                mensagem.AppendLine(String.Format("Ocorreu um erro no servidor. ErrorID [{0}] <br/><br/>", errorId));
                mensagem.AppendLine(String.Format("Exception: [message] {0} [stacktrace] {1}", exception.Message, exception.StackTrace));
                while (exception.InnerException != null)
                {
                    mensagem.AppendLine(String.Format("<br/><br/>Inner Exception: [message] {0} [stacktrace] {1}", exception.InnerException.Message, exception.InnerException.StackTrace));
                    exception = exception.InnerException;
                }


                var email = ConfigurationManager.AppSettings["Email.To"];
                WebMail.SmtpServer = ConfigurationManager.AppSettings["Email.SMTP"];
                WebMail.UserName = ConfigurationManager.AppSettings["Email.Usuario"];
                WebMail.Password = ConfigurationManager.AppSettings["Email.Senha"];

                if (WebMail.SmtpServer.Contains("gmail"))
                {
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;
                }

                WebMail.Send(
                        email,
                        "[ERRO-POETIZANDO]  " + errorId,
                        mensagem.ToString(),
                        email
                    );       
              
            }
            catch (Exception ex)
            {
                LogWrapper.Write("O correu um erro ao enviar e-mail de notificação de erro", ex, LogWrapper.LogType.Error);
            }
        }
    }
}