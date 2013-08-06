using Poetizando.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetizando.Negocio
{
    public class Setup
    {
        public void CriarBaseDeDados()
        {
            new SetupDao().CriarBaseDeDados();
        }
    }
}
