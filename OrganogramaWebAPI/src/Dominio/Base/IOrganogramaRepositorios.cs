using Organograma.Dominio.Modelos;
using System;

namespace Organograma.Dominio.Base
{
    public interface IOrganogramaRepositorios : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IRepositorioGenerico<Contato> Contatos { get; }
        IRepositorioGenerico<ContatoUnidade> ContatosUnidades { get; }
        IRepositorioGenerico<ContatoOrganizacao> ContatosOrganizacoes { get; }
        IRepositorioGenerico<Email> Emails { get; }
        IRepositorioGenerico<Endereco> Enderecos { get; }
        IRepositorioGenerico<EmailOrganizacao> EmailsOrganizacoes { get; }
        IRepositorioGenerico<EmailUnidade> EmailsUnidades { get; }
        IRepositorioGenerico<EsferaOrganizacao> EsferasOrganizacoes { get; }
        IRepositorioGenerico<Historico> Historicos { get; }
        IRepositorioGenerico<IdentificadorExterno> IdentificadoresExternos { get; }
        IRepositorioGenerico<Municipio> Municipios { get; }
        IRepositorioGenerico<Organizacao> Organizacoes { get; }
        IRepositorioGenerico<Poder> Poderes { get; }
        IRepositorioGenerico<Site> Sites { get; }
        IRepositorioGenerico<SiteOrganizacao> SitesOrganizacoes { get; }
        IRepositorioGenerico<SiteUnidade> SitesUnidades { get; }
        IRepositorioGenerico<TipoContato> TiposContatos { get; }
        IRepositorioGenerico<TipoOrganizacao> TiposOrganizacoes { get; }
        IRepositorioGenerico<TipoUnidade> TiposUnidades { get; }
        IRepositorioGenerico<Unidade> Unidades { get; }
    }
}
