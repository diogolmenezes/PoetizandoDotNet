using Poetizando.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poetizando.Portal.Controllers
{
    public class ImagemController : Controller
    {
        //
        // GET: /Imagem/

        public ActionResult Index()
        {
            var imagens = new ImagemBusiness().Listar(0, 15);
            return View(imagens);
        }

        public ActionResult RecuperarImagens(int id)
        {
            var inicio = id;
            var imagens = new ImagemBusiness().Listar(inicio, 15);
            return PartialView("_ListaImagem", imagens);
        }


        public ActionResult Detalhe(string id)
        {
            new EstatisticaBusiness().Atualizar(Entidade.Funcionalidade.DetalheImagem);
            var imagem = new ImagemBusiness().Carregar(id);
            return View(imagem);
        }
    }
}
