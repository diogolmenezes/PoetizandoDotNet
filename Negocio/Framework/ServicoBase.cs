using Poetizando.Database;
using Poetizando.Database.Framework;
using Poetizando.Database.Framework.Interface;
using Poetizando.Entidade.Framework;
using Poetizando.Negocio.Framework.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poetizando.Negocio.Framework
{
    public class ServicoBase<T> : IServico<T> where T : DmgEntidade
    {
        IUnitOfWork unitOfWork = new PoetizandoContext();
        IRepositorio<T> repositorio;

        public ServicoBase()
        {
            repositorio = new RepositorioBase<T>(unitOfWork);
        }

        public T Carregar(object id)
        {
            return repositorio.Carregar(id);
        }

        public IQueryable<T> Filtrar()
        {
            return repositorio.Listar();
        }

        public IList<T> Listar()
        {
            return repositorio.Listar().ToList();
        }

        public void Incluir(T item)
        {
            item.Id = Guid.NewGuid().ToString().Replace("-", "");
            repositorio.Incluir(item);
            unitOfWork.Save();
        }

        public void Excluir(T item)
        {
            repositorio.Excluir(item);
            unitOfWork.Save();
        }

        public void Alterar(T item)
        {
            repositorio.Alterar(item);
            unitOfWork.Save();
        }

        public void Salvar()
        {
            repositorio.Salvar();
        }

        public void Dispose()
        {
            repositorio.Dispose();
        }
    }
}
