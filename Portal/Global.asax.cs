using Poetizando.Portal.Filters;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace Poetizando.Portal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoggingFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("AutorLetra",
                           "autor/letra/{letra}",
                           new { controller = "Autor", action = "letra", letra = "" });

            routes.MapRoute("AutorBusca",
                            "autor/buscar/{nome}",
                            new { controller = "Autor", action = "buscar", nome = "" });

            routes.MapRoute("Autor",
                           "autor/frases/{nome}",
                           new { controller = "Autor", action = "FrasesPorAutor", nome = "" });

            routes.MapRoute("AutorPadrao",
                           "autor/{action}/{nome}/{registroInicial}",
                           new { controller = "Autor", nome = "", registroInicial = 0 });

            routes.MapRoute("FraseIndex",
                        "frase/{action}",
                        new { controller = "Frase" });

            routes.MapRoute("FraseCorrecao",
                           "frase/corrigir/{id}",
                           new { controller = "Frase", action = "corrigir", id = "" });
            
            routes.MapRoute("Frase",
                           "frase/{nome}/{id}",
                           new { controller = "Frase", action = "CarregarFrase", id = "" });

            routes.MapRoute("FraseListar",
                           "frase/Listar/{ordem}/{pagina}",
                           new { controller = "Frase", action = "Listar", ordem = "aleatoreamente", pagina = 1 });

            routes.MapRoute("Blog",
                           "blog/post/{tituloTexto}",
                           new { controller = "Blog", action = "Detalhe" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }        
    }
}