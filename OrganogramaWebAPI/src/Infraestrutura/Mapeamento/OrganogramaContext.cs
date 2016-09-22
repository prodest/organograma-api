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
            modelBuilder.Entity<Atividades>(entity =>
            {
                entity.HasKey(e => e.Idatividade)
                    .HasName("PK_ATIVIDADES");

                entity.ToTable("ATIVIDADES");

                entity.Property(e => e.Idatividade)
                    .HasColumnName("IDATIVIDADE")
                    .HasColumnType("decimal");

                entity.Property(e => e.Classe)
                    .HasColumnName("CLASSE")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Denominacao)
                    .HasColumnName("DENOMINACAO")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Divisao)
                    .HasColumnName("DIVISAO")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Grupo)
                    .HasColumnName("GRUPO")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Obsfimvigencia)
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Pai)
                    .HasColumnName("PAI")
                    .HasColumnType("decimal");

                entity.Property(e => e.Secao)
                    .HasColumnName("SECAO")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Subclasse)
                    .HasColumnName("SUBCLASSE")
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Contatos>(entity =>
            {
                entity.HasKey(e => e.Idparte)
                    .HasName("PK__CONTATOS__4F3250F647A6A41B");

                entity.ToTable("CONTATOS");

                entity.Property(e => e.Idparte).HasColumnName("IDPARTE");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasColumnName("BAIRRO")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Caixapostal)
                    .HasColumnName("CAIXAPOSTAL")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Celular1)
                    .HasColumnName("CELULAR1")
                    .HasColumnType("decimal");

                entity.Property(e => e.Celular1ddd)
                    .HasColumnName("CELULAR1DDD")
                    .HasColumnType("decimal");

                entity.Property(e => e.Celular2)
                    .HasColumnName("CELULAR2")
                    .HasColumnType("decimal");

                entity.Property(e => e.Celular2ddd)
                    .HasColumnName("CELULAR2DDD")
                    .HasColumnType("decimal");

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasColumnType("decimal");

                entity.Property(e => e.Complemento)
                    .HasColumnName("COMPLEMENTO")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Faxddd)
                    .HasColumnName("FAXDDD")
                    .HasColumnType("decimal");

                entity.Property(e => e.Faxnumero)
                    .HasColumnName("FAXNUMERO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fone1)
                    .HasColumnName("FONE1")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone1ddd)
                    .HasColumnName("FONE1DDD")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone1ramal)
                    .HasColumnName("FONE1RAMAL")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone2)
                    .HasColumnName("FONE2")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone2ddd)
                    .HasColumnName("FONE2DDD")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone2ramal)
                    .HasColumnName("FONE2RAMAL")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone3)
                    .HasColumnName("FONE3")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone3ddd)
                    .HasColumnName("FONE3DDD")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fone3ramal)
                    .HasColumnName("FONE3RAMAL")
                    .HasColumnType("decimal");

                entity.Property(e => e.Homepage)
                    .HasColumnName("HOMEPAGE")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Idcontato)
                    .HasColumnName("IDCONTATO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Idmunicipio)
                    .HasColumnName("IDMUNICIPIO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasColumnName("LOGRADOURO")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasColumnName("NUMERO")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.Obsfimvigencia)
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<FunionalidadesPerfisacesso>(entity =>
            {
                entity.HasKey(e => e.Idfuncionalidade)
                    .HasName("PK__FUNIONAL__59CA2D9419AACF41");

                entity.ToTable("FUNIONALIDADES_PERFISACESSO");

                entity.Property(e => e.Idfuncionalidade).HasColumnName("IDFUNCIONALIDADE");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idperfilacesso)
                    .HasColumnName("IDPERFILACESSO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<LogSistema>(entity =>
            {
                entity.HasKey(e => e.Idlogsistema)
                    .HasName("PK__LOG_SIST__7E67D2252334397B");

                entity.ToTable("LOG_SISTEMA");

                entity.Property(e => e.Idlogsistema).HasColumnName("IDLOGSISTEMA");

                entity.Property(e => e.Browser)
                    .HasColumnName("BROWSER")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DataEvento)
                    .HasColumnName("DATA_EVENTO")
                    .HasColumnType("datetime");

                entity.Property(e => e.IpUsuario)
                    .HasColumnName("IP_USUARIO")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Javascript)
                    .HasColumnName("JAVASCRIPT")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.LoginUsuario)
                    .HasColumnName("LOGIN_USUARIO")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Mensagem)
                    .HasColumnName("MENSAGEM")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Rastro)
                    .HasColumnName("RASTRO")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("varchar(500)");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.Idmunicipio)
                    .HasName("PK_MUNICIPIOS");

                entity.ToTable("MUNICIPIOS");

                entity.Property(e => e.Idmunicipio)
                    .HasColumnName("IDMUNICIPIO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Codigoibge)
                    .HasColumnName("CODIGOIBGE")
                    .HasColumnType("decimal");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Obsfimvigencia)
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("UF")
                    .HasColumnType("char(2)");
            });

            modelBuilder.Entity<Organizacoes>(entity =>
            {
                entity.HasKey(e => e.Idparte)
                    .HasName("PK__ORGANIZA__4F3250F623F3538A");

                entity.ToTable("ORGANIZACOES");

                entity.Property(e => e.Idparte).HasColumnName("IDPARTE");

                entity.Property(e => e.Aceitaboletimeletronico)
                    .HasColumnName("ACEITABOLETIMELETRONICO")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Cnpj)
                    .HasColumnName("CNPJ")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Esfera)
                    .HasColumnName("ESFERA")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idatividade)
                    .HasColumnName("IDATIVIDADE")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Idnaturezajuridica)
                    .HasColumnName("IDNATUREZAJURIDICA")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nomefantasia)
                    .HasColumnName("NOMEFANTASIA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Obsfimvigencia)
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Poder)
                    .HasColumnName("PODER")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Razaosocial)
                    .HasColumnName("RAZAOSOCIAL")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Sigla)
                    .HasColumnName("SIGLA")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Titular)
                    .HasColumnName("TITULAR")
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Partes>(entity =>
            {
                entity.HasKey(e => e.Idparte)
                    .HasName("PK__PARTES__4F3250F63FD07829");

                entity.ToTable("PARTES");

                entity.Property(e => e.Idparte).HasColumnName("IDPARTE");

                entity.Property(e => e.Idtipoparte)
                    .IsRequired()
                    .HasColumnName("IDTIPOPARTE")
                    .HasColumnType("char(10)");
            });

            modelBuilder.Entity<PerfisAcesso>(entity =>
            {
                entity.HasKey(e => e.Idperfilacesso)
                    .HasName("PK__PERFIS_A__402EB49D4959E263");

                entity.ToTable("PERFIS_ACESSO");

                entity.Property(e => e.Idperfilacesso).HasColumnName("IDPERFILACESSO");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("DESCRICAO")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<PerfisVisibilidade>(entity =>
            {
                entity.HasKey(e => e.Idperfilvisibilidade)
                    .HasName("PK__PERFIS_V__3D165AFF4D2A7347");

                entity.ToTable("PERFIS_VISIBILIDADE");

                entity.Property(e => e.Idperfilvisibilidade).HasColumnName("IDPERFILVISIBILIDADE");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("DESCRICAO")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<PerfisvisisbilidadeOrgaos>(entity =>
            {
                entity.HasKey(e => e.Idperfilvisibilidadeorgao)
                    .HasName("PK__PERFISVI__5C3E6F9850FB042B");

                entity.ToTable("PERFISVISISBILIDADE_ORGAOS");

                entity.Property(e => e.Idperfilvisibilidadeorgao).HasColumnName("IDPERFILVISIBILIDADEORGAO");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idorgao)
                    .HasColumnName("IDORGAO")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Idperfilvisibilidade)
                    .HasColumnName("IDPERFILVISIBILIDADE")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Pessoas>(entity =>
            {
                entity.HasKey(e => e.Idparte)
                    .HasName("PK__PESSOAS__4F3250F665F62111");

                entity.ToTable("PESSOAS");

                entity.Property(e => e.Idparte).HasColumnName("IDPARTE");

                entity.Property(e => e.Cpf).HasColumnName("CPF");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idunidade).HasColumnName("IDUNIDADE");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Obsfimvigencia)
                    .IsRequired()
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<TiposRelacao>(entity =>
            {
                entity.HasKey(e => e.Idtiporelacao)
                    .HasName("PK__TIPOS_RE__59AF9B627AF13DF7");

                entity.ToTable("TIPOS_RELACAO");

                entity.Property(e => e.Idtiporelacao).HasColumnName("IDTIPORELACAO");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idtiporelacaopai)
                    .HasColumnName("IDTIPORELACAOPAI")
                    .HasColumnType("decimal");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Obsfimvigencia)
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Restrito)
                    .IsRequired()
                    .HasColumnName("RESTRITO")
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<TiposUnidade>(entity =>
            {
                entity.HasKey(e => e.Idtipounidade)
                    .HasName("PK__TIPOS_UN__61F9A8DF7FB5F314");

                entity.ToTable("TIPOS_UNIDADE");

                entity.Property(e => e.Idtipounidade).HasColumnName("IDTIPOUNIDADE");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Obsfimvigencia)
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TiposUsuario>(entity =>
            {
                entity.HasKey(e => e.IdtiposUsuario)
                    .HasName("PK__TIPOS_US__C6355274047AA831");

                entity.ToTable("TIPOS_USUARIO");

                entity.Property(e => e.IdtiposUsuario).HasColumnName("IDTIPOS_USUARIO");

                entity.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Transicoes>(entity =>
            {
                entity.HasKey(e => e.Idparte)
                    .HasName("PK__TRANSICO__4F3250F6093F5D4E");

                entity.ToTable("TRANSICOES");

                entity.Property(e => e.Idparte).HasColumnName("IDPARTE");

                entity.Property(e => e.Descricao)
                    .HasColumnName("DESCRICAO")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Empresa).HasColumnName("EMPRESA");

                entity.Property(e => e.Idsep)
                    .IsRequired()
                    .HasColumnName("IDSEP")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Idsiarhes)
                    .IsRequired()
                    .HasColumnName("IDSIARHES")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Subempresa).HasColumnName("SUBEMPRESA");
            });

            modelBuilder.Entity<Unidades>(entity =>
            {
                entity.HasKey(e => e.Idparte)
                    .HasName("PK__UNIDADES__4F3250F60E04126B");

                entity.ToTable("UNIDADES");

                entity.Property(e => e.Idparte).HasColumnName("IDPARTE");

                entity.Property(e => e.Aceitaboletimeletronico).HasColumnName("ACEITABOLETIMELETRONICO");

                entity.Property(e => e.Descricao)
                    .HasColumnName("DESCRICAO")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idorganizacao).HasColumnName("IDORGANIZACAO");

                entity.Property(e => e.Idtipounidade).HasColumnName("IDTIPOUNIDADE");

                entity.Property(e => e.Iniociovigencia)
                    .HasColumnName("INIOCIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Obsfimvigencia)
                    .HasColumnName("OBSFIMVIGENCIA")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Palavrachave)
                    .HasColumnName("PALAVRACHAVE")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .HasColumnName("SIGLA")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Tramitaprocesso)
                    .HasColumnName("TRAMITAPROCESSO")
                    .HasColumnType("char(10)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__USUARIOS__98242AA912C8C788");

                entity.ToTable("USUARIOS");

                entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasColumnType("char(11)");

                entity.Property(e => e.DataBloqueio)
                    .HasColumnName("DATA_BLOQUEIO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idcontatousuario)
                    .IsRequired()
                    .HasColumnName("IDCONTATOUSUARIO")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Idlocal)
                    .HasColumnName("IDLOCAL")
                    .HasColumnType("decimal");

                entity.Property(e => e.Idorgao)
                    .HasColumnName("IDORGAO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Idtiposusuario)
                    .HasColumnName("IDTIPOSUSUARIO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iplogin)
                    .HasColumnName("IPLOGIN")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Login)
                    .HasColumnName("LOGIN")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.QtdTentativaAcesso)
                    .HasColumnName("QTD_TENTATIVA_ACESSO")
                    .HasColumnType("decimal");

                entity.Property(e => e.Senha)
                    .HasColumnName("SENHA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sessionid)
                    .HasColumnName("SESSIONID")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Visibilidade)
                    .HasColumnName("VISIBILIDADE")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<UsuariosPerfisacesso>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__USUARIOS__98242AA9178D7CA5");

                entity.ToTable("USUARIOS_PERFISACESSO");

                entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idperfilacesso)
                    .HasColumnName("IDPERFILACESSO")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Usuariosperfisvisibilidade>(entity =>
            {
                entity.HasKey(e => e.Idusuarioperfilvisibilidade)
                    .HasName("PK__USUARIOS__21E5E0AE1C5231C2");

                entity.ToTable("USUARIOSPERFISVISIBILIDADE");

                entity.Property(e => e.Idusuarioperfilvisibilidade).HasColumnName("IDUSUARIOPERFILVISIBILIDADE");

                entity.Property(e => e.Fimvigencia)
                    .HasColumnName("FIMVIGENCIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idperfilvisibilidade)
                    .HasColumnName("IDPERFILVISIBILIDADE")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("IDUSUARIO")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Iniciovigencia)
                    .HasColumnName("INICIOVIGENCIA")
                    .HasColumnType("datetime");
            });
        }

        public virtual DbSet<Atividades> Atividades { get; set; }
        public virtual DbSet<Contatos> Contatos { get; set; }
        public virtual DbSet<FunionalidadesPerfisacesso> FunionalidadesPerfisacesso { get; set; }
        public virtual DbSet<LogSistema> LogSistema { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<Organizacoes> Organizacoes { get; set; }
        public virtual DbSet<Partes> Partes { get; set; }
        public virtual DbSet<PerfisAcesso> PerfisAcesso { get; set; }
        public virtual DbSet<PerfisVisibilidade> PerfisVisibilidade { get; set; }
        public virtual DbSet<PerfisvisisbilidadeOrgaos> PerfisvisisbilidadeOrgaos { get; set; }
        public virtual DbSet<Pessoas> Pessoas { get; set; }
        public virtual DbSet<TiposRelacao> TiposRelacao { get; set; }
        public virtual DbSet<TiposUnidade> TiposUnidade { get; set; }
        public virtual DbSet<TiposUsuario> TiposUsuario { get; set; }
        public virtual DbSet<Transicoes> Transicoes { get; set; }
        public virtual DbSet<Unidades> Unidades { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosPerfisacesso> UsuariosPerfisacesso { get; set; }
        public virtual DbSet<Usuariosperfisvisibilidade> Usuariosperfisvisibilidade { get; set; }

    }
}