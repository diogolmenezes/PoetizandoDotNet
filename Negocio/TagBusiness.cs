using Poetizando.Entidade;
using Poetizando.Negocio.Framework;
using System.Linq;

namespace Poetizando.Negocio
{
    public class TagBusiness : ServicoBase<Tag>
    {
        public Tag CarregarPorNome(string nome)
        {
            return base.Filtrar().Where(x => x.Nome.Replace("  ", " ").Replace(" ", "-").Replace(".", "").Replace("?", "").ToLower() == nome.ToLower()).FirstOrDefault();
        }
    }
}
