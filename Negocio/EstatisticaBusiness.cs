using Poetizando.Database;
using Poetizando.Entidade;
using Poetizando.Negocio.Framework;

namespace Poetizando.Negocio
{
    public class EstatisticaBusiness : ServicoBase<Tag>
    {
        public void Atualizar(Funcionalidade funcionalidade)
        {            
            new EstatisticaDao().Atualizar(funcionalidade);
        }
    }
}
