using AutoMapper;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao
{
    public class EsferaOrganizacaoWorkService : IEsferaOrganizacaoWorkService
    {
        private IEsferaOrganizacaoNegocio esferaOrganizacaoNegocio;

        public EsferaOrganizacaoWorkService(IEsferaOrganizacaoNegocio esferaOrganizacaoNegocio)
        {
            this.esferaOrganizacaoNegocio = esferaOrganizacaoNegocio;
        }

        public void Alterar(int id, EsferaOrganizacaoModelo esferaOrganizacao)
        {
            EsferaOrganizacaoModeloNegocio eomn = Mapper.Map<EsferaOrganizacaoModelo, EsferaOrganizacaoModeloNegocio>(esferaOrganizacao);

            esferaOrganizacaoNegocio.Alterar(id, eomn);
        }

        public void Excluir(int id)
        {
            esferaOrganizacaoNegocio.Excluir(id);
        }

        public EsferaOrganizacaoModelo Inserir(EsferaOrganizacaoModeloPost esferaOrganizacao)
        {
            EsferaOrganizacaoModeloNegocio eomn = Mapper.Map<EsferaOrganizacaoModeloPost, EsferaOrganizacaoModeloNegocio>(esferaOrganizacao);

            eomn = esferaOrganizacaoNegocio.Inserir(eomn);

            return Mapper.Map<EsferaOrganizacaoModeloNegocio, EsferaOrganizacaoModelo>(eomn);
        }

        public List<EsferaOrganizacaoModelo> Listar()
        {
            var esferasOrganizacoes = esferaOrganizacaoNegocio.Listar();

            return Mapper.Map<List<EsferaOrganizacaoModeloNegocio>, List<EsferaOrganizacaoModelo>>(esferasOrganizacoes);
        }

        public EsferaOrganizacaoModelo Pesquisar(int id)
        {
            var esferaOrganizacao = esferaOrganizacaoNegocio.Pesquisar(id);

            return Mapper.Map<EsferaOrganizacaoModeloNegocio, EsferaOrganizacaoModelo>(esferaOrganizacao); ;
        }
    }
}
