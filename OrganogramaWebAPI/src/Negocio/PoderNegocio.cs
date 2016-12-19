using AutoMapper;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Validacao;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio
{
    public class PoderNegocio : IPoderNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Poder> repositorioPoderes;
        PoderValidacao validacao;

        public PoderNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioPoderes = repositorios.Poderes;
            validacao = new PoderValidacao(repositorioPoderes);
        }

        public List<PoderModeloNegocio> Listar()
        {
            List<Poder> poderes = repositorioPoderes.OrderBy(o => o.Descricao).ToList();
            
            return Mapper.Map<List<Poder>, List<PoderModeloNegocio>>(poderes);
            
        }

        public PoderModeloNegocio Pesquisar(int id)
        {

            Poder poder = repositorioPoderes.Where(p => p.Id == id).SingleOrDefault();
            validacao.NaoEncontrado(poder);
            return Mapper.Map<Poder, PoderModeloNegocio>(poder);

        }

        public void Alterar(int id, PoderModeloNegocio poderNegocio)
        {

            validacao.PoderValido(poderNegocio);
            validacao.IdValido(id);
            validacao.IdValido(poderNegocio.Id);
            validacao.IdAlteracaoValido(id, poderNegocio);
            validacao.PoderExiste(poderNegocio);
            validacao.DescricaoValida(poderNegocio);
            validacao.DescricaoExistente(poderNegocio);
            
            Poder poder = repositorioPoderes.Where(p => p.Id == id).SingleOrDefault();
            Mapper.Map(poderNegocio, poder);
            unitOfWork.Save();

        }

        public void Excluir(int id)
        {
            validacao.IdValido(id);
            Poder poder = repositorioPoderes.Where(p => p.Id == id).SingleOrDefault();
            validacao.NaoEncontrado(poder);

            repositorioPoderes.Remove(poder);
            unitOfWork.Save();
        }

        public PoderModeloNegocio Inserir(PoderModeloNegocio poderNegocio)
        {
            validacao.DescricaoValida(poderNegocio);
            validacao.DescricaoExistente(poderNegocio);

            Poder poder = Mapper.Map<PoderModeloNegocio, Poder>(poderNegocio);
            repositorioPoderes.Add(poder);
            unitOfWork.Save();

            return Mapper.Map(poder, poderNegocio);

        }
    }
}
