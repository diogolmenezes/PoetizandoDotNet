using Poetizando.Portal.Models;
using System.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Poetizando.Portal.Controllers
{
    public class ContatoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Anuncie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnviarContato(Contato contato)
        {
            if (ModelState.IsValid)
            {                
                var email          = ConfigurationManager.AppSettings["Email.To"];
                WebMail.SmtpServer = ConfigurationManager.AppSettings["Email.SMTP"];
                WebMail.UserName   = ConfigurationManager.AppSettings["Email.Usuario"];
                WebMail.Password   = ConfigurationManager.AppSettings["Email.Senha"];

                if (WebMail.SmtpServer.Contains("gmail"))
                {
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;
                }

                contato.Mensagem += " reply to " + contato.Email;

                WebMail.Send(
                        email,
                        "Poetizando - Contato pelo site - " + contato.Assunto,
                        contato.Mensagem,
                        WebMail.UserName
                    );                   
            }
            
            return View("Sucesso");
        }
    }
}
