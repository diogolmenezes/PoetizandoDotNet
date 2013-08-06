
namespace Poetizando.Entidade
{
    public partial class Imagem
    {
        public virtual string ImagemPequena
        {
            get
            {
                return string.Format("/content/img/frases/pequenas/{0}", Nome.Replace(".png", ".jpg"));
            }
        }

        public virtual string ImagemGrande
        {
            get
            {
                return string.Format("/content/img/frases/{0}", Nome);
            }
        }

    }
}
