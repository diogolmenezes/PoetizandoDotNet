using Poetizando.Negocio;
using Poetizando.Negocio.Help;
using System;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.Linq;
using System.IO;

namespace Poetizando.Portal.Controllers
{
    public class HomeController : Controller
    {
        public int PaginaAtual
        {
            get
            {
                int pagina;

                if (Int32.TryParse(Request.QueryString["p"], out pagina))
                {
                    pagina = (pagina <= 0) ? 1 : pagina;
                }
                else
                    pagina = 1;

                return pagina;
            }
        }

        public ActionResult Index()
        {
            new EstatisticaBusiness().Atualizar(Entidade.Funcionalidade.Home);

            int totalDeRegistros = 0;
            var frases = new FraseBusiness().ListarMaisPupulares(PaginaAtual, 4, 240, out totalDeRegistros);

            ViewBag.TopAutores  = new AutorBusiness().ListarTops();
            ViewBag.PaginaAtual      = PaginaAtual;
            ViewBag.TotalDeRegistros = totalDeRegistros;
            ViewBag.PaginaInicial    = "home";
            ViewBag.Video            = new VideoBusiness().CarregarDestaque();
            ViewBag.Texto            = new TextoBusiness().CarregarDestaque();
            ViewBag.Tags             = new TagBusiness().ListarTopTags();

            return View(frases);
        }

        public ActionResult Buscar(string id)
        {
            ViewBag.Busca   = id;
            ViewBag.Frases  = new FraseBusiness().Listar(id, 5);
            ViewBag.Videos  = new VideoBusiness().Listar(id, 5);
            ViewBag.Autores = new AutorBusiness().Listar(id, 5);
            ViewBag.Textos  = new TextoBusiness().Listar(id, 5);

            return View();
        }

        public ActionResult AjustarTags()
        {            
            var sql = new FraseBusiness().AjustarTags();
            return Content(sql);           
        }


        public ActionResult AjustarAutores()
        {
            new RoboAutores().AjustarInformacoes();
            return null;
        }

        public XmlResult GerarSitemap(string id)
        {
            Sitemap sitemap = new Sitemap();

            var dominio = !String.IsNullOrEmpty(id) ? id : "http://poetizando.com.br";

            var paginasIniciais = new string[] { "/", "/contato", "/video", "/imagem", "/blog", "/sobre" };

            sitemap.Add(new Location()
            {
                Url = String.Format("{0}/frase", dominio),
                ChangeFrequency = Location.eChangeFrequency.always,
                Priority = 0.8D
            });

            foreach (var pagina in paginasIniciais)
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}{1}", dominio, pagina),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }
            
            // cadastrando os autores por letra
            foreach (var letra in "abcdefghijklmnopqrstuvwxyz".ToCharArray())
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/autor/letra/{1}", dominio, letra),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }

            // cadastrando os autores
            var autores = new AutorBusiness().Listar().OrderBy(x=> x.Nome);
            foreach (var autor in autores)
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/frases-de-{1}", dominio, autor.Nome.NomeURL()),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }

            // cadastrando as frases
            var frases = new FraseBusiness().Listar().OrderBy(x=> x.Autor.Nome);
            foreach (var frase in frases)
            {               
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/frase/{1}/{2}", dominio, frase.Autor.Nome.NomeURL(), frase.Id),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }

            // cadastrando as tags
            var tags = new TagBusiness().ListarTopTags();
            foreach (var tag in tags)
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/tag/frases-de-{1}", dominio, tag.Nome.NomeURL()),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }

            // cadastrando videos
            var videos = new VideoBusiness().Listar();
            foreach (var video in videos)
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/video/detalhe/{1}", dominio, video.Titulo.NomeURL()),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }

            // cadastrando musicas
            var frasesComMusica = new FraseBusiness().Filtrar().Where(f=> f.Musica != null).OrderBy(x=> x.Autor.Nome).ToList();
            foreach (var frase in frasesComMusica)
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/frase/{1}/{2}?o=musica", dominio, frase.Autor.Nome.NomeURL(), frase.Id),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }

            // cadastrando imagens
            var imagens = new ImagemBusiness().Listar().OrderBy(x => x.Frase.Autor.Nome).ToList();
            foreach (var imagem in imagens)
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/frase/{1}/{2}?o=imagem", dominio, imagem.Frase.Autor.Nome.NomeURL(), imagem.Frase.Id),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.daily,
                    Priority = 0.8D
                });
            }
            
            // cadastrando blog
            var textos = new TextoBusiness().Listar();
            foreach (var texto in textos)
            {
                sitemap.Add(new Location()
                {
                    Url = String.Format("{0}/blog/post/{1}", dominio, texto.Titulo.NomeURL()),
                    //LastModified = DateTime.UtcNow.AddDays(-1),
                    ChangeFrequency = Location.eChangeFrequency.weekly,
                    Priority = 0.8D
                });
            }        

            var result = new XmlResult(sitemap);

            result.ToFile();

            return result;

        }        
    }

    public class XmlResult : ActionResult
    {
        public XmlResult(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            Data = data;
        }
        public object Data { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            var serializer = new XmlSerializer(Data.GetType());
            serializer.Serialize(response.OutputStream, Data);
        }

        public void ToFile()
        {
            var serializer = new XmlSerializer(Data.GetType());
            StreamWriter sw = new StreamWriter(@"C:\Users\Diogo\Desktop\sitemap.xml");
            serializer.Serialize(sw, Data);
            sw.Close();
        }
    }
}
