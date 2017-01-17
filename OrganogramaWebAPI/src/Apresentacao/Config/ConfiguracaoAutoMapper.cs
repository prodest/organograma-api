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

                //cfg.CreateMap<ContatoModeloPut, ContatoModeloNegocio>()
                //   .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(s => new TipoContatoModeloNegocio { Id = s.IdTipoContato }));

                cfg.CreateMap<ContatoModeloNegocio, ContatoModelo>()
                   .ForMember(dest => dest.IdTipoContato, opt => opt.MapFrom(s => s.TipoContato.Id));

                cfg.CreateMap<ContatoModeloNegocio, ContatoModeloGet>();
                #endregion

                #region Mapeamento de Email
                cfg.CreateMap<EmailModelo, EmailModeloNegocio>().ReverseMap();

                cfg.CreateMap<EmailModeloPut, EmailModeloNegocio>();
                #endregion

                #region Mapeamento de Endereço
                cfg.CreateMap<EnderecoModeloNegocio, EnderecoModelo>()
                   .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(s => s.Municipio.Guid));

                cfg.CreateMap<EnderecoModeloNegocio, EnderecoModeloGet>()
                .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio));

                cfg.CreateMap<EnderecoModelo, EnderecoModeloNegocio>()
                   .ForMember(dest => dest.Municipio, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.GuidMunicipio) ? new MunicipioModeloNegocio() { Guid = s.GuidMunicipio } : null));

                cfg.CreateMap<EnderecoModeloPut, EnderecoModeloNegocio>()
                   .ForMember(dest => dest.Municipio, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.GuidMunicipio) ? new MunicipioModeloNegocio() { Guid = s.GuidMunicipio } : null));
                #endregion

                #region Mapeamento de Esfera de Organização
                cfg.CreateMap<EsferaOrganizacaoModeloNegocio, EsferaOrganizacaoModelo>();

                cfg.CreateMap<EsferaOrganizacaoModelo, EsferaOrganizacaoModeloNegocio>();

                cfg.CreateMap<EsferaOrganizacaoModeloPost, EsferaOrganizacaoModeloNegocio>().ReverseMap();
                #endregion

                #region Mapeamento de Município
                cfg.CreateMap<MunicipioModeloNegocio, MunicipioModeloGet>()
                   //.ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.HasValue ? src.InicioVigencia.Value.ToString("dd/MM/yyyy") : null))
                   //.ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null))
                   ;

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
                 .ForMember(dest => dest.OrganizacaoPai, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.GuidOrganizacaoPai) ? new OrganizacaoModeloNegocio() { Guid = s.GuidOrganizacaoPai } : null))
                 .ForMember(dest => dest.TipoOrganizacao, opt => opt.MapFrom(s => Mapper.Map<TipoOrganizacaoModeloNegocio>(new TipoOrganizacaoModelo { Id = s.IdTipoOrganizacao })));

                cfg.CreateMap<OrganizacaoModeloNegocio, OrganizacaoModeloPut>()
                 .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera.Id))
                 .ForMember(dest => dest.IdPoder, opt => opt.MapFrom(s => s.Poder.Id))
                 .ForMember(dest => dest.GuidOrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai != null ? s.OrganizacaoPai.Guid : null))
                 .ForMember(dest => dest.IdTipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao.Id))
                 .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera.Id))
                 .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => Mapper.Map<EnderecoModeloNegocio, EnderecoModelo>(s.Endereco)))
                 .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => Mapper.Map<List<EmailModeloNegocio>, List<EmailModelo>>(s.Emails)))
                 .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => Mapper.Map<List<SiteModeloNegocio>, List<SiteModelo>>(s.Sites)))
                 .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => Mapper.Map<List<ContatoModeloNegocio>, List<ContatoModelo>>(s.Contatos)));

                cfg.CreateMap<OrganizacaoModeloNegocio, OrganizacaoModeloGet>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco))
                .ForMember(dest => dest.Esfera, opt => opt.MapFrom(s => s.Esfera))
                .ForMember(dest => dest.Poder, opt => opt.MapFrom(s => s.Poder))
                .ForMember(dest => dest.OrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai))
                .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => s.Sites))
                .ForMember(dest => dest.TipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao))
                ;

                cfg.CreateMap<OrganizacaoModeloNegocio, OrganizacaoUnidadeModeloGet>();

                cfg.CreateMap<OrganizacaoModeloNegocio, OrganizacaoPaiModeloGet>();

                cfg.CreateMap<OrganizacaoModeloPatch, OrganizacaoModeloNegocio>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.Esfera, opt => opt.MapFrom(s => s.IdEsfera.HasValue ? new EsferaOrganizacaoModeloNegocio() { Id = s.IdEsfera.Value } : null))
                .ForMember(dest => dest.OrganizacaoPai, opt => opt.MapFrom(s => s.IdOrganizacaoPai.HasValue ? new OrganizacaoModeloNegocio() { Id = s.IdOrganizacaoPai.Value } : null))
                .ForMember(dest => dest.Poder, opt => opt.MapFrom(s => s.IdPoder.HasValue ? new PoderModeloNegocio() { Id = s.IdPoder.Value } : null))
                .ForMember(dest => dest.TipoOrganizacao, opt => opt.MapFrom(s => s.IdTipoOrganizacao.HasValue ? new TipoOrganizacaoModeloNegocio() { Id = s.IdTipoOrganizacao.Value } : null));
                
                #endregion

                #region Mapeamento de Poder
                cfg.CreateMap<PoderModeloPut, PoderModeloNegocio>();

                cfg.CreateMap<PoderModeloPost, PoderModeloNegocio>().ReverseMap(); ;

                cfg.CreateMap<PoderModeloNegocio, PoderModeloGet>();
                #endregion

                #region Mapeamento de Site
                cfg.CreateMap<SiteModelo, SiteModeloNegocio>().ReverseMap();

                //cfg.CreateMap<SiteModeloPut, SiteModeloNegocio>();
                #endregion

                #region Mapeamento de Tipo de Organização
                cfg.CreateMap<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModelo>();

                cfg.CreateMap<TipoOrganizacaoModeloPut, TipoOrganizacaoModeloNegocio>();

                cfg.CreateMap<TipoOrganizacaoModeloPost, TipoOrganizacaoModeloNegocio>();

                cfg.CreateMap<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModeloPost>();
                #endregion

                #region Mapeamento de Tipo de Unidade
                cfg.CreateMap<TipoUnidadeModeloNegocio, TipoUnidadeModelo>();

                cfg.CreateMap<TipoUnidadeModeloPut, TipoUnidadeModeloNegocio>().ReverseMap();

                cfg.CreateMap<TipoUnidadeModeloPost, TipoUnidadeModeloNegocio>();
                #endregion

                #region Mapeamento de Tipo Contato
                cfg.CreateMap<TipoContatoModeloNegocio, TipoContatoModeloGet>();
                #endregion

                #region Mapeamento de Unidade
                cfg.CreateMap<UnidadeModeloNegocio, UnidadeModeloRetornoPost>()
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

                cfg.CreateMap<UnidadeModeloPatch, UnidadeModeloNegocio>()
                   .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.IdTipoUnidade.HasValue && s.IdTipoUnidade.Value != default(int) ? new TipoUnidadeModeloNegocio() { Id = s.IdTipoUnidade.Value } : null))
                   .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.IdUnidadePai.HasValue && s.IdUnidadePai.Value != default(int) ? new UnidadeModeloNegocio() { Id = s.IdUnidadePai.Value } : null));

                cfg.CreateMap<UnidadeModeloNegocio, UnidadePaiModeloGet>();
                #endregion

                #region Importação do mapeamento do Negócio   
                cfg.AddProfile<NegocioProfile>();
                #endregion
            });


        }
    }
}