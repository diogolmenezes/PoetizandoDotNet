using Poetizando.Entidade;
using Poetizando.Negocio;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Poetizando.Portal.Controllers
{
    public class AutorController : Controller
    {
        //
        // GET: /Autor/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buscar(string nome)
        {
            ViewBag.SubTitulo = string.Format("Buscando por \"<strong>{0}</strong>\"", nome);

            var autores = new AutorBusiness().Listar(nome);
            return View("Index", autores);
        }
        
        public ActionResult Letra(string letra)
        {
            ViewBag.Letra = letra.ToLower();
            ViewBag.SubTitulo = string.Format("Autores com a letra \"<strong>{0}</strong>\"", letra.ToUpper());
            var autores = new AutorBusiness().ListarPelaPrimeiraLetra(letra);            
            return View("Index", autores);
        }
        
        public ActionResult FrasesPorAutor(string nome)
        {
            new EstatisticaBusiness().Atualizar(Entidade.Funcionalidade.DetalheAutor);

            var autor = new AutorBusiness().CarregarPorNome(nome);
            
            if (autor == null)
                return null;            

            ViewBag.Frases = new FraseBusiness().ListarPorAutor(autor, 0, 30);

            return View("FrasesPorAutor", autor); 
        }

        public ActionResult MostrarFrases(string nome, int? registroInicial)
        {
            registroInicial = (!registroInicial.HasValue) ? 0 : registroInicial.Value;

            var autor = new AutorBusiness().CarregarPorNome(nome);

            if (autor == null)
                return null;

            var frases = new FraseBusiness().ListarPorAutor(autor, registroInicial.Value, 30);

            return PartialView("_ListaFrase", frases);
        }    
    }
}
