using Poetizando.Entidade.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    public enum Funcionalidade
    {
        Home = 0,
        DetalheFrase,
        DetalheAutor,  
        DetalheImagem,
        CorrecaoFrase,
        CriacaoFrase          
    }
}
