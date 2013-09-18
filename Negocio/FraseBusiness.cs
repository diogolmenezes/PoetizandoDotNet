using Poetizando.Database;
using Poetizando.Database.Framework;
using Poetizando.Entidade;
using Poetizando.Negocio.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IList<Frase> ListarPorTag(string tag, int registroInicial, int total)
        {
            return base.Filtrar().Where(x => x.Tags.Select(y=> y.Nome).Contains(tag)).OrderByDescending(x => x.DataCriacao).Skip(registroInicial).Take(total).ToList();
        }

        public void AjustarTags()
        {
            var script = new StringBuilder();


            var motivacao = base.Filtrar().Where(x =>
               (
                   x.Texto.Contains("fé") ||
                   x.Texto.Contains("impossivel") ||
                   x.Texto.Contains("impossível") ||
                   x.Texto.Contains(" foco ") ||
                   x.Texto.Contains("não possas") ||
                   x.Texto.Contains("impossível") ||
                   x.Texto.Contains("viver bem") ||
                   x.Texto.Contains("pior") ||
                   x.Texto.Contains("fraqueza") ||
                   x.Texto.Contains(" mente ") ||
                   x.Texto.Contains("ponto de partida") ||
                   x.Texto.Contains("sorte") ||
                   x.Texto.Contains("determinação") ||
                   x.Texto.Contains("escolha") ||
                   x.Texto.Contains("sofrimento") ||
                   x.Texto.Contains("sofrer") ||
                   x.Texto.Contains("sofrendo") ||
                   x.Texto.Contains("medo") ||
                   x.Texto.Contains("sacudir") ||
                   x.Texto.Contains("mudar") ||
                   x.Texto.Contains("vitoria") ||
                   x.Texto.Contains("vitórias") ||
                   x.Texto.Contains("motivações") ||
                   x.Texto.Contains("motivação") ||
                   x.Texto.Contains("plano ") ||
                   x.Texto.Contains("desejo") ||
                   x.Texto.Contains("vencer") ||
                   x.Texto.Contains("revoluc") ||
                   x.Texto.Contains("luta") ||
                   x.Texto.Contains("ruins") ||
                   x.Texto.Contains("baixo") 
               )
               && !x.Tags.Select(y => y.Nome).Contains("Motivação")
           );

            foreach (var frase in motivacao)
                script.AppendLine(String.Format("INSERT INTO TagFrase VALUES ('{0}', '{1}', '786b2935-943f-11e2-9b90-b8ac6fe6ba58');", Guid.NewGuid().ToString().Replace("-", ""), frase.Id));




            var frasesDeAmor = base.Filtrar().Where(x => 
                (
                    x.Texto.ToLower().Contains("amor") ||
                    x.Texto.ToLower().Contains("amado") ||
                    x.Texto.ToLower().Contains("paixão") ||
                    x.Texto.ToLower().Contains("paixao") ||
                    x.Texto.ToLower().Contains("namorada") ||
                    x.Texto.ToLower().Contains("marido") ||
                    x.Texto.ToLower().Contains("esposa") ||
                    x.Texto.ToLower().Contains("casado") ||
                    x.Texto.ToLower().Contains("casamento")
                ) 
                && !x.Tags.Select(y=> y.Nome).Contains("amor")
            );
                
            foreach(var frase in frasesDeAmor)
                script.AppendLine(String.Format("INSERT INTO TagFrase VALUES ('{0}', '{1}', '6f95e9f8-943f-11e2-9b90-b8ac6fe6ba58');", Guid.NewGuid().ToString().Replace("-", ""), frase.Id));

                           

            var frasesDeAmizade = base.Filtrar().Where(x =>
                (
                    x.Texto.ToLower().Contains("amigo") ||
                    x.Texto.ToLower().Contains("amizade") ||
                    x.Texto.ToLower().Contains("amiga") ||
                    x.Texto.ToLower().Contains("amigos") ||
                    x.Texto.ToLower().Contains("irmão") ||
                    x.Texto.ToLower().Contains("irmao")                    
                )
                && !x.Tags.Select(y => y.Nome).Contains("amizade")
            );

            foreach (var frase in frasesDeAmizade)
                script.AppendLine(String.Format("INSERT INTO TagFrase VALUES ('{0}', '{1}', 'ef9984b7-943f-11e2-9b90-b8ac6fe6ba59');", Guid.NewGuid().ToString().Replace("-", ""), frase.Id));


            var frasesDePoesia = base.Filtrar().Where(x =>
                (
                    x.Texto.ToLower().Contains("poesia") ||
                    x.Texto.ToLower().Contains("poema") ||
                    x.Texto.ToLower().Contains("poezia") ||
                    x.Texto.ToLower().Contains("texto") ||
                    x.Texto.ToLower().Contains("verso")
                )
                && !x.Tags.Select(y => y.Nome).Contains("poesia")
            );

            foreach (var frase in frasesDePoesia)
                script.AppendLine(String.Format("INSERT INTO TagFrase VALUES ('{0}', '{1}', 'ef9984b7-943f-11e2-9b90-b8ac6fe6ba50');", Guid.NewGuid().ToString().Replace("-", ""), frase.Id));


            var frasesDeAniversario= base.Filtrar().Where(x =>
               (
                   x.Texto.ToLower().Contains("aniversario") ||
                   x.Texto.ToLower().Contains("aniversariante") ||
                   x.Texto.ToLower().Contains("parabéns") ||
                   x.Texto.ToLower().Contains("parabens")
               )
               && !x.Tags.Select(y => y.Nome).Contains("aniversario")
           );

            foreach (var frase in frasesDeAniversario)               
                script.AppendLine(String.Format("INSERT INTO TagFrase VALUES ('{0}', '{1}', 'ef9984b7-943f-11e2-9b90-b8ac6fe6ba50');", Guid.NewGuid().ToString().Replace("-", ""), frase.Id));


            var saudade = base.Filtrar().Where(x =>
               (
                   x.Texto.ToLower().Contains("aniversario") ||
                   x.Texto.ToLower().Contains("aniversariante") ||
                   x.Texto.ToLower().Contains("parabéns") ||
                   x.Texto.ToLower().Contains("parabens")
               )
               && !x.Tags.Select(y => y.Nome).Contains("saudade")
           );

            foreach (var frase in saudade)
                script.AppendLine(String.Format("INSERT INTO TagFrase VALUES ('{0}', '{1}', '7bfff29d-943f-11e2-9b90-b8ac6fe6ba58');", Guid.NewGuid().ToString().Replace("-", ""), frase.Id));

            var natal = base.Filtrar().Where(x =>
               (
                   x.Texto.ToLower().Contains("papai noel") ||
                   x.Texto.ToLower().Contains("natal") ||
                   x.Texto.ToLower().Contains("ano novo") ||
                   x.Texto.ToLower().Contains("fim de ano") ||
                   x.Texto.ToLower().Contains("new year")
               )
               && !x.Tags.Select(y => y.Nome).Contains("natal")
           );

            foreach (var frase in natal)
                script.AppendLine(String.Format("INSERT INTO TagFrase VALUES ('{0}', '{1}', 'd8f0dae6-943f-11e2-9b90-b8ac6fe6ba58');", Guid.NewGuid().ToString().Replace("-", ""), frase.Id));

                     


        }
    }
}
