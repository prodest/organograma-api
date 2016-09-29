using Organograma.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;


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
            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoIbge).HasColumnName("codigoIBGE");

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
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("uf")
                    .HasColumnType("varchar(2)");
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
        }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<TipoOrganizacao> TipoOrganizacao { get; set; }
        public virtual DbSet<TipoUnidade> TipoUnidade { get; set; }

    }
}