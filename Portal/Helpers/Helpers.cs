using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Poetizando.Portal.Helpers
{
    public static class Helpers
    {
        public static MvcHtmlString CriarPaginacao(this HtmlHelper helper, int totalDeRegistros, int paginaAtual, int itensPorPagina, string classeCss)
        {
            //var paginaInicial = 1;           

            //if (paginaAtual > maximoDePaginas * paginaAtual)
            //{
            //    paginaInicial = paginaAtual / maximoDePaginas + maximoDePaginas;
            //}

            //var paginaFinal = paginaInicial + maximoDePaginas;

            //if (paginaFinal > totalDeRegistros / itensPorPagina)
            //    paginaFinal = totalDeRegistros / itensPorPagina;
            
            var HTML = new StringBuilder();

            HTML.AppendLine(String.Format("<nav class=\"paginacao {0}\">", classeCss));
            HTML.AppendLine("<ul>");


            if (paginaAtual > 1)
                HTML.AppendLine(String.Format(" <li><a href=\"/Home/Index/?p={0}\">anterior</a></li>", paginaAtual - 1));

            if (paginaAtual < (totalDeRegistros/itensPorPagina))
                HTML.AppendLine(String.Format(" <li><a href=\"/Home/Index/?p={0}\">próximo</a></li>", paginaAtual + 1));


            //for (int i = paginaInicial; i < paginaFinal; i++)
            //{               
            //    HTML.AppendLine(String.Format(" <li><a href=\"?p={0}\">{1}</a></li>", i, i));
            //}

            //if (totalDeRegistros / itensPorPagina > maximoDePaginas)
            //    HTML.AppendLine(String.Format(" <li><a href=\"?p={0}\">...</a></li>", paginaFinal));            

            HTML.AppendLine("</ul>");
            HTML.AppendLine("</nav>");

            return new MvcHtmlString(HTML.ToString());
        }
    }
}
