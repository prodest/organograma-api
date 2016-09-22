using System;

namespace Organograma.Dominio.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool AutoSave { get; set; }
        void Save();
        IRepositorioGenerico<T> MakeGenericRepository<T>() where T : class;
    }
}
