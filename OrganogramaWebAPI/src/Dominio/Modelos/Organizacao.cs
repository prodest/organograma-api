using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Organizacao
    {
        public Organizacao()
        {
            ContatosOrganizacao = new HashSet<ContatoOrganizacao>();
            EmailsOrganizacao = new HashSet<EmailOrganizacao>();
            SitesOrganizacao = new HashSet<SiteOrganizacao>();
            Unidades = new HashSet<Unidade>();
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
        public int? IdAntigo { get; set; }

        public virtual ICollection<ContatoOrganizacao> ContatosOrganizacao { get; set; }
        public virtual ICollection<EmailOrganizacao> EmailsOrganizacao { get; set; }
        public virtual IdentificadorExterno IdentificadorExterno { get; set; }
        public virtual ICollection<SiteOrganizacao> SitesOrganizacao { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual EsferaOrganizacao Esfera { get; set; }
        public virtual Organizacao OrganizacaoPai { get; set; }
        public virtual ICollection<Organizacao> OrganizacoesFilhas { get; set; }
        public virtual Poder Poder { get; set; }
        public virtual TipoOrganizacao TipoOrganizacao { get; set; }
    }
}
