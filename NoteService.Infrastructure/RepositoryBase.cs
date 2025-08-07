using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Repositories;
using System.Linq.Expressions;

namespace NoteService.Infrastructure
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext) =>
            RepositoryContext = repositoryContext;
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
          RepositoryContext.Set<T>()
            .Where(expression)
            .AsNoTracking() :
          RepositoryContext.Set<T>()
            .Where(expression);

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
