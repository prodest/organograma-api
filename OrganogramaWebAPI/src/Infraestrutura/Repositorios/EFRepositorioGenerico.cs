using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Organograma.Dominio.Base;

namespace Organograma.Infraestrutura.Repositorios
{
    public class EFRepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity>
        where TEntity : class
    {
        protected DbSet<TEntity> _set;

        public EFRepositorioGenerico(DbContext ctx)
        {
            _set = ctx.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            return _set.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return _set.Remove(entity).Entity;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _set.RemoveRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            return _set.Update(entity).Entity;
        }

        public IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> path) where TProperty : class
        {
            return _set.Include(path);
        }

        public Type ElementType
        {
            get { return _set.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return _set.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _set.AsQueryable().Provider; }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _set.AsEnumerable<TEntity>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _set.AsEnumerable().GetEnumerator();
        }
    }
}
