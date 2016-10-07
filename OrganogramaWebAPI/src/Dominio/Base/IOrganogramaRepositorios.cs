using Organograma.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Dominio.Base
{
    public interface IOrganogramaRepositorios : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        IRepositorioGenerico<EsferaOrganizacao> EsferasOrganizacoes { get; }
        IRepositorioGenerico<Municipio> Municipios { get; }
        IRepositorioGenerico<TipoOrganizacao> TiposOrganizacoes { get; }
        IRepositorioGenerico<TipoUnidade> TiposUnidades { get; }

    }
}
