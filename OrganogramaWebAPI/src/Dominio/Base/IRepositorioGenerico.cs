using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Organograma.Dominio.Base
{
    public interface IRepositorioGenerico<T> : IQueryable<T>
            where T : class
    {
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        T Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
            where TProperty : class;
    }
}
