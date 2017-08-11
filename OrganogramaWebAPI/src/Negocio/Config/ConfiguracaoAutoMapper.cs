using AutoMapper;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Modelos.Siarhes;
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

            CreateMap<Contato, HistoricoContatoModeloNegocio>();

            CreateMap<ContatoOrganizacao, ContatoModeloNegocio>()
                .ConvertUsing(s => s.Contato != null ? Mapper.Map<Contato, ContatoModeloNegocio>(s.Contato) : null);

            CreateMap<ContatoOrganizacao, HistoricoContatoModeloNegocio>()
                .ConvertUsing(s => s.Contato != null ? Mapper.Map<Contato, HistoricoContatoModeloNegocio>(s.Contato) : null);

            CreateMap<ContatoUnidade, ContatoModeloNegocio>()
                .ConvertUsing(s => s.Contato != null ? Mapper.Map<Contato, ContatoModeloNegocio>(s.Contato) : null);

            CreateMap<ContatoUnidade, HistoricoContatoModeloNegocio>()
                .ConvertUsing(s => s.Contato != null ? Mapper.Map<Contato, HistoricoContatoModeloNegocio>(s.Contato) : null);

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

            CreateMap<EmailOrganizacao, HistoricoEmailModeloNegocio>()
                .ConvertUsing(s => s.Email != null ? Mapper.Map<Email, HistoricoEmailModeloNegocio>(s.Email) : null);

            CreateMap<EmailUnidade, EmailModeloNegocio>()
                .ConvertUsing(s => s.Email != null ? Mapper.Map<Email, EmailModeloNegocio>(s.Email) : null);

            CreateMap<EmailUnidade, HistoricoEmailModeloNegocio>()
                .ConvertUsing(s => s.Email != null ? Mapper.Map<Email, HistoricoEmailModeloNegocio>(s.Email) : null);

            CreateMap<Email, HistoricoEmailModeloNegocio>();

            #endregion

            #region Mapeamento de Endereço
            CreateMap<Endereco, EnderecoModeloNegocio>()
                .ForMember(dest => dest.Organizacoes, opt => opt.Ignore())
                .ForMember(dest => dest.Unidades, opt => opt.Ignore());

            CreateMap<Endereco, HistoricoEnderecoModeloNegocio>();

            CreateMap<EnderecoModeloNegocio, Endereco>().ForMember(dest => dest.IdMunicipio, opt => opt.MapFrom(s => s.Municipio.Id));
            #endregion

            #region Mapeamento de EsferaOrganizacao
            CreateMap<EsferaOrganizacao, EsferaOrganizacaoModeloNegocio>().ReverseMap();
            #endregion

            #region Mapeamento de Identificador Externo
            CreateMap<IdentificadorExterno, Guid>()
                .ConvertUsing(s => s.Guid);

            CreateMap<IdentificadorExterno, HistoricoIdentificadorExternoModeloNegocio>();
            #endregion

            #region Mapeamento de Municipio
            CreateMap<Municipio, MunicipioModeloNegocio>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(s => s.IdentificadorExterno != null ? s.IdentificadorExterno.Guid.ToString("D") : null));

            CreateMap<MunicipioModeloNegocio, Municipio>()
                .ForMember(dest => dest.IdentificadorExterno, opt =>
                                                            {
                                                                opt.Condition((a, b) => b.IdentificadorExterno == null);
                                                                opt.MapFrom(s => new IdentificadorExterno { Guid = new Guid(s.Guid) });
                                                            });

            //CreateMap<Municipio, MunicipioModeloNegocio>();
            #endregion

            #region Mapeamento de Organização
            CreateMap<Organizacao, OrganizacaoModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.ContatosOrganizacao != null ? Mapper.Map<List<ContatoOrganizacao>, List<ContatoModeloNegocio>>(s.ContatosOrganizacao.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.EmailsOrganizacao != null ? Mapper.Map<List<EmailOrganizacao>, List<EmailModeloNegocio>>(s.EmailsOrganizacao.ToList()) : null))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco))
                .ForMember(dest => dest.Esfera, opt => opt.MapFrom(s => s.Esfera))
                .ForMember(dest => dest.OrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai != null ? Mapper.Map<Organizacao, OrganizacaoModeloNegocio>(s.OrganizacaoPai) : null))
                .ForMember(dest => dest.Poder, opt => opt.MapFrom(s => s.Poder))
                .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => s.SitesOrganizacao != null ? Mapper.Map<List<SiteOrganizacao>, List<SiteModeloNegocio>>(s.SitesOrganizacao.ToList()) : null))
                .ForMember(dest => dest.TipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(s => s.IdentificadorExterno != null ? s.IdentificadorExterno.Guid.ToString("D") : null))
                .ForMember(dest => dest.OrganizacoesFilhas, opt => opt.Ignore())
                .ForMember(dest => dest.Unidades, opt => opt.Ignore())
                .MaxDepth(1)
                .ForAllMembers(opt =>
                {
                    /*O mapeamento só deve ser feito caso o objeto de destino seja nulo ou, para o caso de ser um inteiro, 0
                      Caso o objeto de destino esteja preenchido, o mapeamento não deve ser feito.
                     */
                    opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        bool mapear = false;
                        if (destMember == null)
                            mapear = true;
                        else
                        {
                            //destMember é do tipo object. Esse cast irá falhar quando o tipo não for inteiro.
                            try
                            {
                                int valor = (int)destMember;
                                if (valor == 0)
                                    mapear = true;
                            }
                            catch (Exception)
                            { }
                        }

                        return mapear;
                    });
                });

            CreateMap<OrganizacaoModeloNegocio, Organizacao>()
                .ForMember(dest => dest.IdOrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai != null ? s.OrganizacaoPai.Id : (int?)null))
                .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera != null ? s.Esfera.Id : 0))
                .ForMember(dest => dest.IdPoder, opt => opt.MapFrom(s => s.Poder != null ? s.Poder.Id : 0))
                .ForMember(dest => dest.IdTipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao != null ? s.TipoOrganizacao.Id : 0))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco))
                .ForMember(dest => dest.Esfera, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizacaoPai, opt => opt.Ignore())
                .ForMember(dest => dest.Poder, opt => opt.Ignore())
                .ForMember(dest => dest.TipoOrganizacao, opt => opt.Ignore())
                .ForMember(dest => dest.IdentificadorExterno, opt =>
                                                            {
                                                                opt.Condition((a, b) => b.IdentificadorExterno == null);
                                                                opt.MapFrom(s => new IdentificadorExterno { Guid = new Guid(s.Guid) });
                                                            })
                .ForMember(dest => dest.EmailsOrganizacao, opt => opt.MapFrom(s => Mapper.Map<List<EmailModeloNegocio>, List<EmailOrganizacao>>(s.Emails)))
                .ForMember(dest => dest.SitesOrganizacao, opt => opt.MapFrom(s => Mapper.Map<List<SiteModeloNegocio>, List<SiteOrganizacao>>(s.Sites)))
                .ForMember(dest => dest.ContatosOrganizacao, opt => opt.MapFrom(s => Mapper.Map<List<ContatoModeloNegocio>, List<ContatoOrganizacao>>(s.Contatos)));

            CreateMap<Organizacao, HistoricoOrganizacaoModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.ContatosOrganizacao != null ? Mapper.Map<List<ContatoOrganizacao>, List<HistoricoContatoModeloNegocio>>(s.ContatosOrganizacao.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.EmailsOrganizacao != null ? Mapper.Map<List<EmailOrganizacao>, List<HistoricoEmailModeloNegocio>>(s.EmailsOrganizacao.ToList()) : null))
                .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => s.SitesOrganizacao != null ? Mapper.Map<List<SiteOrganizacao>, List<HistoricoSiteModeloNegocio>>(s.SitesOrganizacao.ToList()) : null))
                .MaxDepth(1);
            #endregion

            #region Mapeamento de Poder
            CreateMap<Poder, PoderModeloNegocio>().ReverseMap();
            #endregion

            #region Mapeamento de Responsável
            CreateMap<FuncionarioSiarhes, UnidadeModeloNegocio.Responsavel>();
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

            CreateMap<SiteOrganizacao, HistoricoSiteModeloNegocio>()
                .ConvertUsing(s => s.Site != null ? Mapper.Map<Site, HistoricoSiteModeloNegocio>(s.Site) : null);

            CreateMap<SiteUnidade, SiteModeloNegocio>()
                .ConvertUsing(s => s.Site != null ? Mapper.Map<Site, SiteModeloNegocio>(s.Site) : null);

            CreateMap<SiteUnidade, HistoricoSiteModeloNegocio>()
                .ConvertUsing(s => s.Site != null ? Mapper.Map<Site, HistoricoSiteModeloNegocio>(s.Site) : null);

            CreateMap<Site, HistoricoSiteModeloNegocio>();

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
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => (s.ContatosUnidade != null && s.ContatosUnidade.Count > 0) ? Mapper.Map<List<ContatoUnidade>, List<ContatoModeloNegocio>>(s.ContatosUnidade.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => (s.EmailsUnidade != null && s.EmailsUnidade.Count > 0) ? Mapper.Map<List<EmailUnidade>, List<EmailModeloNegocio>>(s.EmailsUnidade.ToList()) : null))
                .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => (s.SitesUnidade != null && s.SitesUnidade.Count > 0) ? Mapper.Map<List<SiteUnidade>, List<SiteModeloNegocio>>(s.SitesUnidade.ToList()) : null))
                .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade))
                .ForMember(dest => dest.Organizacao, opt => opt.MapFrom(s => s.Organizacao))
                .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? Mapper.Map<Unidade, UnidadeModeloNegocio>(s.UnidadePai) : null))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(s => s.IdentificadorExterno != null ? s.IdentificadorExterno.Guid.ToString("D") : null))
                .MaxDepth(1)
                .ForAllMembers(opt =>
                {
                    opt.Condition((src, dest, srcMember, destMember) =>
                    {
                        bool mapear = false;
                        if (destMember == null)
                            mapear = true;
                        else
                        {
                            try
                            {
                                int valor = (int)destMember;
                                if (valor == 0)
                                    mapear = true;
                            }
                            catch (Exception)
                            { }
                        }
                        return mapear;
                    });
                });
            ;

            CreateMap<UnidadeModeloNegocio, Unidade>()
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(s => s.Organizacao != null ? s.Organizacao.Id : default(int)))
                .ForMember(dest => dest.IdTipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade != null ? s.TipoUnidade.Id : default(int)))
                .ForMember(dest => dest.IdEndereco, opt => opt.MapFrom(s => s.Endereco != null ? s.Endereco.Id : (int?)null))
                .ForMember(dest => dest.IdUnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? s.UnidadePai.Id : (int?)null))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModeloNegocio, Endereco>(s.Endereco) : null))
                .ForMember(dest => dest.Organizacao, opt => opt.Ignore())
                .ForMember(dest => dest.TipoUnidade, opt => opt.Ignore())
                .ForMember(dest => dest.UnidadePai, opt => opt.Ignore())
                .ForMember(dest => dest.ContatosUnidade, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<ContatoModeloNegocio>, List<ContatoUnidade>>(s.Contatos) : null))
                .ForMember(dest => dest.EmailsUnidade, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<EmailModeloNegocio>, List<EmailUnidade>>(s.Emails) : null))
                .ForMember(dest => dest.SitesUnidade, opt => opt.MapFrom(s => s.Sites != null ? Mapper.Map<List<SiteModeloNegocio>, List<SiteUnidade>>(s.Sites) : null))
                .ForMember(dest => dest.IdentificadorExterno, opt =>
                                                            {
                                                                opt.Condition((a, b) => b.IdentificadorExterno == null);
                                                                opt.MapFrom(s => new IdentificadorExterno { Guid = new Guid(s.Guid) });
                                                            });

            CreateMap<Unidade, HistoricoUnidadeModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.ContatosUnidade != null ? Mapper.Map<List<ContatoUnidade>, List<HistoricoContatoModeloNegocio>>(s.ContatosUnidade.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.EmailsUnidade != null ? Mapper.Map<List<EmailUnidade>, List<HistoricoEmailModeloNegocio>>(s.EmailsUnidade.ToList()) : null))
                .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => s.SitesUnidade != null ? Mapper.Map<List<SiteUnidade>, List<HistoricoSiteModeloNegocio>>(s.SitesUnidade.ToList()) : null))
                .MaxDepth(1);
            #endregion
        }
    }

    /*
    public class EsferaOrganizacaoCustomResolver : IValueResolver<OrganizacaoModeloNegocio, Organizacao, int>
    {
        public int Resolve(OrganizacaoModeloNegocio source, Organizacao destination, int destMember, ResolutionContext context)
        {
            if (source.Esfera != null)
            {
                return source.Esfera.Id;
            } else {
                return destination.IdEsfera;
            }
        }
    }
    */

}
