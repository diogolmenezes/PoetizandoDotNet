using Poetizando.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poetizando.Portal.Controllers
{
    public class VideoController : Controller
    {
        //
        // GET: /Video/

        public ActionResult Index()
        {
            ViewBag.Video = new VideoBusiness().CarregarDestaque();

            var videos = new VideoBusiness().Listar(true);
            return View(videos);
        }

        public ActionResult Detalhe(string id)
        {
            var video = new VideoBusiness().CarregarPorNome(id);
            return View(video);
        }

    }
}
