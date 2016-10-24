
using Microsoft.EntityFrameworkCore;
using Organograma.Dominio.Base;

namespace Organograma.Infraestrutura.Repositorios
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public bool AutoSave { get; set; }
        private DbContext _context { get; set; }

        public EFUnitOfWork(DbContext ctx)
        {
            AutoSave = false;
            _context = ctx;
        }

        public IRepositorioGenerico<T> MakeGenericRepository<T>() where T : class
        {
            return new EFRepositorioGenerico<T>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Attach(object entity)
        {
            _context.Attach(entity);
        }

        public void Dispose()
        {
            if (AutoSave)
                Save();

            _context.Dispose();
        }
    }
}
