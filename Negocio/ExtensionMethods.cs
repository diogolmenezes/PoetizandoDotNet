using System.Linq;

namespace Poetizando.Negocio
{
    public static class MetodosDeExtensao
    {

        public static string RemoverTags(this string texto)
        {
            return System.Text.RegularExpressions.Regex.Replace(texto, "<.*?>", string.Empty);
        }

        public static string NomeURL(this string nome)
        {
            nome = nome.Replace("  ", " ").Replace(" ", "-").Replace(".", "").Replace("?", "").ToLower();
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(nome);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static string NomeWiki(this string nome)
        {
            //var novoNome = "";
            //nome.Split(' ').ToList().ForEach(x => novoNome += "_" + x.First().ToString().ToUpper() + string.Join("", x.Skip(1)));
            //return novoNome.Substring(1).Replace("  ", " ").Replace(" ", "_").Replace(".", "");
            return nome.Replace("  ", " ").Replace(" ", "_").Replace(".", "").Replace("?","");
        }
    }
}
