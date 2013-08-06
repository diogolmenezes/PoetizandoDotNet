using Poetizando.Database;
using Poetizando.Entidade;
using Poetizando.Entidade.Dto;
using Poetizando.Negocio.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio
{
    public class AutorBusiness : ServicoBase<Autor>
    {
        public IList<Autor> Listar(string nome)
        {
            return Listar(nome, null);
        }

        public IList<Autor> Listar(string nome, int? maximo)
        {
            var query = base.Filtrar().Where(x => x.Nome.Contains(nome));

            if (maximo != null)
                query = query.Take(maximo.Value);

            return query.ToList();
        }

        public IList<AutorRankingDto> ListarTops()
        {            
            return new AutorDao().ListarTops(5);
        }

        public IList<Autor> ListarPelaPrimeiraLetra(string letra)
        {
            return new AutorDao().ListarPelaPrimeiraLetra(letra);
            
        }

        public Autor CarregarPorNome(string nome)
        {
            return base.Filtrar().Where(x => x.Nome.Replace("  ", " ").Replace(" ", "-").Replace(".", "").Replace("?","").ToLower() == nome && x.Ativo).FirstOrDefault();
        }
    }
}
