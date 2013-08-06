using Poetizando.Entidade;
using Poetizando.Negocio.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio
{
    public class TextoBusiness : ServicoBase<Texto>
    {
        public Texto CarregarDestaque()
        {
            return base.Filtrar().Where(v => v.Ativo).OrderByDescending(v => v.DataCriacao).FirstOrDefault();
        }

        public IList<Texto> Listar(string busca, int? maximo)
        {
            var query = base.Filtrar().Where(x => x.Ativo && (x.Titulo.Contains(busca) || x.Descricao.Contains(busca) || x.Autor.Nome.Contains(busca)));

            if (maximo != null)
                query = query.Take(maximo.Value);

            return query.ToList();
        }

        public IList<Texto> ListarParaBlog()
        {
            return ListarParaBlog(null);
        }

        public IList<Texto> ListarParaBlog(int? maximo)
        {
            var query = base.Filtrar().Where(x => x.Ativo);

            if (maximo != null)
                query = query.Take(maximo.Value);

            return query.OrderByDescending(x=> x.DataCriacao).ToList();
        }

        public Texto CarregarPorTitulo(string tituloTexto)
        {
            return base.Filtrar().Where(x => x.Titulo.Replace("  ", " ").Replace(" ", "-").Replace(".", "").Replace("?", "").ToLower() == tituloTexto && x.Ativo).FirstOrDefault();
        }
    }
}
