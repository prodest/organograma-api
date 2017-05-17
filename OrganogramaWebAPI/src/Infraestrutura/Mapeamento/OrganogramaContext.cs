using Organograma.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;

namespace Organograma.Infraestrutura.Mapeamento
{
    public partial class OrganogramaContext : DbContext
    {

        public static string ConnectionString { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<ContatoOrganizacao> ContatoOrganizacao { get; set; }
        public virtual DbSet<ContatoUnidade> ContatoUnidade { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<EmailOrganizacao> EmailOrganizacao { get; set; }
        public virtual DbSet<EmailUnidade> EmailUnidade { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<EsferaOrganizacao> EsferaOrganizacao { get; set; }
        public virtual DbSet<IdentificadorExterno> IdentificadorExterno { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<Organizacao> Organizacao { get; set; }
        public virtual DbSet<Poder> Poder { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<SiteOrganizacao> SiteOrganizacao { get; set; }
        public virtual DbSet<SiteUnidade> SiteUnidade { get; set; }
        public virtual DbSet<TipoContato> TipoContato { get; set; }
        public virtual DbSet<TipoOrganizacao> TipoOrganizacao { get; set; }
        public virtual DbSet<TipoUnidade> TipoUnidade { get; set; }
        public virtual DbSet<Unidade> Unidade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            //optionsBuilder.EnableSensitiveDataLogging();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                optionsBuilder.UseLoggerFactory(new OrganogramaLoggerFactory());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contato>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdTipoContato).HasColumnName("idTipoContato");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasColumnName("telefone")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.TipoContato)
                    .WithMany(p => p.Contatos)
                    .HasForeignKey(d => d.IdTipoContato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contato_TipoContato");
            });

            modelBuilder.Entity<ContatoOrganizacao>(entity =>
            {
                entity.HasIndex(e => new { e.IdContato, e.IdOrganizacao })
                    .HasName("UQ_Contato_Organizacao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdContato).HasColumnName("idContato");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.ContatosOrganizacao)
                    .HasForeignKey(d => d.IdContato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContatoOrganizacao_Contato");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.ContatosOrganizacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContatoOrganizacao_Organizacao");
            });

            modelBuilder.Entity<ContatoUnidade>(entity =>
            {
                entity.HasIndex(e => new { e.IdContato, e.IdUnidade })
                    .HasName("UK_ContatoUnidade_Contato_Unidade")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdContato).HasColumnName("idContato");

                entity.Property(e => e.IdUnidade).HasColumnName("idUnidade");

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.ContatosUnidade)
                    .HasForeignKey(d => d.IdContato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContatoUnidade_Contato");

                entity.HasOne(d => d.Unidade)
                    .WithMany(p => p.ContatosUnidade)
                    .HasForeignKey(d => d.IdUnidade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContatoUnidade_Unidade");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasColumnName("endereco")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<EmailOrganizacao>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmail, e.IdOrganizacao })
                    .HasName("UQ_Email_Organizacao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEmail).HasColumnName("idEmail");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.EmailsOrganizacao)
                    .HasForeignKey(d => d.IdEmail)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmailOrganizacao_Email");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.EmailsOrganizacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmailOrganizacao_Organizacao");
            });

            modelBuilder.Entity<EmailUnidade>(entity =>
            {
                entity.HasIndex(e => new { e.IdEmail, e.IdUnidade })
                    .HasName("UK_EmailUnidade_Email_Unidade")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEmail).HasColumnName("idEmail");

                entity.Property(e => e.IdUnidade).HasColumnName("idUnidade");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.EmailsUnidade)
                    .HasForeignKey(d => d.IdEmail)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmailUnidade_Email");

                entity.HasOne(d => d.Unidade)
                    .WithMany(p => p.EmailsUnidade)
                    .HasForeignKey(d => d.IdUnidade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmailUnidade_Unidade");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasColumnName("bairro")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasColumnName("cep")
                    .HasColumnType("varchar(8)");

                entity.Property(e => e.Complemento)
                    .HasColumnName("complemento")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdMunicipio).HasColumnName("idMunicipio");

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasColumnName("logradouro")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Numero)
                    .HasColumnName("numero")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.Municipio)
                    .WithMany(p => p.Enderecos)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Endereco_Municipio");
            });

            modelBuilder.Entity<EsferaOrganizacao>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_EsferaOrganizacaoDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<IdentificadorExterno>(entity =>
            {
                entity.HasIndex(e => e.Guid)
                    .HasName("UK_IdentificadorExternoGuid")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.IdMunicipio).HasColumnName("idMunicipio");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.Property(e => e.IdUnidade).HasColumnName("idUnidade");

                entity.HasOne(d => d.Municipio)
                    .WithOne(p => p.IdentificadorExterno)
                    .HasForeignKey<IdentificadorExterno>(d => d.IdMunicipio)
                    .HasConstraintName("FK_IdentificadorExterno_Municipio");

                entity.HasOne(d => d.Organizacao)
                    .WithOne(p => p.IdentificadorExterno)
                    .HasForeignKey<IdentificadorExterno>(d => d.IdOrganizacao)
                    .HasConstraintName("FK_IdentificadorExterno_Organizacao");

                entity.HasOne(d => d.Unidade)
                    .WithOne(p => p.IdentificadorExterno)
                    .HasForeignKey<IdentificadorExterno>(d => d.IdUnidade)
                    .HasConstraintName("FK_IdentificadorExterno_Unidade");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasIndex(e => e.CodigoIbge)
                    .HasName("UQ__codigoIbge")
                    .IsUnique();

                entity.HasIndex(e => new { e.Nome, e.Uf })
                    .HasName("UQ_nome_uf")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoIbge).HasColumnName("codigoIbge");

                entity.Property(e => e.FimVigencia)
                    .HasColumnName("fimVigencia")
                    .HasColumnType("datetime");

                entity.Property(e => e.InicioVigencia)
                    .HasColumnName("inicioVigencia")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ObservacaoFimVigencia)
                    .HasColumnName("observacaoFimVigencia")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("uf")
                    .HasColumnType("varchar(2)");
            });

            modelBuilder.Entity<Organizacao>(entity =>
            {
                entity.HasIndex(e => e.Cnpj)
                    .HasName("UK_OrganizacaoCnpj")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("cnpj")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.IdAntigo).HasColumnName("idAntigo");

                entity.Property(e => e.IdEmpresaSiarhes).HasColumnName("idEmpresaSiarhes");

                entity.Property(e => e.IdEndereco).HasColumnName("idEndereco");

                entity.Property(e => e.IdEsfera).HasColumnName("idEsfera");

                entity.Property(e => e.IdOrganizacaoPai).HasColumnName("idOrganizacaoPai");

                entity.Property(e => e.IdPoder).HasColumnName("idPoder");

                entity.Property(e => e.IdSubEmpresaSiarhes).HasColumnName("idSubEmpresaSiarhes");

                entity.Property(e => e.IdTipoOrganizacao).HasColumnName("idTipoOrganizacao");

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasColumnName("nomeFantasia")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razaoSocial")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.Endereco)
                    .WithMany(p => p.Organizacoes)
                    .HasForeignKey(d => d.IdEndereco)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_Endereco");

                entity.HasOne(d => d.Esfera)
                    .WithMany(p => p.Organizacoes)
                    .HasForeignKey(d => d.IdEsfera)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_EsferaOrganizacao");

                entity.HasOne(d => d.OrganizacaoPai)
                    .WithMany(p => p.OrganizacoesFilhas)
                    .HasForeignKey(d => d.IdOrganizacaoPai)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_OrganizacaoPai");

                entity.HasOne(d => d.Poder)
                    .WithMany(p => p.Organizacoes)
                    .HasForeignKey(d => d.IdPoder)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_Poder");

                entity.HasOne(d => d.TipoOrganizacao)
                    .WithMany(p => p.Organizacoes)
                    .HasForeignKey(d => d.IdTipoOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_TipoOrganizacao");
            });

            modelBuilder.Entity<Poder>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UQ__PoderDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<SiteOrganizacao>(entity =>
            {
                entity.HasIndex(e => new { e.IdSite, e.IdOrganizacao })
                    .HasName("UQ_Site_Organizacao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.Property(e => e.IdSite).HasColumnName("idSite");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.SitesOrganizacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteOrganizacao_Organizacao");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SitesOrganizacao)
                    .HasForeignKey(d => d.IdSite)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteOrganizacao_Site");
            });

            modelBuilder.Entity<SiteUnidade>(entity =>
            {
                entity.HasIndex(e => new { e.IdSite, e.IdUnidade })
                    .HasName("UK_SiteUnidade_Site_Unidade")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdSite).HasColumnName("idSite");

                entity.Property(e => e.IdUnidade).HasColumnName("idUnidade");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SitesUnidade)
                    .HasForeignKey(d => d.IdSite)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteUnidade_Site");

                entity.HasOne(d => d.Unidade)
                    .WithMany(p => p.SitesUnidade)
                    .HasForeignKey(d => d.IdUnidade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteUnidade_Unidade");
            });

            modelBuilder.Entity<TipoContato>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_TipoContatoDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.QuantidadeDigitos).HasColumnName("quantidadeDigitos");
            });

            modelBuilder.Entity<TipoOrganizacao>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_TipoOrganizacaoDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FimVigencia)
                    .HasColumnName("fimVigencia")
                    .HasColumnType("datetime");

                entity.Property(e => e.InicioVigencia)
                    .HasColumnName("inicioVigencia")
                    .HasColumnType("datetime");

                entity.Property(e => e.ObservacaoFimVigencia)
                    .HasColumnName("observacaoFimVigencia")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<TipoUnidade>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_TipoUnidadeDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FimVigencia)
                    .HasColumnName("fimVigencia")
                    .HasColumnType("datetime");

                entity.Property(e => e.InicioVigencia)
                    .HasColumnName("inicioVigencia")
                    .HasColumnType("datetime");

                entity.Property(e => e.ObservacaoFimVigencia)
                    .HasColumnName("observacaoFimVigencia")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.HasIndex(e => new { e.IdOrganizacao, e.Nome })
                    .HasName("UK_UnidadeNome")
                    .IsUnique();

                entity.HasIndex(e => new { e.IdOrganizacao, e.Sigla })
                    .HasName("UK_UnidadeSigla")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAntigo).HasColumnName("idAntigo");

                entity.Property(e => e.IdEndereco).HasColumnName("idEndereco");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.Property(e => e.IdTipoUnidade).HasColumnName("idTipoUnidade");

                entity.Property(e => e.IdUnidadePai).HasColumnName("idUnidadePai");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.Endereco)
                    .WithMany(p => p.Unidades)
                    .HasForeignKey(d => d.IdEndereco)
                    .HasConstraintName("FK_Unidade_Endereco");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.Unidades)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Unidade_Organizacao");

                entity.HasOne(d => d.TipoUnidade)
                    .WithMany(p => p.Unidades)
                    .HasForeignKey(d => d.IdTipoUnidade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Unidade_TipoUnidade");

                entity.HasOne(d => d.UnidadePai)
                    .WithMany(p => p.UnidadesFilhas)
                    .HasForeignKey(d => d.IdUnidadePai)
                    .HasConstraintName("FK_Unidade_UnidadePai");
            });
        }
    }
}