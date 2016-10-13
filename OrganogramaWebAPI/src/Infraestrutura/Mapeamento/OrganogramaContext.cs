using Organograma.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Organograma.Infraestrutura.Mapeamento
{
    public partial class OrganogramaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=10.32.254.137;Database=Organograma;User Id=Apl_Organograma;Password=Um9gJ0;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contato>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Telefone).HasColumnName("telefone");

                entity.Property(e => e.TipoTelefone)
                    .HasColumnName("tipoTelefone")
                    .HasDefaultValueSql("1");
            });

            modelBuilder.Entity<ContatoOrganizacao>(entity =>
            {
                entity.HasIndex(e => new { e.IdContato, e.IdOrganizacao })
                    .HasName("UQ_Contato_Organizacao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdContato).HasColumnName("idContato");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.HasOne(d => d.IdContatoNavigation)
                    .WithMany(p => p.ContatoOrganizacao)
                    .HasForeignKey(d => d.IdContato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContatoOrganizacao_Contato");

                entity.HasOne(d => d.IdOrganizacaoNavigation)
                    .WithMany(p => p.ContatoOrganizacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContatoOrganizacao_Organizacao");
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

                entity.HasOne(d => d.IdEmailNavigation)
                    .WithMany(p => p.EmailOrganizacao)
                    .HasForeignKey(d => d.IdEmail)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmailOrganizacao_Email");

                entity.HasOne(d => d.IdOrganizacaoNavigation)
                    .WithMany(p => p.EmailOrganizacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmailOrganizacao_Organizacao");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasColumnName("bairro")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Cep).HasColumnName("cep");

                entity.Property(e => e.Complemento)
                    .HasColumnName("complemento")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IdMunicipio).HasColumnName("idMunicipio");

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasColumnName("logradouro")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Endereco)
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

                entity.HasIndex(e => e.RazaoSocial)
                    .HasName("UK_OrganizacaoRazaoSocial")
                    .IsUnique();

                entity.HasIndex(e => new { e.NomeFantasia, e.IdOrganizacaoPai })
                    .HasName("UK_OrganizacaoNomeFantasia")
                    .IsUnique();

                entity.HasIndex(e => new { e.Sigla, e.IdOrganizacaoPai })
                    .HasName("UK_OrganizacaoSigla")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("cnpj")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.IdEndereco).HasColumnName("idEndereco");

                entity.Property(e => e.IdEsfera).HasColumnName("idEsfera");

                entity.Property(e => e.IdOrganizacaoPai)
                    .IsRequired()
                    .HasColumnName("idOrganizacaoPai");

                entity.Property(e => e.IdPoder).HasColumnName("idPoder");

                entity.Property(e => e.IdTipoOrganizacao).HasColumnName("idTipoOrganizacao");

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasColumnName("nomeFantasia")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razaoSocial")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.IdEnderecoNavigation)
                    .WithMany(p => p.Organizacao)
                    .HasForeignKey(d => d.IdEndereco)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Organizacao_Endereco");

                entity.HasOne(d => d.IdEsferaNavigation)
                    .WithMany(p => p.Organizacao)
                    .HasForeignKey(d => d.IdEsfera)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_EsferaOrganizacao");

                entity.HasOne(d => d.IdOrganizacaoPaiNavigation)
                    .WithMany(p => p.InverseIdOrganizacaoPaiNavigation)
                    .HasForeignKey(d => d.IdOrganizacaoPai)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_OrganizacaoPai");

                entity.HasOne(d => d.IdPoderNavigation)
                    .WithMany(p => p.Organizacao)
                    .HasForeignKey(d => d.IdPoder)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Organizacao_Poder");

                entity.HasOne(d => d.IdTipoOrganizacaoNavigation)
                    .WithMany(p => p.Organizacao)
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

                entity.HasOne(d => d.IdOrganizacaoNavigation)
                    .WithMany(p => p.SiteOrganizacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteOrganizacao_Organizacao");

                entity.HasOne(d => d.IdSiteNavigation)
                    .WithMany(p => p.SiteOrganizacao)
                    .HasForeignKey(d => d.IdSite)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteOrganizacao_Site");
            });

            modelBuilder.Entity<TipoOrganizacao>(entity =>
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

            modelBuilder.Entity<Poder>(entity =>
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


        }

        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<ContatoOrganizacao> ContatoOrganizacao { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<EmailOrganizacao> EmailOrganizacao { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<EsferaOrganizacao> EsferaOrganizacao { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<Organizacao> Organizacao { get; set; }
        public virtual DbSet<Poder> Poder { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<SiteOrganizacao> SiteOrganizacao { get; set; }
        public virtual DbSet<TipoOrganizacao> TipoOrganizacao { get; set; }
        public virtual DbSet<TipoUnidade> TipoUnidade { get; set; }
    }
}