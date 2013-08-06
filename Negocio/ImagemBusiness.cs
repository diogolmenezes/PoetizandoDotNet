using Poetizando.Entidade;
using Poetizando.Negocio.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio
{
    public class ImagemBusiness : ServicoBase<Imagem>
    {
        public IList<Imagem> Listar(int inicio, int tamanho)
        {
            return base.Filtrar().Where(i => i.Ativo).OrderByDescending(x=> x.DataCriacao).Skip(inicio).Take(tamanho).ToList();
        }

        public Imagem Recuperar(Frase frase)
        {
            return base.Filtrar().Where(i => i.Ativo && i.Frase.Id == frase.Id).FirstOrDefault();
        }
    }
}
