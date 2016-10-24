using Organograma.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organograma.Negocio.Modelos;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Validacao;
using AutoMapper;

namespace Organograma.Negocio
{
    public class UnidadeNegocio : IUnidadeNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Unidade> repositorioUnidades;
        private UnidadeValidacao unidadeValidacao;
        private TipoUnidadeValidacao tipoUnidadeValidacao;
        private OrganizacaoValidacao organizacaoValidacao;
        private EnderecoValidacao enderecoValidacao;
        private ContatoValidacao contatoValidacao;
        private EmailValidacao emailValidacao;
        private SiteValidacao siteValidacao;


        public UnidadeNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioUnidades = repositorios.Unidades;
            unidadeValidacao = new UnidadeValidacao(repositorioUnidades, repositorios.TiposUnidades, repositorios.Organizacoes);
            tipoUnidadeValidacao = new TipoUnidadeValidacao(repositorios.TiposUnidades);
            organizacaoValidacao = new OrganizacaoValidacao(repositorios.Organizacoes);
            enderecoValidacao = new EnderecoValidacao(repositorios.Enderecos, repositorios.Municipios);
            contatoValidacao = new ContatoValidacao(repositorios.Contatos, repositorios.TiposContatos);
            emailValidacao = new EmailValidacao();
            siteValidacao = new SiteValidacao();
        }

        public void Alterar(int id, UnidadeModeloNegocio unidade)
        {
            unidadeValidacao.NaoNula(unidade);

            //unidadeValidacao.IdPreenchido(id);
            //unidadeValidacao.IdPreenchido(unidade.Id);

            unidadeValidacao.IdAlteracaoValido(id, unidade);

            //validacao.DescricaoValida(unidade.Descricao);

            //validacao.DescricaoExistente(unidade.Descricao);

            Unidade eo = repositorioUnidades.Where(e => e.Id == unidade.Id).SingleOrDefault();

            unidadeValidacao.NaoEncontrado(eo);

            //eo.Descricao = unidade.Descricao;

            unitOfWork.Save();
        }

        public void Excluir(int id)
        {
            //unidadeValidacao.IdPreenchido(id);

            var unidade = repositorioUnidades.SingleOrDefault(eo => eo.Id == id);
            unidadeValidacao.NaoEncontrado(unidade);

            repositorioUnidades.Remove(unidade);

            unitOfWork.Save();
        }

        public UnidadeModeloNegocio Inserir(UnidadeModeloNegocio unidade)
        {
            #region Verificação de campos obrigatórios

            unidadeValidacao.NaoNula(unidade);
            unidadeValidacao.Preenchida(unidade);

            tipoUnidadeValidacao.NaoNulo(unidade.TipoUnidade);
            tipoUnidadeValidacao.IdPreenchido(unidade.TipoUnidade);

            organizacaoValidacao.NaoNulo(unidade.Organizacao);
            organizacaoValidacao.IdPreenchido(unidade.Organizacao);

            unidadeValidacao.UnidadePaiPreenchida(unidade.UnidadePai);

            enderecoValidacao.Preenchido(unidade.Endereco);

            contatoValidacao.Preenchido(unidade.Contatos);

            emailValidacao.Preenchido(unidade.Emails);

            siteValidacao.Preenchido(unidade.Sites);

            #endregion

            #region Validação de Negócio

            unidadeValidacao.Valida(unidade);

            tipoUnidadeValidacao.Existe(unidade.TipoUnidade);

            organizacaoValidacao.Existe(unidade.Organizacao);

            unidadeValidacao.UnidadePaiValida(unidade.UnidadePai);

            enderecoValidacao.Valido(unidade.Endereco);

            contatoValidacao.Valido(unidade.Contatos);

            emailValidacao.Valido(unidade.Emails);

            siteValidacao.Valido(unidade.Sites);

            #endregion

            var unid = Mapper.Map<UnidadeModeloNegocio, Unidade>(unidade);

            repositorioUnidades.Add(unid);

            //unitOfWork.Save();

            return Mapper.Map<Unidade, UnidadeModeloNegocio>(unid);
        }

        public List<UnidadeModeloNegocio> Listar()
        {
            var unidades = repositorioUnidades.ToList();

            unidadeValidacao.NaoEncontrado(unidades);

            return Mapper.Map<List<Unidade>, List<UnidadeModeloNegocio>>(unidades);
        }

        public UnidadeModeloNegocio Pesquisar(int id)
        {
            var unidade = repositorioUnidades.OrderBy(eo => eo.Nome)
                                             .SingleOrDefault(eo => eo.Id == id);

            unidadeValidacao.NaoEncontrado(unidade);

            return Mapper.Map<Unidade, UnidadeModeloNegocio>(unidade); ;
        }
    }
}
