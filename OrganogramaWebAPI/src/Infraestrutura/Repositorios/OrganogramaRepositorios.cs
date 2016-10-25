using System;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Mapeamento;

namespace Organograma.Infraestrutura.Repositorios
{
    public class OrganogramaRepositorios : IOrganogramaRepositorios
    {
        public OrganogramaRepositorios()
        {
            UnitOfWork = new EFUnitOfWork(new OrganogramaContext());

            Contatos = UnitOfWork.MakeGenericRepository<Contato>();
            ContatosUnidades = UnitOfWork.MakeGenericRepository<ContatoUnidade>();
            Emails = UnitOfWork.MakeGenericRepository<Email>();
            EmailsUnidades = UnitOfWork.MakeGenericRepository<EmailUnidade>();
            Enderecos = UnitOfWork.MakeGenericRepository<Endereco>();
            EsferasOrganizacoes = UnitOfWork.MakeGenericRepository<EsferaOrganizacao>();
            Municipios = UnitOfWork.MakeGenericRepository<Municipio>();
            Organizacoes = UnitOfWork.MakeGenericRepository<Organizacao>();
            Poderes = UnitOfWork.MakeGenericRepository<Poder>();
            Sites = UnitOfWork.MakeGenericRepository<Site>();
            SitesUnidades = UnitOfWork.MakeGenericRepository<SiteUnidade>();
            TiposContatos = UnitOfWork.MakeGenericRepository<TipoContato>();
            TiposOrganizacoes = UnitOfWork.MakeGenericRepository<TipoOrganizacao>();
            TiposUnidades = UnitOfWork.MakeGenericRepository<TipoUnidade>();
            Unidades = UnitOfWork.MakeGenericRepository<Unidade>();
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IRepositorioGenerico<Contato> Contatos { get; private set; }
        public IRepositorioGenerico<ContatoUnidade> ContatosUnidades { get; private set; }
        public IRepositorioGenerico<Email> Emails { get; private set; }
        public IRepositorioGenerico<EmailUnidade> EmailsUnidades { get; private set; }
        public IRepositorioGenerico<Endereco> Enderecos { get; private set; }
        public IRepositorioGenerico<EsferaOrganizacao> EsferasOrganizacoes { get; private set; }
        public IRepositorioGenerico<Municipio> Municipios { get; private set; }
        public IRepositorioGenerico<Organizacao> Organizacoes { get; private set; }
        public IRepositorioGenerico<Poder> Poderes { get; private set; }
        public IRepositorioGenerico<Site> Sites { get; private set; }
        public IRepositorioGenerico<SiteUnidade> SitesUnidades { get; private set; }
        public IRepositorioGenerico<TipoContato> TiposContatos { get; private set; }
        public IRepositorioGenerico<TipoOrganizacao> TiposOrganizacoes { get; private set; }
        public IRepositorioGenerico<TipoUnidade> TiposUnidades { get; private set; }
        public IRepositorioGenerico<Unidade> Unidades { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
