using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Modelos.Siarhes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organograma.Negocio
{
    public class IntegracaoSiarhesNegocio
    {
        private const string _baseUrlSiarhes = "https://api.es.gov.br/siarhes/v1/";
        private IUnitOfWork _unitOfWork;
        private IRepositorioGenerico<Organizacao> _repositorioOrganizacoes;
        private IRepositorioGenerico<Unidade> _repositorioUnidades;
        private IRepositorioGenerico<ContatoOrganizacao> _repositorioContatosOrganizacoes;
        private IRepositorioGenerico<ContatoUnidade> _repositorioContatosUnidades;
        private IRepositorioGenerico<Contato> _repositorioContatos;
        private IRepositorioGenerico<EmailOrganizacao> _repositorioEmailsOrganizacoes;
        private IRepositorioGenerico<EmailUnidade> _repositorioEmailsUnidades;
        private IRepositorioGenerico<Email> _repositorioEmails;
        private IRepositorioGenerico<Endereco> _repositorioEnderecos;
        private IRepositorioGenerico<SiteOrganizacao> _repositorioSitesOrganizacoes;
        private IRepositorioGenerico<SiteUnidade> _repositorioSitesUnidades;
        private IRepositorioGenerico<Site> _repositorioSites;
        private IRepositorioGenerico<TipoOrganizacao> _repositorioTiposOrganizacao;
        private IRepositorioGenerico<TipoUnidade> _repositorioTiposUnidades;
        private IRepositorioGenerico<Historico> _repositorioHistoricos;
        private List<TipoOrganizacao> TiposOrganizacao;
        private List<TipoUnidade> TiposUnidades;
        private List<Municipio> Municipios { get; set; }
        private Organizacao _governoEstado;

        public async Task Integrar(IOrganogramaRepositorios repositorios, string clientAccessToken)
        {
            if (repositorios == null) throw new ArgumentNullException("O repositório não pode ser nulo.");
            if (clientAccessToken == null) throw new ArgumentNullException("O access token não pode ser nulo.");

            _unitOfWork = repositorios.UnitOfWork;
            _repositorioOrganizacoes = repositorios.Organizacoes;
            _repositorioUnidades = repositorios.Unidades;
            _repositorioContatosOrganizacoes = repositorios.ContatosOrganizacoes;
            _repositorioContatosUnidades = repositorios.ContatosUnidades;
            _repositorioContatos = repositorios.Contatos;
            _repositorioEmailsOrganizacoes = repositorios.EmailsOrganizacoes;
            _repositorioEmailsUnidades = repositorios.EmailsUnidades;
            _repositorioEmails = repositorios.Emails;
            _repositorioEnderecos = repositorios.Enderecos;
            _repositorioHistoricos = repositorios.Historicos;
            _repositorioSitesOrganizacoes = repositorios.SitesOrganizacoes;
            _repositorioSitesUnidades = repositorios.SitesUnidades;
            _repositorioSites = repositorios.Sites;
            _repositorioTiposOrganizacao = repositorios.TiposOrganizacoes;
            _repositorioTiposUnidades = repositorios.TiposUnidades;
            TiposOrganizacao = _repositorioTiposOrganizacao.ToList();
            TiposUnidades = _repositorioTiposUnidades.ToList();
            Municipios = repositorios.Municipios.ToList();

            DateTime agora = DateTime.Now;

            List<OrganizacaoSiarhes> organizacoesSiarhes = JsonData.DownloadAsync<List<OrganizacaoSiarhes>>($"{_baseUrlSiarhes}subempresas", clientAccessToken).Result;

            if (organizacoesSiarhes != null && organizacoesSiarhes.Count > 0)
            {
                AtualizarTiposOrganizacao();

                _unitOfWork.Save();

                TiposOrganizacao = _repositorioTiposOrganizacao.ToList();

                await RemoverOrganizacoesExcluidas(organizacoesSiarhes);

                ConverterDados(organizacoesSiarhes, agora);

                _unitOfWork.Save();
            }

            List<UnidadeSiarhes> unidadesSiarhes = JsonData.DownloadAsync<List<UnidadeSiarhes>>($"{_baseUrlSiarhes}organograma", clientAccessToken).Result;

            var idsOrganizacoes = organizacoesSiarhes.Select(os => new { Empresa = os.Empresa, Subempresa = os.Codigo })
                                                     .ToList();

            if (unidadesSiarhes != null && unidadesSiarhes.Count > 0)
            {
                AtualizarTiposUnidade(unidadesSiarhes);

                _unitOfWork.Save();

                TiposUnidades = _repositorioTiposUnidades.ToList();

                await RemoverUnidadesExcluidas(unidadesSiarhes);

                ConverterDados(unidadesSiarhes, agora);

                _unitOfWork.Save();
            }
        }

        private async Task RemoverOrganizacoesExcluidas(List<OrganizacaoSiarhes> organizacoesSiarhes)
        {
            List<Organizacao> organizacoes = await _repositorioOrganizacoes.Where(o => o.Esfera.Descricao.ToUpper().Equals("ESTADUAL")
                                                                                    && o.Poder.Descricao.ToUpper().Equals("EXECUTIVO")
                                                                                    && o.Endereco.Municipio.Uf.ToUpper().Equals("ES"))
                                                                           .Include(o => o.IdentificadorExterno)
                                                                           .Include(i => i.Endereco)
                                                                           .Include(i => i.ContatosOrganizacao).ThenInclude(c => c.Contato)
                                                                           .Include(i => i.SitesOrganizacao).ThenInclude(s => s.Site)
                                                                           .Include(i => i.EmailsOrganizacao).ThenInclude(s => s.Email)
                                                                           .ToListAsync();

            var a = organizacoesSiarhes.Select(o => new { Empresa = (int?)o.Empresa, Codigo = (int?)o.Codigo })
                                       .ToList();

            var organizacoesExcluir = organizacoes.Where(o => !a.Contains(
                                                                        new
                                                                        {
                                                                            Empresa = o.IdEmpresaSiarhes,
                                                                            Codigo = o.IdSubEmpresaSiarhes
                                                                        })
                                                           && !o.Cnpj.Equals("27080530000143"))
                                                  .ToList();

            foreach (Organizacao orgExcluir in organizacoesExcluir)
            {
                await RemoverUnidadesExcluidas(orgExcluir);

                InserirHistorico(orgExcluir, "Exclusão", null);

                ExcluiContatos(orgExcluir);
                ExcluiEndereco(orgExcluir);
                ExcluiEmails(orgExcluir);
                ExcluiSites(orgExcluir);

                _repositorioOrganizacoes.Remove(orgExcluir);
            }
        }

        private async Task RemoverUnidadesExcluidas(Organizacao orgExcluir)
        {
            var unidades = await _repositorioUnidades.Where(u => u.IdOrganizacao == orgExcluir.Id)
                                                     .ToListAsync();

            foreach (var unidade in unidades)
            {
                RemoverUnidadeExcluida(unidade);
            }
        }

        private async Task RemoverUnidadesExcluidas(List<UnidadeSiarhes> unidadesSiarhes)
        {
            List<Unidade> unidades = await _repositorioUnidades.Where(u => u.Organizacao.IdEmpresaSiarhes.HasValue
                                                                        && u.Organizacao.IdEmpresaSiarhes.Value > 0
                                                                        && u.Organizacao.IdSubEmpresaSiarhes.HasValue
                                                                        && u.Organizacao.IdSubEmpresaSiarhes.Value > 0)
                                                               .Include(u => u.IdentificadorExterno)
                                                               .Include(u => u.Endereco)
                                                               .Include(u => u.ContatosUnidade).ThenInclude(cu => cu.Contato)
                                                               .Include(u => u.EmailsUnidade).ThenInclude(eu => eu.Email)
                                                               .Include(u => u.SitesUnidade).ThenInclude(su => su.Site)
                                                               .ToListAsync();

            var identificadoresUnidadesSiarhes = unidadesSiarhes.Select(u => new { Empresa = (int?)u.Empresa, Codigo = RemoveDiacritics(u.Setor.ToUpper()) })
                                                                .ToList();

            List<Unidade> unidadesExcluir = unidades.Where(u => !identificadoresUnidadesSiarhes.Contains(
                                                                                                        new
                                                                                                        {
                                                                                                            Empresa = u.Organizacao.IdEmpresaSiarhes,
                                                                                                            Codigo = u.Sigla
                                                                                                        }))
                                                    .ToList();

            foreach (Unidade uniExcluir in unidadesExcluir)
            {
                RemoverUnidadeExcluida(uniExcluir);
            }
        }

        private void RemoverUnidadeExcluida(Unidade unidade)
        {
            InserirHistorico(unidade, "Exclusão", null);

            if (unidade.Endereco != null)
                ExcluirEndereco(unidade);

            foreach (var cu in unidade.ContatosUnidade)
            {
                ExcluirContato(cu);
            }

            foreach (var eu in unidade.EmailsUnidade)
            {
                ExcluirEmail(eu);
            }

            foreach (var su in unidade.SitesUnidade)
            {
                ExcluirSite(su);
            }

            _repositorioUnidades.Remove(unidade);
        }

        #region Converter Dados
        private void ConverterDados(List<OrganizacaoSiarhes> organizacoesSiarhes, DateTime agora)
        {
            if (organizacoesSiarhes == null) throw new ArgumentNullException("A lista de organizações não pode ser nula.");

            var organizacoes = _repositorioOrganizacoes.Where(o => 1 == 1) //Não foi possível utiliar o ThenInclude sem utiliar o Where
                                                       .Include(e => e.Endereco).ThenInclude(m => m.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                                       .Include(e => e.Esfera)
                                                       .Include(p => p.Poder)
                                                       .Include(c => c.ContatosOrganizacao).ThenInclude(co => co.Contato).ThenInclude(tc => tc.TipoContato)
                                                       .Include(eo => eo.EmailsOrganizacao).ThenInclude(e => e.Email)
                                                       .Include(so => so.SitesOrganizacao).ThenInclude(s => s.Site)
                                                       .Include(to => to.TipoOrganizacao)
                                                       .Include(to => to.IdentificadorExterno)
                                                       .ToList();

            _governoEstado = organizacoes.Where(o => o.Cnpj.Equals("27080530000143"))
                                         .SingleOrDefault();

            if (_governoEstado == null)
                throw new OrganogramaException("A organização Governo do Estado não foi enconrtada.");

            if (organizacoesSiarhes != null && organizacoesSiarhes.Count > 0)
            {
                foreach (var orgSiarhes in organizacoesSiarhes)
                {
                    Organizacao org = null;

                    string cnpj = orgSiarhes.Cgc.ToString().PadLeft(14, '0');
                    org = organizacoes.Where(o => o.IdEmpresaSiarhes == orgSiarhes.Empresa
                                               && o.IdSubEmpresaSiarhes == orgSiarhes.Codigo)
                                      .SingleOrDefault();

                    if (org == null)
                    {
                        org = organizacoes.Where(o => o.Cnpj.Equals(cnpj))
                                          .SingleOrDefault();
                    }
                    else if (!org.Cnpj.Equals(cnpj))
                    {
                        throw new OrganogramaException($"O CNPJ da organização {orgSiarhes.Razao} - {orgSiarhes.Nome} - {orgSiarhes.Fantasia} está diferente do que já está cadastrado");
                    }

                    if (org == null)
                    {
                        org = new Organizacao();
                        org.InicioVigencia = agora;
                        organizacoes.Add(org);

                        org.IdentificadorExterno = ObterIdentificadoresExternos();
                    }

                    org.IdEmpresaSiarhes = orgSiarhes.Empresa;
                    org.IdSubEmpresaSiarhes = orgSiarhes.Codigo;
                    org.RazaoSocial = !string.IsNullOrWhiteSpace(orgSiarhes.Razao) ? orgSiarhes.Razao : "null";
                    org.Cnpj = cnpj;
                    org.NomeFantasia = orgSiarhes.Nome;
                    org.Sigla = orgSiarhes.Fantasia;
                    org.IdEsfera = 1; //estadual
                    org.IdPoder = 1; //executivo
                    org.IdTipoOrganizacao = ObterIdTipoOrganizacao(org.RazaoSocial);
                    PreencherContatosOrganizacao(org, orgSiarhes);
                    PreencherEmailsOrganizacao(org, orgSiarhes);
                    PreencherEndereco(org, orgSiarhes);
                    PreencherSitesOrganizacao(org, orgSiarhes);

                    if (orgSiarhes.Codigo_Pai.HasValue)
                    {
                        var organizacaoPai = organizacoes.Where(o => o.IdEmpresaSiarhes == orgSiarhes.Empresa
                                                                  && o.IdSubEmpresaSiarhes == orgSiarhes.Codigo_Pai)
                                                         .SingleOrDefault();

                        if (organizacaoPai != null)
                        {
                            if (organizacaoPai.Id > 0)
                                org.IdOrganizacaoPai = organizacaoPai.Id;
                            else
                            {
                                if (organizacaoPai.OrganizacoesFilhas == null)
                                    organizacaoPai.OrganizacoesFilhas = new List<Organizacao>();

                                organizacaoPai.OrganizacoesFilhas.Add(org);
                            }
                        }
                        else
                        {
                            organizacaoPai = new Organizacao();
                            organizacaoPai.InicioVigencia = agora;
                            organizacaoPai.IdEmpresaSiarhes = orgSiarhes.Empresa;
                            organizacaoPai.IdSubEmpresaSiarhes = orgSiarhes.Codigo;
                            organizacaoPai.IdentificadorExterno = ObterIdentificadoresExternos();

                            organizacoes.Add(organizacaoPai);

                            organizacaoPai.OrganizacoesFilhas = new List<Organizacao>();
                            organizacaoPai.OrganizacoesFilhas.Add(org);
                        }
                    }
                    else
                    {
                        org.IdOrganizacaoPai = _governoEstado.Id;
                    }

                    if (org.Id == 0)
                        _repositorioOrganizacoes.Add(org);
                }
            }
        }

        private void ConverterDados(List<UnidadeSiarhes> unidadesSiarhes, DateTime agora)
        {
            if (unidadesSiarhes == null) throw new ArgumentNullException("A lista de unidades não pode ser nula.");

            var unidades = _repositorioUnidades.Where(o => o.Organizacao.IdEmpresaSiarhes.HasValue
                                                        && o.Organizacao.IdSubEmpresaSiarhes.HasValue)
                                               .Include(u => u.TipoUnidade)
                                               .Include(u => u.Organizacao).ThenInclude(o => o.IdentificadorExterno)
                                               .Include(u => u.UnidadePai)
                                               .Include(u => u.Endereco).ThenInclude(u => u.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                               .Include(u => u.ContatosUnidade).ThenInclude(u => u.Contato).ThenInclude(u => u.TipoContato)
                                               .Include(u => u.EmailsUnidade).ThenInclude(u => u.Email)
                                               .Include(u => u.SitesUnidade).ThenInclude(u => u.Site)
                                               .Include(u => u.IdentificadorExterno)
                                               .ToList();

            var organizacoes = _repositorioOrganizacoes.Where(o => o.IdEmpresaSiarhes.HasValue
                                                                && o.IdSubEmpresaSiarhes.HasValue)
                                                       .ToList();

            if (unidadesSiarhes != null && unidadesSiarhes.Count > 0)
            {
                foreach (var undSiarhes in unidadesSiarhes)
                {
                    Unidade und = null;

                    und = unidades.Where(u => u.Organizacao.IdEmpresaSiarhes.Value == undSiarhes.Empresa
                                           && RemoveDiacritics(u.Sigla.ToUpper()).Equals(RemoveDiacritics(undSiarhes.Setor.ToUpper())))
                                  .SingleOrDefault();

                    if (und == null)
                    {
                        und = new Unidade();
                        und.InicioVigencia = agora;
                        und.IdentificadorExterno = ObterIdentificadoresExternos();

                        unidades.Add(und);
                        _repositorioUnidades.Add(und);
                    }

                    var organizacao = organizacoes.Where(o => o.IdEmpresaSiarhes.Value == undSiarhes.Empresa
                                                             && o.IdSubEmpresaSiarhes.Value == undSiarhes.Subempresa)
                                                    .SingleOrDefault();

                    if (organizacao == null)
                        throw new Exception($"A organização da unidade {undSiarhes.Setor} - {undSiarhes.NomeSetor} não foi encontrada. Empresa = {undSiarhes.Empresa}, Subempresa = {undSiarhes.Subempresa}.");

                    und.Organizacao = organizacao;
                    und.IdOrganizacao = organizacao.Id;
                    und.Nome = undSiarhes.NomeSetor;
                    und.Sigla = undSiarhes.Setor;
                    und.IdTipoUnidade = ObterIdTipoUnidade(undSiarhes);

                    PreencherContatosUnidade(und, undSiarhes);
                    PreencherEmailsUnidade(und, undSiarhes);
                    PreencherEndereco(und, undSiarhes);
                    PreencherSitesUnidade(und, undSiarhes);

                    if (!string.IsNullOrWhiteSpace(undSiarhes.PaiSetor))
                    {
                        var unidadePai = unidades.Where(u => u.Organizacao.IdEmpresaSiarhes == undSiarhes.Empresa
                                                          && u.Sigla.Equals(undSiarhes.PaiSetor))
                                                 .SingleOrDefault();

                        if (unidadePai != null)
                        {
                            if (unidadePai.Id > 0)
                                und.IdUnidadePai = unidadePai.Id;
                            else
                            {
                                if (unidadePai.UnidadesFilhas == null)
                                    unidadePai.UnidadesFilhas = new List<Unidade>();

                                unidadePai.UnidadesFilhas.Add(und);
                            }
                        }
                        else
                        {
                            var unidadePaiSiarhes = unidadesSiarhes.Where(us => us.Empresa == undSiarhes.Empresa
                                                                             && us.Setor.Equals(undSiarhes.PaiSetor))
                                                                   .SingleOrDefault();

                            if (unidadePaiSiarhes != null)
                            {
                                unidadePai = new Unidade();
                                unidadePai.InicioVigencia = agora;
                                unidadePai.Organizacao = organizacoes.Where(o => o.IdEmpresaSiarhes.Value == unidadePaiSiarhes.Empresa
                                                                              && o.IdSubEmpresaSiarhes.Value == unidadePaiSiarhes.Subempresa)
                                                                     .SingleOrDefault();

                                if (unidadePai.Organizacao == null)
                                    throw new OrganogramaException($"A organização da unidade pai {undSiarhes.Setor} - {undSiarhes.NomeSetor} não foi encontrada. Empresa = {undSiarhes.Empresa}, Subempresa = {undSiarhes.Subempresa}.");

                                unidadePai.IdOrganizacao = unidadePai.Organizacao.Id;
                                unidadePai.Sigla = undSiarhes.PaiSetor;
                                unidadePai.IdentificadorExterno = ObterIdentificadoresExternos();

                                unidades.Add(unidadePai);
                                _repositorioUnidades.Add(unidadePai);

                                unidadePai.UnidadesFilhas = new List<Unidade>();
                                unidadePai.UnidadesFilhas.Add(und);
                            }
                            else
                            {
                                und.IdUnidadePai = null;
                            }
                        }
                    }
                    else
                    {
                        und.IdUnidadePai = null;
                    }
                }
            }
        }

        private int ObterIdTipoOrganizacao(string razaoSocial)
        {
            int idTipoOrganizacao = 0;
            if (!string.IsNullOrWhiteSpace(razaoSocial))
            {
                idTipoOrganizacao = TiposOrganizacao.Where(to => to.Descricao.ToUpper().Equals("AUTARQUIA"))
                                                                .Select(to => to.Id)
                                                                .Single(); //autarquia

                if (razaoSocial.Equals("ESTADO DO ESPIRITO SANTO") || razaoSocial.ToUpper().Contains("SECRETARIA"))
                    idTipoOrganizacao = TiposOrganizacao.Where(to => to.Descricao.ToUpper().Equals("SECRETARIA"))
                                                     .Select(to => to.Id)
                                                     .Single(); //SECRETARIA
                else if (RemoveDiacritics(razaoSocial.ToUpper()).Contains("FUNDACAO"))
                    idTipoOrganizacao = TiposOrganizacao.Where(to => RemoveDiacritics(to.Descricao.ToUpper()).Equals("FUNDACAO"))
                                 .Select(to => to.Id)
                                 .Single(); //FUNDACAO
            }

            return idTipoOrganizacao;
        }

        private int ObterIdTipoUnidade(UnidadeSiarhes undSiarhes)
        {
            if (undSiarhes == null) throw new ArgumentNullException("A unidade não pode ser nula.");

            int idTipoUnidade = 0;

            if (!string.IsNullOrWhiteSpace(undSiarhes.TipoSetor))
            {
                idTipoUnidade = TiposUnidades.Where(tu => RemoveDiacritics(tu.Descricao.ToUpper()).Equals(RemoveDiacritics(undSiarhes.TipoSetor.ToUpper())))
                                             .Select(tu => tu.Id)
                                             .SingleOrDefault();
            }
            else
            {
                idTipoUnidade = TiposUnidades.Where(tu => tu.Descricao.ToLower().Equals("null"))
                                             .Select(tu => tu.Id)
                                             .SingleOrDefault();
            }

            if (idTipoUnidade == 0)
                throw new OrganogramaException("Tipo de unidade não encontrado.");

            return idTipoUnidade;
        }

        private void PreencherContatosOrganizacao(Organizacao organizacao, OrganizacaoSiarhes organizacaoSiarhes)
        {
            if (organizacao == null) throw new ArgumentNullException("A organização não pode ser nula.");
            if (organizacaoSiarhes == null) throw new ArgumentNullException("A organização SIARHES não pode ser nula.");

            if (organizacaoSiarhes.Ddd.HasValue && organizacaoSiarhes.Ddd.Value > 0 && organizacaoSiarhes.Fone.HasValue && organizacaoSiarhes.Fone.Value > 0)
            {
                string telefone = organizacaoSiarhes.Ddd.ToString() + organizacaoSiarhes.Fone.ToString();

                if (organizacao.ContatosOrganizacao != null && organizacao.ContatosOrganizacao.Count > 0)
                {
                    ContatoOrganizacao contatoOrganizacao = organizacao.ContatosOrganizacao.Where(corg => corg.Contato.Telefone.Equals(telefone)
                                                                                                       && corg.Contato.IdTipoContato == 3)
                                                                                           .SingleOrDefault();

                    if (contatoOrganizacao == null)
                    {
                        ExcluirContatos(organizacao);
                        organizacao.ContatosOrganizacao = new List<ContatoOrganizacao>();

                        organizacao.ContatosOrganizacao.Add(new ContatoOrganizacao
                        {
                            Contato = new Contato
                            {
                                Telefone = telefone,
                                IdTipoContato = 3
                            }
                        });
                    }
                    else if (organizacao.ContatosOrganizacao.Count > 1)
                    {
                        List<ContatoOrganizacao> contatosOrganizacao = organizacao.ContatosOrganizacao.Where(corg => !(corg.Contato.Telefone.Equals(telefone)
                                                                                                                    && corg.Contato.IdTipoContato == 3))
                                                                                                      .ToList();

                        ExcluirContatos(contatosOrganizacao);

                    }
                }
                else
                {
                    List<ContatoOrganizacao> contatosOrganizacao = new List<ContatoOrganizacao>(); ;

                    contatosOrganizacao.Add(new ContatoOrganizacao
                    {
                        Contato = new Contato
                        {
                            Telefone = telefone,
                            IdTipoContato = 3
                        }
                    });

                    organizacao.ContatosOrganizacao = contatosOrganizacao;
                }
            }
            else if (organizacao.ContatosOrganizacao != null && organizacao.ContatosOrganizacao.Count > 0)
            {
                ExcluirContatos(organizacao);
                organizacao.ContatosOrganizacao = null;
            }
        }

        private void PreencherContatosUnidade(Unidade unidade, UnidadeSiarhes unidadeSiarhes)
        {
            if (unidade == null) throw new ArgumentNullException("A unidade não pode ser nula.");
            if (unidadeSiarhes == null) throw new ArgumentNullException("A unidade SIARHES não pode ser nula.");

            if (!string.IsNullOrWhiteSpace(unidadeSiarhes.Fone))
            {
                if (unidade.ContatosUnidade != null && unidade.ContatosUnidade.Count > 0)
                {
                    ContatoUnidade contatoUnidade = unidade.ContatosUnidade.Where(corg => corg.Contato.Telefone.Equals(unidadeSiarhes.Fone)
                                                                                       && corg.Contato.IdTipoContato == 3)
                                                                           .SingleOrDefault();

                    if (contatoUnidade == null)
                    {
                        ExcluirContatos(unidade);
                        unidade.ContatosUnidade = new List<ContatoUnidade>();

                        unidade.ContatosUnidade.Add(new ContatoUnidade
                        {
                            Contato = new Contato
                            {
                                Telefone = unidadeSiarhes.Fone,
                                IdTipoContato = 3
                            }
                        });
                    }
                    else if (unidade.ContatosUnidade.Count > 1)
                    {
                        List<ContatoUnidade> contatosUnidade = unidade.ContatosUnidade.Where(corg => !(corg.Contato.Telefone.Equals(unidadeSiarhes.Fone)
                                                                                                    && corg.Contato.IdTipoContato == 3))
                                                                                      .ToList();

                        ExcluirContatos(contatosUnidade);

                    }
                }
                else
                {
                    List<ContatoUnidade> contatosUnidade = new List<ContatoUnidade>(); ;

                    contatosUnidade.Add(new ContatoUnidade
                    {
                        Contato = new Contato
                        {
                            Telefone = unidadeSiarhes.Fone,
                            IdTipoContato = 3
                        }
                    });

                    unidade.ContatosUnidade = contatosUnidade;
                }
            }
            else if (unidade.ContatosUnidade != null && unidade.ContatosUnidade.Count > 0)
            {
                ExcluirContatos(unidade);
                unidade.ContatosUnidade = null;
            }
        }

        private void PreencherEmailsOrganizacao(Organizacao organizacao, OrganizacaoSiarhes organizacaoSiarhes)
        {
            if (organizacao == null) throw new ArgumentNullException("A organização não pode ser nula.");
            if (organizacaoSiarhes == null) throw new ArgumentNullException("A organização SIARHES não pode ser nula.");

            if (!string.IsNullOrWhiteSpace(organizacaoSiarhes.Email))
            {
                if (organizacao.EmailsOrganizacao != null && organizacao.EmailsOrganizacao.Count > 0)
                {
                    EmailOrganizacao emailOrganizacao = organizacao.EmailsOrganizacao.Where(corg => corg.Email.Endereco.Equals(organizacaoSiarhes.Email))
                                                                                     .SingleOrDefault();

                    if (emailOrganizacao == null)
                    {
                        ExcluirEmails(organizacao);

                        List<EmailOrganizacao> emailsOrganizacao = new List<EmailOrganizacao>();

                        emailsOrganizacao.Add(new EmailOrganizacao
                        {
                            Email = new Email
                            {
                                Endereco = organizacaoSiarhes.Email
                            }
                        });
                    }
                    else if (organizacao.EmailsOrganizacao.Count > 1)
                    {
                        List<EmailOrganizacao> emailsOrganizacao = organizacao.EmailsOrganizacao.Where(corg => !corg.Email.Endereco.Equals(organizacaoSiarhes.Email))
                                                                                                .ToList();

                        ExcluirEmails(emailsOrganizacao);

                    }
                }
                else
                {
                    List<EmailOrganizacao> emailsOrganizacao = new List<EmailOrganizacao> { new EmailOrganizacao { Email = new Email { Endereco = organizacaoSiarhes.Email } } };

                    organizacao.EmailsOrganizacao = emailsOrganizacao;
                }
            }
            else if (organizacao.EmailsOrganizacao != null && organizacao.EmailsOrganizacao.Count > 0)
            {
                ExcluirEmails(organizacao);
            }
        }

        private void PreencherEmailsUnidade(Unidade unidade, UnidadeSiarhes unidadeSiarhes)
        {
            if (unidade == null) throw new ArgumentNullException("A unidade não pode ser nula.");
            if (unidadeSiarhes == null) throw new ArgumentNullException("A unidade SIARHES não pode ser nula.");

            if (unidade.EmailsUnidade != null && unidade.EmailsUnidade.Count > 0)
            {
                ExcluirEmails(unidade);
            }
        }

        private void PreencherEndereco(Organizacao organizacao, OrganizacaoSiarhes organizacaoSiarhes)
        {
            if (organizacao == null) throw new ArgumentNullException("A organização não pode ser nula.");
            if (organizacaoSiarhes == null) throw new ArgumentNullException("A organização SIARHES não pode ser nula.");

            if (organizacao.Endereco == null)
                organizacao.Endereco = new Endereco();

            string informacaoNaoFornecida = "Informação não fornecida [SIARHES]";

            organizacao.Endereco.Bairro = !string.IsNullOrWhiteSpace(organizacaoSiarhes.Bairro) ? organizacaoSiarhes.Bairro : informacaoNaoFornecida;
            organizacao.Endereco.Cep = !string.IsNullOrWhiteSpace(organizacaoSiarhes.Cep) ? organizacaoSiarhes.Cep : informacaoNaoFornecida;
            organizacao.Endereco.Complemento = !string.IsNullOrWhiteSpace(organizacaoSiarhes.Complemento) ? organizacaoSiarhes.Complemento : null;
            organizacao.Endereco.IdMunicipio = ObterIdMunicipio(organizacaoSiarhes.Municipio);
            organizacao.Endereco.Logradouro = !string.IsNullOrWhiteSpace(organizacaoSiarhes.Logradouro) ? organizacaoSiarhes.Logradouro : informacaoNaoFornecida;
            organizacao.Endereco.Numero = !string.IsNullOrWhiteSpace(organizacaoSiarhes.NumEnder) ? organizacaoSiarhes.NumEnder : null;
        }

        private void PreencherEndereco(Unidade unidade, UnidadeSiarhes unidadeSiarhes)
        {
            if (unidade == null) throw new ArgumentNullException("A unidade não pode ser nula.");
            if (unidadeSiarhes == null) throw new ArgumentNullException("A unidade SIARHES não pode ser nula.");

            if (!string.IsNullOrWhiteSpace(unidadeSiarhes.Bairro)
                && !string.IsNullOrWhiteSpace(unidadeSiarhes.Cep)
                && !string.IsNullOrWhiteSpace(unidadeSiarhes.Logradouro)
                && !string.IsNullOrWhiteSpace(unidadeSiarhes.Municipio)
                )
            {
                if (unidade.Endereco == null)
                    unidade.Endereco = new Endereco();

                unidade.Endereco.Bairro = unidadeSiarhes.Bairro;

                unidade.Endereco.Cep = unidadeSiarhes.Cep.Trim().Replace("-", "");
                if (unidade.Endereco.Cep.Length > 8)
                    unidade.Endereco.Cep = unidade.Endereco.Cep.Substring(0, 8);

                if (unidade.Endereco.Cep.Length > 8) throw new OrganogramaException($"O CEP deve conter somente 8 caracteres. Unidade:  { unidadeSiarhes.Setor } - { unidadeSiarhes.NomeSetor}. Empresa = { unidadeSiarhes.Empresa}, Subempresa = { unidadeSiarhes.Subempresa}.");
                unidade.Endereco.Complemento = !string.IsNullOrWhiteSpace(unidadeSiarhes.Complemento) ? unidadeSiarhes.Complemento : null;
                unidade.Endereco.IdMunicipio = ObterIdMunicipio(unidadeSiarhes.Municipio);
                unidade.Endereco.Logradouro = unidadeSiarhes.Logradouro;
                unidade.Endereco.Numero = !string.IsNullOrWhiteSpace(unidadeSiarhes.NumEnder) ? unidadeSiarhes.NumEnder : null;
            }
        }

        private void PreencherSitesOrganizacao(Organizacao organizacao, OrganizacaoSiarhes organizacaoSiarhes)
        {
            if (organizacao == null) throw new ArgumentNullException("A organização não pode ser nula.");
            if (organizacaoSiarhes == null) throw new ArgumentNullException("A organização SIARHES não pode ser nula.");

            if (!string.IsNullOrWhiteSpace(organizacaoSiarhes.Web))
            {
                if (organizacao.SitesOrganizacao != null && organizacao.SitesOrganizacao.Count > 0)
                {
                    SiteOrganizacao siteOrganizacao = organizacao.SitesOrganizacao.Where(corg => corg.Site.Url.Equals(organizacaoSiarhes.Web))
                                                                                  .SingleOrDefault();

                    if (siteOrganizacao == null)
                    {
                        ExcluirSites(organizacao);

                        List<SiteOrganizacao> sitesOrganizacao = new List<SiteOrganizacao>();

                        sitesOrganizacao.Add(new SiteOrganizacao
                        {
                            Site = new Site
                            {
                                Url = organizacaoSiarhes.Web
                            }
                        });
                    }
                    else if (organizacao.SitesOrganizacao.Count > 1)
                    {
                        List<SiteOrganizacao> sitesOrganizacao = organizacao.SitesOrganizacao.Where(corg => !corg.Site.Url.Equals(organizacaoSiarhes.Web))
                                                                                             .ToList();

                        ExcluirSites(sitesOrganizacao);

                        sitesOrganizacao = null;
                    }
                }
                else
                {
                    List<SiteOrganizacao> sitesOrganizacao = new List<SiteOrganizacao> { new SiteOrganizacao { Site = new Site { Url = organizacaoSiarhes.Web } } };

                    organizacao.SitesOrganizacao = sitesOrganizacao;
                }
            }
            else if (organizacao.SitesOrganizacao != null && organizacao.SitesOrganizacao.Count > 0)
            {
                ExcluirSites(organizacao);
            }
        }

        private void PreencherSitesUnidade(Unidade unidade, UnidadeSiarhes unidadeSiarhes)
        {
            if (unidade == null) throw new ArgumentNullException("A unidade não pode ser nula.");
            if (unidadeSiarhes == null) throw new ArgumentNullException("A unidade SIARHES não pode ser nula.");

            if (unidade.SitesUnidade != null && unidade.SitesUnidade.Count > 0)
            {
                ExcluirSites(unidade);
            }
        }

        private int ObterIdMunicipio(string municipio)
        {
            if (municipio == null) throw new ArgumentNullException("O município não pode ser nulo.");

            int idMunicipio = 0;

            if (!string.IsNullOrWhiteSpace(municipio))
            {
                idMunicipio = Municipios.Where(m => RemoveDiacritics(m.Nome.ToUpper()).Equals(RemoveDiacritics(municipio.ToUpper())))
                                        .Select(m => m.Id)
                                        .SingleOrDefault();
            }

            if (idMunicipio == 0) throw new Exception("Município não encontrado!");
            return idMunicipio;

        }

        private IdentificadorExterno ObterIdentificadoresExternos()
        {
            return new IdentificadorExterno { Guid = Guid.NewGuid() };
        }
        #endregion

        #region Manipulação de entidades
        private void ExcluirContatos(Organizacao organizacao)
        {
            if (organizacao == null) throw new ArgumentNullException("A organização não pode ser nula.");

            if (organizacao.ContatosOrganizacao != null)
            {
                ExcluirContatos(organizacao.ContatosOrganizacao.ToList());

                organizacao.ContatosOrganizacao = null;
            }
        }

        private void ExcluirContatos(Unidade unidade)
        {
            if (unidade == null) throw new ArgumentNullException("A unidade não pode ser nula.");

            if (unidade.ContatosUnidade != null)
            {
                ExcluirContatos(unidade.ContatosUnidade.ToList());

                unidade.ContatosUnidade = null;
            }
        }

        private void ExcluirContatos(List<ContatoOrganizacao> contatosOrganizacao)
        {
            if (contatosOrganizacao == null) throw new ArgumentNullException("O contato da organização não pode ser nulo.");

            foreach (var contatoOrganizacao in contatosOrganizacao)
            {
                _repositorioContatosOrganizacoes.Remove(contatoOrganizacao);
                _repositorioContatos.Remove(contatoOrganizacao.Contato);
            }
        }

        private void ExcluirContatos(List<ContatoUnidade> contatosUnidade)
        {
            if (contatosUnidade == null) throw new ArgumentNullException("O contato da unidade não pode ser nulo.");

            foreach (var contatoUnidade in contatosUnidade)
            {
                _repositorioContatosUnidades.Remove(contatoUnidade);
                _repositorioContatos.Remove(contatoUnidade.Contato);
            }
        }

        private void ExcluirEmails(Organizacao organizacao)
        {
            if (organizacao == null) throw new ArgumentNullException("A organização não pode ser nula.");

            if (organizacao.EmailsOrganizacao != null)
            {
                ExcluirEmails(organizacao.EmailsOrganizacao.ToList());

                organizacao.EmailsOrganizacao = null;
            }
        }

        private void ExcluirEmails(Unidade unidade)
        {
            if (unidade == null) throw new ArgumentNullException("A unidade não pode ser nula.");

            if (unidade.EmailsUnidade != null)
            {
                ExcluirEmails(unidade.EmailsUnidade.ToList());

                unidade.EmailsUnidade = null;
            }
        }

        private void ExcluirEmails(List<EmailOrganizacao> emailsOrganizacao)
        {
            if (emailsOrganizacao == null) throw new ArgumentNullException("O email da organização não pode ser nulo.");

            foreach (var emailOrganizacao in emailsOrganizacao)
            {
                _repositorioEmailsOrganizacoes.Remove(emailOrganizacao);
                _repositorioEmails.Remove(emailOrganizacao.Email);
            }
        }

        private void ExcluirEmails(List<EmailUnidade> emailsUnidade)
        {
            if (emailsUnidade == null) throw new ArgumentNullException("O email da unidade não pode ser nulo.");

            foreach (var emailUnidade in emailsUnidade)
            {
                _repositorioEmailsUnidades.Remove(emailUnidade);
                _repositorioEmails.Remove(emailUnidade.Email);
            }
        }

        private void ExcluirSites(Organizacao organizacao)
        {
            if (organizacao == null) throw new ArgumentNullException("A organização não pode ser nula.");

            if (organizacao.SitesOrganizacao != null)
            {
                ExcluirSites(organizacao.SitesOrganizacao.ToList());

                organizacao.SitesOrganizacao = null;
            }
        }

        private void ExcluirSites(Unidade unidade)
        {
            if (unidade == null) throw new ArgumentNullException("A unidade não pode ser nula.");

            if (unidade.SitesUnidade != null)
            {
                ExcluirSites(unidade.SitesUnidade.ToList());

                unidade.SitesUnidade = null;
            }
        }

        private void ExcluirSites(List<SiteOrganizacao> sitesOrganizacao)
        {
            if (sitesOrganizacao == null) throw new ArgumentNullException("O site da organização não pode ser nulo.");

            foreach (var siteOrganizacao in sitesOrganizacao)
            {
                _repositorioSitesOrganizacoes.Remove(siteOrganizacao);
                _repositorioSites.Remove(siteOrganizacao.Site);
            }
        }

        private void ExcluirSites(List<SiteUnidade> sitesUnidade)
        {
            if (sitesUnidade == null) throw new ArgumentNullException("O site da unidade não pode ser nulo.");

            foreach (var siteUnidade in sitesUnidade)
            {
                _repositorioSitesUnidades.Remove(siteUnidade);
                _repositorioSites.Remove(siteUnidade.Site);
            }
        }

        private void AtualizarTiposOrganizacao()
        {
            #region Autarquia
            TipoOrganizacao tipoOrganizacao = _repositorioTiposOrganizacao.Where(to => to.Descricao.ToUpper().Equals("AUTARQUIA"))
                                                                          .SingleOrDefault(); //autarquia

            if (tipoOrganizacao == null)
            {
                tipoOrganizacao = new TipoOrganizacao { Descricao = "Autarquia", InicioVigencia = DateTime.Now };

                _repositorioTiposOrganizacao.Add(tipoOrganizacao);
            }
            #endregion

            #region Secretaria
            tipoOrganizacao = _repositorioTiposOrganizacao.Where(to => to.Descricao.ToUpper().Equals("SECRETARIA"))
                                                          .SingleOrDefault(); //secretaria

            if (tipoOrganizacao == null)
            {
                tipoOrganizacao = new TipoOrganizacao { Descricao = "Secretaria", InicioVigencia = DateTime.Now };

                _repositorioTiposOrganizacao.Add(tipoOrganizacao);
            }
            #endregion

            #region Fundação
            tipoOrganizacao = _repositorioTiposOrganizacao.Where(to => RemoveDiacritics(to.Descricao.ToUpper()).Equals("FUNDACAO"))
                                                          .SingleOrDefault(); //fundação

            if (tipoOrganizacao == null)
            {
                tipoOrganizacao = new TipoOrganizacao { Descricao = "Fundação", InicioVigencia = DateTime.Now };

                _repositorioTiposOrganizacao.Add(tipoOrganizacao);
            }
            #endregion
        }

        private void AtualizarTiposUnidade(List<UnidadeSiarhes> unidadesSiarhes)
        {
            if (unidadesSiarhes == null) throw new ArgumentNullException("A lista de unidades não pode ser nula.");

            var tiposUnidadesSiarhes = unidadesSiarhes.GroupBy(us => us.TipoSetor)
                                                      .Select(us => us.Key)
                                                      .ToList();

            foreach (var tus in tiposUnidadesSiarhes)
            {
                string tipoUnidadeSiarhes = tus;

                if (string.IsNullOrWhiteSpace(tipoUnidadeSiarhes))
                    tipoUnidadeSiarhes = "null";

                var tipoUnidade = TiposUnidades.Where(tu => RemoveDiacritics(tu.Descricao.ToUpper()).Equals(RemoveDiacritics(tipoUnidadeSiarhes.ToUpper())))
                                              .SingleOrDefault();

                if (tipoUnidade == null)
                {
                    tipoUnidade = new TipoUnidade { Descricao = tipoUnidadeSiarhes, InicioVigencia = DateTime.Now };

                    _repositorioTiposUnidades.Add(tipoUnidade);
                }
            }
        }
        #endregion

        private string RemoveDiacritics(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                string stFormD = value.Normalize(NormalizationForm.FormD);
                int len = stFormD.Length;
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < len; i++)
                {
                    System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                    if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(stFormD[i]);
                    }
                }

                return (sb.ToString().Normalize(NormalizationForm.FormC));
            }
            else
            {
                return value;
            }
        }

        private void InserirHistorico(Organizacao organizacao, string obsFimVigencia, DateTime? fimvigencia)
        {
            HistoricoOrganizacaoModeloNegocio historicoOrganizacao = Mapper.Map<Organizacao, HistoricoOrganizacaoModeloNegocio>(organizacao);

            string json = JsonData.SerializeObject(historicoOrganizacao);

            Historico historico = new Historico
            {
                Json = json,
                InicioVigencia = organizacao.InicioVigencia,
                FimVigencia = fimvigencia.HasValue ? fimvigencia.Value : DateTime.Now,
                ObservacaoFimVigencia = obsFimVigencia,
                IdIdentificadorExterno = organizacao.IdentificadorExterno.Id
            };

            _repositorioHistoricos.Add(historico);
        }

        private void InserirHistorico(Unidade unidade, string obsFimVigencia, DateTime? fimvigencia)
        {
            HistoricoUnidadeModeloNegocio historicoUnidade = Mapper.Map<Unidade, HistoricoUnidadeModeloNegocio>(unidade);

            string json = JsonData.SerializeObject(historicoUnidade);

            Historico historico = new Historico
            {
                Json = json,
                InicioVigencia = unidade.InicioVigencia,
                FimVigencia = fimvigencia.HasValue ? fimvigencia.Value : DateTime.Now,
                ObservacaoFimVigencia = obsFimVigencia,
                IdIdentificadorExterno = unidade.IdentificadorExterno.Id
            };

            _repositorioHistoricos.Add(historico);
        }

        private void ExcluirContato(ContatoUnidade contatoUnidade)
        {
            _repositorioContatos.Remove(contatoUnidade.Contato);
            _repositorioContatosUnidades.Remove(contatoUnidade);
        }

        private void ExcluirEmail(EmailUnidade emailUnidade)
        {
            _repositorioEmails.Remove(emailUnidade.Email);
            _repositorioEmailsUnidades.Remove(emailUnidade);
        }

        private void ExcluirEndereco(Unidade unidade)
        {
            _repositorioEnderecos.Remove(unidade.Endereco);
        }

        private void ExcluirSite(SiteUnidade siteUnidade)
        {
            _repositorioSites.Remove(siteUnidade.Site);
            _repositorioSitesUnidades.Remove(siteUnidade);
        }

        private void ExcluiContatos(Organizacao organizacao)
        {
            if (organizacao.ContatosOrganizacao != null)
            {
                foreach (var contatoOrganizacao in organizacao.ContatosOrganizacao)
                {
                    _repositorioContatosOrganizacoes.Remove(contatoOrganizacao);
                    _repositorioContatos.Remove(contatoOrganizacao.Contato);
                }

            }
        }

        private void ExcluiEmails(Organizacao organizacao)
        {
            if (organizacao.EmailsOrganizacao != null)
            {
                foreach (var emailOrganizacao in organizacao.EmailsOrganizacao)
                {
                    _repositorioEmailsOrganizacoes.Remove(emailOrganizacao);
                    _repositorioEmails.Remove(emailOrganizacao.Email);
                }

            }

        }

        private void ExcluiEndereco(Organizacao organizacao)
        {
            if (organizacao.Endereco != null)
            {
                _repositorioEnderecos.Remove(organizacao.Endereco);
            }
        }

        private void ExcluiSites(Organizacao organizacao)
        {
            if (organizacao.SitesOrganizacao != null)
            {
                foreach (var siteOrganizacao in organizacao.SitesOrganizacao)
                {
                    _repositorioSitesOrganizacoes.Remove(siteOrganizacao);
                    _repositorioSites.Remove(siteOrganizacao.Site);
                }

            }
        }
    }
}
