using Organograma.Dominio.Modelos;
using System;

namespace Organograma.Dominio.Base
{
    public interface IOrganogramaRepositorios : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        IRepositorioGenerico<Endereco> Enderecos { get; }
        IRepositorioGenerico<EsferaOrganizacao> EsferasOrganizacoes { get; }
        IRepositorioGenerico<Municipio> Municipios { get; }
        IRepositorioGenerico<Organizacao> Organizacoes { get; }
        IRepositorioGenerico<TipoOrganizacao> TiposOrganizacoes { get; }
        IRepositorioGenerico<TipoUnidade> TiposUnidades { get; }
        IRepositorioGenerico<Poder> Poderes { get; }
        IRepositorioGenerico<Unidade> Unidades { get; }

    }
}
