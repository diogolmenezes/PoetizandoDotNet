using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetizando.Database
{
    public class SetupDao
    {
        public void CriarBaseDeDados()
        {
            using (var contexto = new PoetizandoContext())
            {
                contexto.Database.Create();
            }
        }
    }
}
