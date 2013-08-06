using Poetizando.Database;
using Poetizando.Database.Framework;
using Poetizando.Entidade;
using Poetizando.Negocio.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio
{
    public class FraseBusiness : ServicoBase<Frase>
    {
        public void PublicarNaFP(string id)
        {            
            var frase = base.Carregar(id);
            frase.EstaNaFanPage = true;
            base.Alterar(frase);            
        }

        public IList<Frase> Listar(string busca)
        {
            return Listar(busca, null);
        }

        public IList<Frase> ListarAdministrativo(string busca, string autorId)
        {
            if (!string.IsNullOrEmpty(busca) && !string.IsNullOrEmpty(autorId))
                return base.Filtrar().Where(x => x.Texto.Contains(busca) && x.Autor.Id == autorId).OrderBy(x=> x.Texto).ToList();
            else if (!string.IsNullOrEmpty(busca))
                return base.Filtrar().Where(x => x.Texto.Contains(busca)).OrderBy(x => x.Texto).ToList();
            else if (!string.IsNullOrEmpty(autorId))
                return base.Filtrar().Where(x => x.Autor.Id == autorId).OrderBy(x => x.Texto).ToList();
            else
                return new List<Frase>();
        }

        public IList<Frase> Listar(string busca, int? maximo)
        {
            var query = base.Filtrar().Where(x => x.Texto.Contains(busca) || x.Autor.Nome.Contains(busca));

            if(maximo != null)
                query = query.Take(maximo.Value);

            return query.ToList();
        }

        public Frase RetornarFraseAleatoria()
        {
            //var frase = new FraseDao().RetornarFraseAleatoria();

            //if (frase.Texto.Length > 140)
            //    frase.Texto = string.Format("{0} (...)", frase.Texto.Substring(0, 140));

            //return frase;
            //TODO: fazer
            return base.Filtrar().ToList().FirstOrDefault();
        }

        public IList<Frase> ListarMaisPupulares(int pagina, int itensPorPagina, int? tamanhoMaximoDaFrase, out int totalDeRegistros)
        {
            return new FraseDao().ListarMaisPopulares(pagina, itensPorPagina, tamanhoMaximoDaFrase, out totalDeRegistros);
        }

        public IList<Frase> ListarAleatoriamente(int maximo)
        {
            return new FraseDao().ListarAleatoriamente(maximo);
        }

        public IList<Frase> ListarPorAutor(Autor autor, int registroInicial, int total)
        {
            return base.Filtrar().Where(x => x.Autor.Id == autor.Id).OrderByDescending(x => x.DataCriacao).Skip(registroInicial).Take(total).ToList();            
        }       
    }
}
