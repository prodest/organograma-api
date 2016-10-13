using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Organizacao
    {
        public Organizacao()
        {
            ContatoOrganizacao = new HashSet<ContatoOrganizacao>();
            EmailOrganizacao = new HashSet<EmailOrganizacao>();
            SiteOrganizacao = new HashSet<SiteOrganizacao>();
        }

        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int IdEsfera { get; set; }
        public int IdPoder { get; set; }
        public int IdTipoOrganizacao { get; set; }
        public int? IdOrganizacaoPai { get; set; }
        public int IdEndereco { get; set; }

        public virtual ICollection<ContatoOrganizacao> ContatoOrganizacao { get; set; }
        public virtual ICollection<EmailOrganizacao> EmailOrganizacao { get; set; }
        public virtual ICollection<SiteOrganizacao> SiteOrganizacao { get; set; }
        public virtual Endereco IdEnderecoNavigation { get; set; }
        public virtual EsferaOrganizacao IdEsferaNavigation { get; set; }
        public virtual Organizacao IdOrganizacaoPaiNavigation { get; set; }
        public virtual ICollection<Organizacao> InverseIdOrganizacaoPaiNavigation { get; set; }
        public virtual Poder IdPoderNavigation { get; set; }
        public virtual TipoOrganizacao IdTipoOrganizacaoNavigation { get; set; }
    }
}
