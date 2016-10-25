using AutoMapper;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Config;
using System;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static void CriarMapeamento()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;

                #region Mapeamento de Contato
                cfg.CreateMap<ContatoModelo, ContatoModeloNegocio>()
                   .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(s => new TipoContatoModeloNegocio { Id = s.IdTipoContato }));

                cfg.CreateMap<ContatoModeloNegocio, ContatoModelo>()
                   .ForMember(dest => dest.IdTipoContato, opt => opt.MapFrom(s => s.TipoContato.Id));
                #endregion

                #region Mapeamento de Email
                cfg.CreateMap<EmailModelo, EmailModeloNegocio>().ReverseMap();
                #endregion

                #region Mapeamento de Endereço
                cfg.CreateMap<EnderecoModeloNegocio, EnderecoModelo>()
                   .ForMember(dest => dest.IdMunicipio, opt => opt.MapFrom(s => s.Municipio.Id));

                cfg.CreateMap<EnderecoModeloNegocio, EnderecoModeloGet>();

                cfg.CreateMap<EnderecoModelo, EnderecoModeloNegocio>()
                   .ForMember(dest => dest.Municipio, opt => opt.MapFrom(s => s.IdMunicipio != default(int) ? new MunicipioModeloNegocio() { Id = s.IdMunicipio } : null));
                #endregion

                #region Mapeamento de Esfera de Organização
                cfg.CreateMap<EsferaOrganizacaoModeloNegocio, EsferaOrganizacaoModelo>();

                cfg.CreateMap<EsferaOrganizacaoModelo, EsferaOrganizacaoModeloNegocio>();

                cfg.CreateMap<EsferaOrganizacaoModeloPost, EsferaOrganizacaoModeloNegocio>();
                #endregion

                #region Mapeamento de Município
                cfg.CreateMap<MunicipioModeloNegocio, MunicipioModeloGet>()
                   .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.HasValue ? src.InicioVigencia.Value.ToString("dd/MM/yyyy") : null))
                   .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<MunicipioModeloPost, MunicipioModeloNegocio>();

                cfg.CreateMap<MunicipioModeloPut, MunicipioModeloNegocio>().ReverseMap();
                #endregion

                #region Mapeamento de Organização
                cfg.CreateMap<OrganizacaoModeloPost, OrganizacaoModeloNegocio>()
                 .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModelo, EnderecoModeloNegocio>(s.Endereco) : null))
                 .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => Mapper.Map<List<EmailModelo>, List<EmailModeloNegocio>>(s.Emails)))
                 .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => Mapper.Map<List<SiteModelo>, List<SiteModeloNegocio>>(s.Sites)))
                 .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => Mapper.Map<List<ContatoModelo>, List<ContatoModeloNegocio>>(s.Contatos)))
                 .ForMember(dest => dest.Poder, opt => opt.MapFrom(s => Mapper.Map<PoderModeloNegocio>(new PoderModeloGet { Id = s.IdPoder })))
                 .ForMember(dest => dest.Esfera, opt => opt.MapFrom(s => Mapper.Map<EsferaOrganizacaoModeloNegocio>(new EsferaOrganizacaoModelo { Id = s.IdEsfera })))
                 .ForMember(dest => dest.OrganizacaoPai, opt => opt.MapFrom(s => s.IdOrganizacaoPai != default(int) ? new OrganizacaoModeloNegocio() { Id = s.IdOrganizacaoPai } : null))
                 .ForMember(dest => dest.TipoOrganizacao, opt => opt.MapFrom(s => Mapper.Map<TipoOrganizacaoModeloNegocio>(new TipoOrganizacaoModelo { Id = s.IdTipoOrganizacao })));

                cfg.CreateMap<OrganizacaoModeloNegocio, OrganizacaoModeloGet>()
                 .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera.Id))
                 .ForMember(dest => dest.IdPoder, opt => opt.MapFrom(s => s.Poder.Id))
                 .ForMember(dest => dest.IdOrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai != null ? s.OrganizacaoPai.Id : 0))
                 .ForMember(dest => dest.IdTipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao.Id))
                 .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera.Id))
                 .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => Mapper.Map<EnderecoModeloNegocio, EnderecoModelo>(s.Endereco)))
                 .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => Mapper.Map<List<EmailModeloNegocio>, List<EmailModelo>>(s.Emails)))
                 .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => Mapper.Map<List<SiteModeloNegocio>, List<SiteModelo>>(s.Sites)))
                 .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => Mapper.Map<List<ContatoModeloNegocio>, List<ContatoModelo>>(s.Contatos)));

                cfg.CreateMap<OrganizacaoModeloNegocio, OrganizacaoUnidadeModeloGet>();
                #endregion

                #region Mapeamento de Poder
                cfg.CreateMap<PoderModeloPut, PoderModeloNegocio>();

                cfg.CreateMap<PoderModeloPost, PoderModeloNegocio>();

                cfg.CreateMap<PoderModeloNegocio, PoderModeloGet>();
                #endregion

                #region Mapeamento de Site
                cfg.CreateMap<SiteModelo, SiteModeloNegocio>().ReverseMap();
                #endregion

                #region Mapeamento de Tipo de Organização
                cfg.CreateMap<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModelo>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<TipoOrganizacaoModeloPut, TipoOrganizacaoModeloNegocio>();

                cfg.CreateMap<TipoOrganizacaoModeloPost, TipoOrganizacaoModeloNegocio>();
                #endregion

                #region Mapeamento de Tipo de Unidade
                cfg.CreateMap<TipoUnidadeModeloNegocio, TipoUnidadeModelo>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<TipoUnidadeModeloPut, TipoUnidadeModeloNegocio>().ReverseMap();

                cfg.CreateMap<TipoUnidadeModeloPost, TipoUnidadeModeloNegocio>();
                #endregion

                #region Mapeamento de Unidade
                cfg.CreateMap<UnidadeModeloNegocio, UnidadeModelo>()
                   .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(s => s.Organizacao.Id))
                   .ForMember(dest => dest.IdTipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade.Id))
                   .ForMember(dest => dest.IdUnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? s.UnidadePai.Id : (int?)null))
                   .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModeloNegocio, EnderecoModelo>(s.Endereco) : null))
                ;

                cfg.CreateMap<UnidadeModeloNegocio, UnidadeModeloGet>()
                   .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade != null ? Mapper.Map<TipoUnidadeModeloNegocio, TipoUnidadeModeloPut>(s.TipoUnidade) : null))
                   .ForMember(dest => dest.Organizacao, opt => opt.MapFrom(s => s.Organizacao != null ? Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoUnidadeModeloGet>(s.Organizacao) : null))
                   .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? Mapper.Map<UnidadeModeloNegocio, UnidadePaiModeloGet>(s.UnidadePai) : null))
                   .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModeloNegocio, EnderecoModeloGet>(s.Endereco) : null))
                ;

                cfg.CreateMap<UnidadeModeloPost, UnidadeModeloNegocio>()
                   .ForMember(dest => dest.Organizacao, opt => opt.MapFrom(s => s.IdOrganizacao != default(int) ? new OrganizacaoModeloNegocio() { Id = s.IdOrganizacao } : null))
                   .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.IdTipoUnidade != default(int) ? new TipoUnidadeModeloNegocio() { Id = s.IdTipoUnidade } : null))
                   .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.IdUnidadePai.HasValue && s.IdUnidadePai.Value != default(int) ? new UnidadeModeloNegocio() { Id = s.IdUnidadePai.Value } : null))
                   .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModelo, EnderecoModeloNegocio>(s.Endereco) : null))
                   .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<ContatoModelo>, List<ContatoModeloNegocio>>(s.Contatos) : null))
                   .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<EmailModelo>, List<EmailModeloNegocio>>(s.Emails) : null))
                   .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => s.Sites != null ? Mapper.Map<List<SiteModelo>, List<SiteModeloNegocio>>(s.Sites) : null));

                cfg.CreateMap<UnidadeModeloNegocio, UnidadePaiModeloGet>();
                #endregion

                #region Importação do mapeamento do Negócio   
                cfg.AddProfile<NegocioProfile>();
                #endregion
            });


        }
    }
}