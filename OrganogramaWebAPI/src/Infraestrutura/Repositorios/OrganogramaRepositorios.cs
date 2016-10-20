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

            Enderecos = UnitOfWork.MakeGenericRepository<Endereco>();
            EsferasOrganizacoes = UnitOfWork.MakeGenericRepository<EsferaOrganizacao>();
            Organizacoes = UnitOfWork.MakeGenericRepository<Organizacao>();
            Municipios = UnitOfWork.MakeGenericRepository<Municipio>();
            TiposOrganizacoes = UnitOfWork.MakeGenericRepository<TipoOrganizacao>();
            TiposUnidades = UnitOfWork.MakeGenericRepository<TipoUnidade>();
            Poderes = UnitOfWork.MakeGenericRepository<Poder>();
            Unidades = UnitOfWork.MakeGenericRepository<Unidade>();
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IRepositorioGenerico<Endereco> Enderecos { get; private set; }
        public IRepositorioGenerico<EsferaOrganizacao> EsferasOrganizacoes { get; private set; }
        public IRepositorioGenerico<Municipio> Municipios { get; private set; }
        public IRepositorioGenerico<Organizacao> Organizacoes { get; private set; }
        public IRepositorioGenerico<TipoOrganizacao> TiposOrganizacoes { get; private set; }
        public IRepositorioGenerico<TipoUnidade> TiposUnidades { get; private set; }
        public IRepositorioGenerico<Poder> Poderes { get; private set; }
        public IRepositorioGenerico<Unidade> Unidades { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
