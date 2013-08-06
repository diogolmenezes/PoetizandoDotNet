using Poetizando.Database.Framework.Interface;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Poetizando.Database.Framework
{
    public class RepositorioBase<T> : IDisposable, IRepositorio<T> where T : class
    {
        private DbContext context;

        public RepositorioBase(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            context = unitOfWork as DbContext;
        }

        public RepositorioBase()
        {
            context = new PoetizandoContext();
        }

        public T Carregar(object id)
        {
            return context.Set<T>().Find(id);
        }

        public IQueryable<T> Listar()
        {
            return context.Set<T>();
        }

        public void Incluir(T item)
        {
            context.Set<T>().Add(item);
        }

        public void Excluir(T item)
        {
            context.Set<T>().Remove(item);
        }

        public void Alterar(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public void Salvar()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }   
}
