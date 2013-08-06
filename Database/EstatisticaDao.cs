using Poetizando.Database.Framework;
using Poetizando.Entidade;

namespace Poetizando.Database
{
    public class EstatisticaDao : RepositorioBase<Estatistica>
    {
        public void Atualizar(Funcionalidade funcionalidade)
        {
            var SQL = string.Format("UPDATE Estatistica SET Acessos = Acessos + 1 WHERE Funcionalidade = '{0}';", funcionalidade.ToString());
            new PoetizandoContext().Database.ExecuteSqlCommand(SQL);
        }
    }
}
