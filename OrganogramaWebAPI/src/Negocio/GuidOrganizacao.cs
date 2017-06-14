using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Validacao;
using System;
using System.Linq;

namespace Organograma.Negocio
{
    public class GuidOrganizacao : IGuidOrganizacaoProvider
    {
        private IRepositorioGenerico<Organizacao> _repositorioOrganizacoes;
        private OrganizacaoValidacao _validacao;

        public GuidOrganizacao(IOrganogramaRepositorios repositorios)
        {
            _repositorioOrganizacoes = repositorios.Organizacoes;

            _validacao = new OrganizacaoValidacao(_repositorioOrganizacoes);
        }

        public Guid Search(string sigla)
        {
            OrganizacaoModeloNegocio organizacaoNegocio = new OrganizacaoModeloNegocio();

            Guid guid = _repositorioOrganizacoes.Where(o => o.Sigla.Trim().ToUpper().Equals(sigla.Trim().ToUpper()))
                                                              .Select(o => o.IdentificadorExterno.Guid)
                                                              .SingleOrDefault();

            _validacao.NaoEncontrado(guid);

            return guid;
        }

        public Guid SearchPatriarca(Guid guid)
        {
            Organizacao organizacao = _repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(guid))
                                                              .SingleOrDefault();

            _validacao.NaoEncontrado(organizacao);

            int idOrganizacaoPatriarca = ObterOrganizacaoPatriarca(organizacao);

            Guid guidPatriarca = _repositorioOrganizacoes.Where(o => o.Id == idOrganizacaoPatriarca)
                                                .Select(o => o.IdentificadorExterno.Guid)
                                                .SingleOrDefault();

            return guidPatriarca;
        }


        private int ObterOrganizacaoPatriarca(Organizacao organizacao)
        {
            int idOrganizacaoPatriarca = organizacao.Id;

            if (organizacao.IdOrganizacaoPai.HasValue)
            {
                Organizacao organizacaoPai = _repositorioOrganizacoes.Where(o => o.Id == organizacao.IdOrganizacaoPai.Value)
                                                                     .SingleOrDefault();

                idOrganizacaoPatriarca = ObterOrganizacaoPatriarca(organizacaoPai);
            }

            return idOrganizacaoPatriarca;
        }

    }
}
