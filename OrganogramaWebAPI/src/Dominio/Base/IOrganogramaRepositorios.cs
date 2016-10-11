using Organograma.Dominio.Modelos;
using System;

namespace Organograma.Dominio.Base
{
    public interface IOrganogramaRepositorios : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        IRepositorioGenerico<EsferaOrganizacao> EsferasOrganizacoes { get; }
        IRepositorioGenerico<Municipio> Municipios { get; }
        IRepositorioGenerico<TipoOrganizacao> TiposOrganizacoes { get; }
        IRepositorioGenerico<TipoUnidade> TiposUnidades { get; }
        IRepositorioGenerico<Poder> Poderes { get; }


    }
}
