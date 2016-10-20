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

            EsferasOrganizacoes = UnitOfWork.MakeGenericRepository<EsferaOrganizacao>();
            Municipios = UnitOfWork.MakeGenericRepository<Municipio>();
            TiposOrganizacoes = UnitOfWork.MakeGenericRepository<TipoOrganizacao>();
            TiposUnidades = UnitOfWork.MakeGenericRepository<TipoUnidade>();
            Poderes = UnitOfWork.MakeGenericRepository<Poder>();
            Organizacoes = UnitOfWork.MakeGenericRepository<Organizacao>();
            Contatos = UnitOfWork.MakeGenericRepository<Contato>();
            TiposContato = UnitOfWork.MakeGenericRepository<TipoContato>();

        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IRepositorioGenerico<EsferaOrganizacao> EsferasOrganizacoes { get; private set; }
        public IRepositorioGenerico<Municipio> Municipios { get; private set; }
        public IRepositorioGenerico<TipoOrganizacao> TiposOrganizacoes { get; private set; }
        public IRepositorioGenerico<TipoUnidade> TiposUnidades { get; private set; }
        public IRepositorioGenerico<Poder> Poderes { get; private set; }
        public IRepositorioGenerico<Organizacao> Organizacoes { get; private set; }
        public IRepositorioGenerico<Contato> Contatos { get; private set; }
        public IRepositorioGenerico<TipoContato> TiposContato { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
