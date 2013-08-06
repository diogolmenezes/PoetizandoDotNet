using System;
using System.Linq;

namespace Poetizando.Database.Framework.Interface
{
    public interface IRepositorio<T> : IDisposable where T : class
    {
        //IQueryable<T> Fetch();
        //IEnumerable<T> GetAll();
        //IEnumerable<T> Find(Func<T, bool> predicate);
        //T Single(Func<T, bool> predicate);
        //T First(Func<T, bool> predicate);
        //void Add(T entity);
        //void Delete(T entity);
        //void Attach(T entity);
        //void SaveChanges();
        //void SaveChanges(SaveOptions options);,

        T Carregar(object id);
        IQueryable<T> Listar();
        void Incluir(T item);
        void Excluir(T item);
        void Alterar(T item);
        void Salvar();
    }

    

}
