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
            #region Mapeamento de Contato
            CreateMap<ContatoModeloNegocio, Contato>()
                .ForMember(dest => dest.IdTipoContato, opt => opt.MapFrom(s => s.TipoContato.Id));

            CreateMap<ContatoModeloNegocio, ContatoOrganizacao>()
                .ForMember(dest => dest.Contato, opt => opt.MapFrom(s => s));

            CreateMap<ContatoModeloNegocio, ContatoUnidade>()
                .ForMember(dest => dest.Contato, opt => opt.MapFrom(s => s));

            CreateMap<Contato, ContatoModeloNegocio>()
                .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(s => s.TipoContato != null ? Mapper.Map<TipoContato, TipoContatoModeloNegocio>(s.TipoContato) : null));

            CreateMap<ContatoOrganizacao, ContatoModeloNegocio>()
                .ConvertUsing(s => s.Contato != null ? Mapper.Map<Contato, ContatoModeloNegocio>(s.Contato) : null);

            CreateMap<ContatoUnidade, ContatoModeloNegocio>()
                .ConvertUsing(s => s.Contato != null ? Mapper.Map<Contato, ContatoModeloNegocio>(s.Contato) : null);
            #endregion

            #region Mapeamento de Email
            CreateMap<EmailModeloNegocio, Email>()
                .ReverseMap();

            CreateMap<EmailModeloNegocio, EmailOrganizacao>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s));

            CreateMap<EmailModeloNegocio, EmailUnidade>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s));

            CreateMap<EmailOrganizacao, EmailModeloNegocio>()
                .ConvertUsing(s => s.Email != null ? Mapper.Map<Email, EmailModeloNegocio>(s.Email) : null);

            CreateMap<EmailUnidade, EmailModeloNegocio>()
                .ConvertUsing(s => s.Email != null ? Mapper.Map<Email, EmailModeloNegocio>(s.Email) : null);
            #endregion

            #region Mapeamento de Endereço
            CreateMap<Endereco, EnderecoModeloNegocio>()
                .ForMember(dest => dest.Organizacoes, opt => opt.Ignore())
                .ForMember(dest => dest.Unidades, opt => opt.Ignore());

            CreateMap<EnderecoModeloNegocio, Endereco>().ForMember(dest => dest.IdMunicipio, opt => opt.MapFrom(s => s.Municipio.Id));
            #endregion

            #region Mapeamento de EsferaOrganizacao
            CreateMap<EsferaOrganizacao, EsferaOrganizacaoModeloNegocio>().ReverseMap();
            #endregion

            #region Mapeamento de Municipio
            CreateMap<MunicipioModeloNegocio, Municipio>().ReverseMap();

            //CreateMap<Municipio, MunicipioModeloNegocio>();
            #endregion

            #region Mapeamento de Organização
            CreateMap<Organizacao, OrganizacaoModeloNegocio>();

            CreateMap<OrganizacaoModeloNegocio, Organizacao>()
                .ForMember(dest => dest.IdOrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai != null ? s.OrganizacaoPai.Id : (int?)null))
                .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera.Id))
                .ForMember(dest => dest.IdPoder, opt => opt.MapFrom(s => s.Poder.Id))
                .ForMember(dest => dest.IdTipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao.Id))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco))
                .ForMember(dest => dest.EmailsOrganizacao, opt => opt.MapFrom(s => Mapper.Map<List<EmailModeloNegocio>, List<EmailOrganizacao>>(s.Emails)));
            #endregion

            #region Mapeamento de Poder
            CreateMap<Poder, PoderModeloNegocio>().ReverseMap();
            #endregion

            #region Mapeamento de Site
            CreateMap<SiteModeloNegocio, Site>()
                .ReverseMap();

            CreateMap<SiteModeloNegocio, SiteOrganizacao>()
                .ForMember(dest => dest.Site, opt => opt.MapFrom(s => s));

            CreateMap<SiteModeloNegocio, SiteUnidade>()
                .ForMember(dest => dest.Site, opt => opt.MapFrom(s => s));

            CreateMap<SiteOrganizacao, SiteModeloNegocio>()
                .ConvertUsing(s => s.Site != null ? Mapper.Map<Site, SiteModeloNegocio>(s.Site) : null);

            CreateMap<SiteUnidade, SiteModeloNegocio>()
                .ConvertUsing(s => s.Site != null ? Mapper.Map<Site, SiteModeloNegocio>(s.Site) : null);
            #endregion

            #region Mapeamento de Tipo de Contato
            CreateMap<TipoContatoModeloNegocio, TipoContato>().ReverseMap();
            #endregion

            #region Mapeamento de Tipo de Organização
            CreateMap<TipoOrganizacao, TipoOrganizacaoModeloNegocio>().ReverseMap();
            #endregion

            #region Mapeamento de Tipo de Unidade
            CreateMap<TipoUnidade, TipoUnidadeModeloNegocio>().ReverseMap();
            #endregion

            #region Mapeamento de Unidade
            CreateMap<Unidade, UnidadeModeloNegocio>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<Endereco, EnderecoModeloNegocio>(s.Endereco) : null))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.ContatosUnidade != null ? Mapper.Map<List<ContatoUnidade>, List<ContatoModeloNegocio>>(s.ContatosUnidade.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.EmailsUnidade != null ? Mapper.Map<List<EmailUnidade>, List<EmailModeloNegocio>>(s.EmailsUnidade.ToList()) : null))
                .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => s.SitesUnidade != null ? Mapper.Map<List<SiteUnidade>, List<SiteModeloNegocio>>(s.SitesUnidade.ToList()) : null))
                .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.SitesUnidade != null ? Mapper.Map<Unidade, UnidadeModeloNegocio>(s.UnidadePai) : null))
                .MaxDepth(1);
            ;

            CreateMap<UnidadeModeloNegocio, Unidade>()
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(s => s.Organizacao != null ? s.Organizacao.Id : default(int)))
                .ForMember(dest => dest.IdTipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade != null ? s.TipoUnidade.Id : default(int)))
                .ForMember(dest => dest.IdEndereco, opt => opt.MapFrom(s => s.Endereco != null ? s.Endereco.Id : (int?)null))
                .ForMember(dest => dest.IdUnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? s.UnidadePai.Id : (int?)null))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModeloNegocio, Endereco>(s.Endereco) : null))
                .ForMember(dest => dest.Organizacao, opt => opt.MapFrom(s => s.Organizacao != null ? Mapper.Map<OrganizacaoModeloNegocio, Organizacao>(s.Organizacao) : null))
                .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade != null ? Mapper.Map<TipoUnidadeModeloNegocio, TipoUnidade>(s.TipoUnidade) : null))
                .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? Mapper.Map<UnidadeModeloNegocio, Unidade>(s.UnidadePai) : null))
                .ForMember(dest => dest.ContatosUnidade, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<ContatoModeloNegocio>, List<ContatoUnidade>>(s.Contatos) : null))
                .ForMember(dest => dest.EmailsUnidade, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<EmailModeloNegocio>, List<EmailUnidade>>(s.Emails) : null))
                .ForMember(dest => dest.SitesUnidade, opt => opt.MapFrom(s => s.Sites != null ? Mapper.Map<List<SiteModeloNegocio>, List<SiteUnidade>>(s.Sites) : null));
            #endregion
        }
    }
}
