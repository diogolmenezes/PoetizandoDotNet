using Poetizando.Database.Framework;
using Poetizando.Entidade;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Database
{
    public class FraseDao : RepositorioBase<Frase>
    {

        public IList<Frase> ListarMaisPopulares(int pagina, int itensPorPagina, int? tamanhoMaximoDaFrase, out int totalDeRegistros)
        {
            try
            {
                var skip = (pagina *  itensPorPagina) - itensPorPagina;
                //TODO: melhorar esse metodo, colocar logica de popularidade da frase
                if (tamanhoMaximoDaFrase.HasValue)
                {
                    totalDeRegistros = base.Listar().Where(x => x.Texto.Length <= tamanhoMaximoDaFrase && x.Ativo).Count();
                    return base.Listar().Where(x => x.Texto.Length <= tamanhoMaximoDaFrase && x.Ativo).OrderByDescending(x => x.EstaNaFanPage).ThenByDescending(x=> x.DataCriacao).Skip(skip).Take(itensPorPagina).ToList();
                }
                else
                {
                    totalDeRegistros = base.Listar().Count();
                    return base.Listar().Where(x => x.Ativo).OrderByDescending(x => x.EstaNaFanPage).ThenByDescending(x => x.DataCriacao).Skip(skip).Take(itensPorPagina).ToList();
                }
            }
            catch
            {                
                throw;
            }            
        }

        public IList<Frase> ListarAleatoriamente(int maximo)
        {
            var SQL = string.Format("SELECT * FROM Frase f WHERE f.ativo = 1 order by rand() limit {0};", maximo);
            return new PoetizandoContext().Frases.SqlQuery(SQL).ToList();
        }
    }
}
