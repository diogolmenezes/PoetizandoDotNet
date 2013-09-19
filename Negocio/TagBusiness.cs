using Poetizando.Entidade;
using Poetizando.Negocio.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio
{
    public class TagBusiness : ServicoBase<Tag>
    {
        public Tag CarregarPorNome(string nome)
        {
            return base.Filtrar().Where(x => x.Nome.Replace("  ", " ").Replace(" ", "-").Replace(".", "").Replace("?", "").ToLower() == nome.ToLower()).FirstOrDefault();
        }

        public IList<Tag> ListarTopTags()
        {
            return base.Filtrar().Where(x => x.Frases.Count > 0).ToList();
        }
    }
}
