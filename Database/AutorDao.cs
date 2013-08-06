using Poetizando.Database.Framework;
using Poetizando.Entidade;
using Poetizando.Entidade.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Database
{
    public class AutorDao : RepositorioBase<Autor>
    {        
        public IList<AutorRankingDto> ListarTops(int total)
        {            
            var contexto = new PoetizandoContext();
            var query = from a in contexto.Autores
                        join b in contexto.Frases on a.Id equals b.Autor.Id                        
                        where 
                        a.Ativo
                        group a by new
                        {
                            a.Id,
                            a.Imagem,
                            a.Nome,
                            a.Destaque
                        }
                            into grupo
                            let totalDeFrases = grupo.Count()
                            orderby grupo.Key.Destaque descending, totalDeFrases descending
                            select new AutorRankingDto()
                            {
                                Id = grupo.Key.Id,
                                Nome = grupo.Key.Nome,
                                TotalDeFrases = totalDeFrases,
                                Imagem = (String.IsNullOrEmpty(grupo.Key.Imagem)) ? "img-autor.png" : grupo.Key.Imagem
                            };


            return query.Take(total).ToList();
        }

        public IList<Autor> ListarPelaPrimeiraLetra(string letra)
        {
            return base.Listar().Where(x => x.Nome.ToLower().StartsWith(letra.ToLower()) && x.Ativo)
                .OrderBy(x => x.Nome)                
                .ToList();
        }
    }
}
