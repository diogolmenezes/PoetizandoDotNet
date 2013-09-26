using Poetizando.Entidade;
using Poetizando.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Poetizando.Portal.Controllers
{
    public class FraseController : Controller
    {
        //
        // GET: /Frase/

        public ActionResult Index(string o)
        {
            IList<Frase> frases = new List<Frase>();

            ViewBag.Ordem = (String.IsNullOrEmpty(o)) ? "aleatoriamente" : o;

            if (String.IsNullOrEmpty(o) || o == "aleatoriamente")
                frases = new FraseBusiness().ListarAleatoriamente(10);
            else
            {
                int total = 0;
                frases = new FraseBusiness().ListarMaisPupulares(1, 10, null, out total);
            }

            return View(frases);
        }

        public ActionResult CarregarFrase(string id)
        {
            new EstatisticaBusiness().Atualizar(Entidade.Funcionalidade.DetalheFrase);
            var frase = new FraseBusiness().Carregar(id);
            ViewBag.Imagem = new ImagemBusiness().Recuperar(frase);
            ViewBag.Opcao = (String.IsNullOrEmpty(Request.QueryString["o"])) ? "imagem" : Request.QueryString["o"];
            return View("Detalhe", frase);
        }

        public ActionResult Aleatoria()
        {
            var frase = new FraseBusiness().RetornarFraseAleatoria();

            return PartialView("_Frase", frase);
        }

        public ActionResult MaisPopulares()
        {
            int total = 0;
            var frases = new FraseBusiness().ListarMaisPupulares(1, 10, null, out total);
            return PartialView("_Populares", frases);
        }        

        public ActionResult Listar(string ordem, int pagina)
        {
            IList<Frase> frases = new List<Frase>();

            if (String.IsNullOrEmpty(ordem) || ordem == "aleatoriamente")
                frases = new FraseBusiness().ListarAleatoriamente(30);
            else
            {
                int total = 0;
                frases = new FraseBusiness().ListarMaisPupulares(pagina, 30, null, out total);
            }

            return PartialView("_ListaFrase", frases);
        }

        public ActionResult Corrigir(string id)
        {
            new EstatisticaBusiness().Atualizar(Entidade.Funcionalidade.CorrecaoFrase);

            if (String.IsNullOrEmpty(id))
                return View("Error");

            var frase = new FraseBusiness().Carregar(id);                      

            ViewBag.Autores = new AutorBusiness().Listar().OrderBy(x => x.Nome).ToList();
            ViewBag.Categorias = new TagBusiness().Listar().OrderBy(x => x.Nome).ToList();

            return View(frase);
        }

        public ActionResult FrasesPorTag(string tag)
        {
            var objTag = new TagBusiness().CarregarPorNome(tag);
            IList<Frase> frases = new List<Frase>();
            ViewBag.Frases = new FraseBusiness().ListarPorTag(tag, 0, 30);
            return View(objTag);
        }

        public ActionResult MostrarPorTag(string nome, int? registroInicial)
        {
            registroInicial = (!registroInicial.HasValue) ? 0 : registroInicial.Value;

            var tag = new TagBusiness().CarregarPorNome(nome);

            if (tag == null)
                return null;

            var frases = new FraseBusiness().ListarPorTag(tag.Nome, registroInicial.Value, 30);

            return PartialView("_ListaFrase", frases);
        }    

        public ActionResult Criar()
        {
            new EstatisticaBusiness().Atualizar(Entidade.Funcionalidade.CriacaoFrase);

            ViewBag.Autores = new AutorBusiness().Listar().OrderBy(x => x.Nome).ToList();
            ViewBag.Categorias = new TagBusiness().Listar().OrderBy(x => x.Nome).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Criar(FormCollection form)
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

                string autor     = (String.IsNullOrEmpty(form["ddlAutor"])) ? form["txtOutroAutor"] : form["ddlAutor"];
                string categoria = (String.IsNullOrEmpty(form["ddlCategoria"])) ? form["txtOutraCategoria"] : form["ddlCategoria"]; ;
                var msg          = new StringBuilder();

                msg.AppendLine(String.Format("<br/>Frase: {0}", form["Texto"].Replace("\r\n", "<br/>")));
                msg.AppendLine(String.Format("<br/>Autor: {0}", autor));
                msg.AppendLine(String.Format("<br/>Categoria: {0}", categoria));


                var SQL = new StringBuilder();
                var guidFrase = Guid.NewGuid().ToString().Replace("-", "");
                if (String.IsNullOrEmpty(form["txtOutroAutor"]))
                    SQL.AppendLine(String.Format("INSERT INTO Frase VALUES ('{0}','{1}',1,now(),'{2}', 0, 0, null, null);", guidFrase, form["Texto"], form["ddlAutor"]));
                else
                {
                    var guidAutor = Guid.NewGuid().ToString().Replace("-", "");
                    SQL.AppendLine(String.Format("INSERT INTO Autor (Id, Nome, Destaque, Ativo) VALUES ('{0}','{1}', 0, 1);", guidAutor, form["txtOutroAutor"]));
                    SQL.AppendLine(String.Format("INSERT INTO Frase VALUES ('{0}','{1}',1,now(),'{2}', 0, 0, null, null);", guidFrase, form["Texto"], guidAutor));
                }

                if (!String.IsNullOrEmpty(form["ddlCategoria"]))
                    SQL.AppendLine(String.Format("INSERT INTO TagFrase (Id, Frase_Id, Tag_Id) VALUES ('{0}','{1}', '{2}');", Guid.NewGuid().ToString().Replace("-", ""), guidFrase, form["ddlCategoria"]));


                SQL.AppendLine("<br/>");
                SQL.AppendLine("<br/>");
                SQL.AppendLine(String.Format("INSERT INTO Imagem (Id, Nome, DataCriacao, Ativo, Frase_Id) VALUES ('{0}', '{1}.png', '{2}', '1', '{3}');", guidFrase, guidFrase, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), guidFrase));
                SQL.AppendLine("UPDATE Frase INNER JOIN Imagem ON Frase.Id = Imagem.Frase_Id SET Frase.DataCriacao = Imagem.DataCriacao, EstaNaFanPage = 1;");
                SQL.AppendLine("<br/>");
                SQL.AppendLine("<br/>");

                msg.AppendLine(String.Format("<br/>SQL: {0}", SQL.ToString()));

                

                WebMail.Send(
                        email,
                        "Poetizando - Sugestão de frase",
                        msg.ToString(),
                        WebMail.UserName
                    );
            }

            return View("FraseEnviada");
        }

        [HttpPost]
        public ActionResult Corrigir(FormCollection form)
        {
            if (ModelState.IsValid)
            {

                var email = ConfigurationManager.AppSettings["Email.To"];
                WebMail.SmtpServer = ConfigurationManager.AppSettings["Email.SMTP"];
                WebMail.UserName = ConfigurationManager.AppSettings["Email.Usuario"];
                WebMail.Password = ConfigurationManager.AppSettings["Email.Senha"];

                if (WebMail.SmtpServer.Contains("gmail"))
                {
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;
                }

                string autor     = (String.IsNullOrEmpty(form["ddlAutor"])) ? form["txtOutroAutor"] : form["ddlAutor"];
                string categoria = (String.IsNullOrEmpty(form["ddlCategoria"])) ? form["txtOutraCategoria"] : form["ddlCategoria"];

                var msg = new StringBuilder();

                msg.AppendLine("=================== FRASE ANTIGA ===================");
                msg.AppendLine(String.Format("<br/>Id Frase: {0}", form["Id"]));
                msg.AppendLine(String.Format("<br/>Frase: {0}", form["TextoAntigo"]));

                msg.AppendLine("<br/><br/><br/>"); 
                
                msg.AppendLine("=================== FRASE NOVA ===================");
                msg.AppendLine(String.Format("<br/>Remover do site: {0}", form["chkImpropria"].Contains("true")));
                msg.AppendLine(String.Format("<br/>Frase: {0}", form["Texto"]));
                msg.AppendLine(String.Format("<br/>Autor: {0}", autor));
                msg.AppendLine(String.Format("<br/>Categoria: {0}", categoria));

                var sqlAutor = (!String.IsNullOrEmpty(form["ddlAutor"])) ? String.Format(", Autor_Id = '{0}' ", form["ddlAutor"]) : "";

                msg.AppendLine(String.Format("<br/>SQL: UPDATE Frase SET Texto = '{0}' {1} WHERE Id = '{2}';", form["Texto"], sqlAutor, form["Id"]));

                msg.AppendLine("<br/><br/><br/>"); 

                msg.AppendLine("=================== CONTATO  ===================");
                msg.AppendLine(String.Format("<br/>E-mail de contato: {0}", form["txtEmail"]));

                WebMail.Send(
                        email,
                        "Poetizando - Correção de frase",
                        msg.ToString(),
                        WebMail.UserName
                    );
            }

            ViewBag.Autor = new FraseBusiness().Carregar(form["Id"]).Autor;

            return View("CorrecaoEnviada");
        }
    }
}
