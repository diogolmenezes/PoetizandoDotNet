using Poetizando.Entidade;
using Poetizando.Negocio.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio
{
    public class VideoBusiness : ServicoBase<Video>
    {
        public Video CarregarDestaque()
        {
            return base.Filtrar().Where(v => v.Ativo).OrderByDescending(v => v.DataCriacao).FirstOrDefault();
        }

        public IList<Video> Listar(bool ignorarDestaque)
        {
            return base.Filtrar().Where(v => v.Ativo).OrderByDescending(v => v.DataCriacao).Skip(1).ToList();
        }

        public IList<Video> Listar(string busca, int? maximo)
        {
            var query = base.Filtrar().Where(x => x.Ativo && (x.Titulo.Contains(busca) || x.Descricao.Contains(busca)));

            if (maximo != null)
                query = query.Take(maximo.Value);

            return query.ToList();
        }

        public Video CarregarPorNome(string nome)
        {
            return base.Filtrar().Where(x => x.Titulo.Replace("  ", " ").Replace(" ", "-").Replace(".", "").Replace("?", "").ToLower() == nome && x.Ativo).FirstOrDefault();
        }
    }
}
