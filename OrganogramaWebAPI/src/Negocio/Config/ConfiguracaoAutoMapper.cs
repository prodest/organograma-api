using AutoMapper;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static NegocioProfile GetNegocioProfile()
        {
            return new NegocioProfile();
        }
    }

    public class NegocioProfile : Profile
    {
        public NegocioProfile()
        {
            #region Mapeamento de EsferaOrganizacao
            CreateMap<EsferaOrganizacao, EsferaOrganizacaoModeloNegocio>();
            CreateMap<EsferaOrganizacaoModeloNegocio, EsferaOrganizacao>();
            #endregion

            CreateMap<Municipio, MunicipioModeloNegocio>().ReverseMap();
            CreateMap<Poder, PoderModeloNegocio>().ReverseMap();

            CreateMap<EmailModeloNegocio, Email>().ReverseMap();
            CreateMap<EnderecoModeloNegocio, Endereco>().ReverseMap();
            CreateMap<SiteModeloNegocio, Site>().ReverseMap();
            CreateMap<ContatoModeloNegocio, Contato>().ReverseMap();

            CreateMap<TipoOrganizacao, TipoOrganizacaoModeloNegocio>();
            CreateMap<TipoUnidade, TipoUnidadeModeloNegocio>();
        }
    }




}
