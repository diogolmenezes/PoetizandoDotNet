using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio.Framework.Interface
{
    public interface IServico<T> where T : class
    {
        T Carregar(object id);
        IQueryable<T> Filtrar();
        IList<T> Listar();
        void Incluir(T item);
        void Excluir(T item);
        void Alterar(T item);
        void Salvar();
    }
}

