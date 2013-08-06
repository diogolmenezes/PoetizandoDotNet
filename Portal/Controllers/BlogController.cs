using Poetizando.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poetizando.Portal.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            var textos = new TextoBusiness().ListarParaBlog();
            return View(textos);
        }

        public ActionResult Detalhe(string tituloTexto)
        {
            var texto = new TextoBusiness().CarregarPorTitulo(tituloTexto);
            ViewBag.FbTitle = String.Format("Poetizando - {0}", texto.Titulo);
            ViewBag.FbDescription = texto.Descricao.RemoverTags();
            ViewBag.FbImage = String.Format("http://poetizando.com.br/content/img/blog/{0}", texto.Imagem);
            return View(texto);
        }

    }
}
