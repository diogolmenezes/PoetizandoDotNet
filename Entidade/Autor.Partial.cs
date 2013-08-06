using System;

namespace Poetizando.Entidade
{
    public partial class Autor
    {
        public virtual string ImagemPequena
        {
            get
            {
                return !TemImagem ? "/content/img/autores/pequena/img-autor.png" : string.Format("/content/img/autores/pequena/{0}", Imagem);
            }
        }

        public virtual string ImagemGrande
        {
            get
            {
                return !TemImagem ? "/content/img/autores/img-autor.png" : string.Format("/content/img/autores/{0}", Imagem);
            }
        }

        public virtual bool TemImagem
        {
            get
            {
                return !String.IsNullOrEmpty(Imagem);
            }
        }
    }
}
