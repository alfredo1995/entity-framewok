Crianção de uma API de Pedidos no Console
	
	Criar uma Brank Solution >  dentro da solution criar um novo projeto CONSOLE 

Instalando Pacote do SQL Server
	
	dotnet add  Curso\CursoEFCore.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.5

instalando Pacote do SQL Server via Pack Nuget 
	
	Depedencia do proejto >  botão direito > Manage Nuget Package

criar a pasta DOMAIN e ValuesObjects > dentro do projeto Console CursoEFCore
	
	Na pasta CursoEFCore > ValuesObjects  > Criar as class Enum com as propriedades

CursoEFCore > ValuesObjects  >  StatusPedidos.cs
        
	namespace EFCore.Obejct
	{
	    public enum StatusPedido 
	    {
		Analise,
		Fizalizado,
		Entregue,
	    } 
	}

CursoEFCore > ValuesObjects  >  TipoFrete.cs
	
	using System;
	using System.Collections.Generic;
	using System.Text;

	namespace EFCore.Obejct
	{
	    namespace CursoEFCore.ValuesObject
	    {
		public enum TipoFrete
		{
		    CIF,
		    FOB,
		    SemFrete,
		}
	    }
	}


CursoEFCore > ValuesObjects  >  TipoProduto.cs
	
	namespace EFCore.Obejct
	{
	    namespace CursoEFCore.ValuesObject
	    {
		public enum TipoProduto
		{
		    MecadoriaParaRevenda,
		    Embalagem,
		    Serviço,
		}
	    }
	}


CursoEFCore > Domain>  Cliente.cs

	namespace CursoEFCore.Domain
	{
	    public class Cliente
	    {
		public int Id { get; set; }
		public string CodigoBarras { get; set; }
		public string Descricao { get; set; }
		public decimal Valor { get; set; }
		public TipoProduto TipoProduto { get; set; }
		public bool Ativo { get; set; }
	    }
	}


CursoEFCore > Domain>  Produto.cs

	namespace CursoEFCore.Domain
	{
	    public class Produto
	    {
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Telefone { get; set; }
		public string CEP { get; set; }
		public string Estado { get; set; }
		public string cidade { get; set; }
	    }
	}


CursoEFCore > Domain>  Pedido.cs
	
	namespace CursoEFCore.Domain
	{
	    public class Pedido
	    {
		public int Id { get; set; }
		public int ClienteId { get; set; }
		public Cliente Cliente { get; set; }
		public DateTime IniciadoEm { get; set; }
		public DateTime FizalizadoEm { get; set; }
		public TipoFrete TipoFrete { get; set; }
		public StatusPedido Status { get; set; }
		public string Observacao { get; set; }
		public ICollection<PedidoItem> Intes { get; set; }
	    }
	}


CursoEFCore > Domain>  PedidoItem.cs

	namespace CursoEFCore.Domain
	{
	    public class PedidoItem
	    {
		public int Id { get; set; }
		public int PedidoId { get; set; }
		public Pedido Pedido { get; set; }
		public Produto Produto { get; set; }
		public int Quatidade { get; set; }
		public decimal Desconto { get; set; }
	    }
	}

Cria class de contexto DbContext

	criar dentro de CursoEFCore > pasta DATA

CursoEFCore > Data> ApplicationContext.cs : herdando de DbContext

	namespace CursoEFCore.Data
	{
	    class ApplicationContext : DbContext
	    {
		public DbSet<Pedido> Pedidos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		    optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalag=CursoEFCore;Integrated Secury=true");
		}

		// protected override void OnModelCreating(MoldelBuilder modelBuilder) { modelBuilder.Entity<Pedido>();}
	    }
	}
	
CursoEFCore > Data> ApplicationContext.cs sobreescrevendo os metodos
	
	 
	namespace CursoEFCore.Data
	{
	    class ApplicationContext : DbContext
	    {
		public DbSet<Pedido> Pedidos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		    optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalag=CursoEFCore;Integrated Secury=true");
		}
	    }
	}

Mapeamento atraves Fluent API
	
	using CursoEFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Text;

	namespace CursoEFCore.Data
	{
		class ApplicationContext : DbContext
		{
		public DbSet<Pedido> Pedidos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore;Integrated Security=true");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Cliente>(p =>
			{
				p.ToTable("Cliente");
				p.HasKey(p => p.Id);
				p.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
				p.Property(p => p.Telefone).HasColumnType("CHAR(11)").IsRequired();
				p.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
				p.Property(p => p.Estado).HasColumnType("CHAR(8)").IsRequired();
				p.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

				//p.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
			});

			modelBuilder.Entity<Produto>(p =>
			{
				p.ToTable("Produto");
				p.HasKey(p => p.Id);
				p.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(80)").IsRequired();
				p.Property(p => p.Descricao).HasColumnType("CHAR(11)").IsRequired();
				p.Property(p => p.Valor).IsRequired();
			});

			modelBuilder.Entity<Pedido>(p =>
			{
				p.ToTable("Pedido   ");
				p.HasKey(p => p.Id);
				p.Property(p => p.IniciadoEm).HasDefaultValueSql("GETDATA(80)").IsRequired();
				p.Property(p => p.Status).HasConversion<string>();
				p.Property(p => p.TipoFrete).HasConversion<int>();
				p.Property(p => p.Observacao).HasColumnType("CHAR(502)").IsRequired();

				//	p.HasMany(p => p.Intens;
				//   .WithOne(p => p.Pedido)
				//.OnDelete(DeleteBehavior.Cascade);
				//});

				modelBuilder.Entity<PedidoItem>(p =>
				{
					p.ToTable("PedidoIntem");
					p.HasKey(p => p.Id);
					p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
					p.Property(p => p.Valor).IsRequired();
					p.Property(p => p.Desconto).IsRequired();
				});
			});	}}}


Mapeamendo modelo de dados em arquivos separados
	
	CursoEFCore > Data > criar a pasta Configurations > criar o arquivo > ClientConfiguration.cs
	
	using System;
	using System.Collections.Generic;
	using System.Text;

	namespace CursoEFCore.Data.Configuration
	{
	    public class ClienteConfiguration
	    {

	    }
	}

implementar uma class IEntityType informando a entidade ClientConfiguration.cs

	
	using CursoEFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Text;

	namespace CursoEFCore.Data.Configuration
	{
	    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
	    {

	    }
	}

implementando a interface 

	using CursoEFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using System;
	using System.Collections.Generic;
	using System.Text;

	namespace CursoEFCore.Data.Configuration
	{
	    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
	    {
		public void Configure(EntityTypeBuilder<Cliente> builder)
		{
		    throw new NotImplementedException();
		}
	    }
	}	
	
reaproventado a propriedade da entidade cliente

	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using System;
	using System.Collections.Generic;
	using System.Text;

	namespace CursoEFCore.Data.Configuration
	{
	    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
	    {
		public void Configure(EntityTypeBuilder<Cliente> builder)
		{
		    builder.ToTable("Cliente");
		    builder.HasKey(p => p.Id);
		    builder.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
		    builder.Property(p => p.Telefone).HasColumnType("CHAR(11)").IsRequired();
		    builder.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
		    builder.Property(p => p.Estado).HasColumnType("CHAR(8)").IsRequired();
		    builder.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

		    //builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
		}
	    }

reaproventado a propriedade da entidade Produto

	using CursoEFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using System;
	using System.Collections.Generic;
	using System.Text;

	namespace CursoEFCore.Data.Configuration
	{
	    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
	    {
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
		    builder.ToTable("Produto");
		    builder.HasKey(p => p.Id);
		    builder.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(80)").IsRequired();
		    builder.Property(p => p.Descricao).HasColumnType("CHAR(11)").IsRequired();
		    builder.Property(p => p.Valor).IsRequired();

		    //builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
		}
	    }
	}

ApplicationContext

	using CursoEFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;

	namespace CursoEFCore.Data
	{
	    class ApplicationContext : DbContext
	    {
		public DbSet<Pedido> Pedidos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		    optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore;Integrated Securuty=true");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)

		{
		    modelBuilder.Entity<Cliente>(p =>
		    {
			p.ToTable("Cliente");
			p.HasKey(p => p.Id);
			p.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
			p.Property(p => p.Telefone).HasColumnType("CHAR(11)").IsRequired();
			p.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
			p.Property(p => p.Estado).HasColumnType("CHAR(8)").IsRequired();
			p.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

			//p.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
		    });

		    modelBuilder.Entity<Produto>(p =>
		    {
			p.ToTable("Produto");
			p.HasKey(p => p.Id);
			p.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(80)").IsRequired();
			p.Property(p => p.Descricao).HasColumnType("CHAR(11)").IsRequired();
			p.Property(p => p.Valor).IsRequired();
		    });

		    modelBuilder.Entity<Pedido>(p =>
		    {
			p.ToTable("Pedido  ");
			p.HasKey(p => p.Id);
			p.Property(p => p.IniciadoEm).HasDefaultValueSql("GetDate()").IsRequired();
			p.Property(p => p.Status).HasConversion<string>();
			p.Property(p => p.TipoFrete).HasConversion<int>();
			p.Property(p => p.Observacao).HasColumnType("CHAR(502)").IsRequired();


			modelBuilder.Entity<PedidoItem>(p =>
			{
			    p.ToTable("PedidoIntem");
			    p.HasKey(p => p.Id);
			    p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
			    p.Property(p => p.Valor).IsRequired();
			    p.Property(p => p.Desconto).IsRequired();
			});
		    });
		}
	    }
	}
	
	
apos criar as class representando a entidade ApplicationContext pode apagar as propriedades

	using CursoEFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;

	namespace CursoEFCore.Data
	{
	    class ApplicationContext : DbContext
	    {
		public DbSet<Pedido> Pedidos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		    optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore;Integrated Security=true");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)

		{
		}
	    }
	}
	
aplication configuration	

	using CursoEFCore.Data.Configuration;
	using CursoEFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;

	namespace CursoEFCore.Data
	{
	    class ApplicationContext : DbContext
	    {
		public DbSet<Pedido> Pedidos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		    optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore;Integrated Security=true");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)

		{
			
		    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

		    modelBuilder.ApplyConfiguration(new ClienteConfiguration());
		    modelBuilder.ApplyConfiguration(new PedidoConfiguration());
                    modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
                    modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
		}
	    }
	}
	
	
Utilizando DataAnnotations	Domain  > Cliente.cs > adicior atributos key, required, table e Column ()
	
	using EFCore.Obejct.CursoEFCore.ValuesObject;
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	namespace CursoEFCore.Domain
	{

		[Table("Clientes")]
		public class Cliente
		{
		[key]
		public int Id { get; set; }

		[Required]
		public string CodigoBarras { get; set; }
		public string Descricao { get; set; }

		[Coloumn()]
		public decimal Valor { get; set; }

		public TipoProduto TipoProduto { get; set; }
		public bool Ativo { get; set; }
		public string Nome { get; internal set; }
		public string Telefone { get; internal set; }
		public string CEP { get; internal set; }
		public string Cidade { get; internal set; }
		public string Estado { get; internal set; }
	    }
	}
	
	
Habilitando Migrações
	
	intalar o pacote Microsoft.EntityFrameworkCore.Design e Microsoft.EntityFrameworkCore.Tools

	dotnet tool install --global dotnet-ef --version 3.1.5

Comando no Package Nuget Console

	get-help entityframework
	dotnet ef migrations add teste
	dotnet ef migrations add PrimeiraMigracao -p .\EFCore\EFCore.csproj

	Add-Migration NovaMigration
	Update-Database


gerando script SQL passando .\pasta\solutionProjet
	
	terminal : dotnet ef migrations script -p .\EFCore\EFCore.csproj -o .\EFCore\PrimeiraMigracao.SQL
	
Aplicando a migração no Banco de Dados

	dotnet ef database update -p .\EFCore\EFCore.csproj -v
	dotnet ef migrations add AdicionarEmail -p .\EFCore\EFCore.csproj -v
	
Inserido Registro com Entity Framework Core / ApplicationContext
	
	public DbSet<Cliente> Clientes { get; set; }
	public DbSet<Pedido> Pedidos { get; set; }
	public DbSet<Produto> Produtos { get; set; }
	
Criar metodo e instancia na entidade Programa.cs

	public static void InserirDados(){
	 var produto = new Produto{
		Descricao = "Produto Teste",
		CodigoBarras = "123456789",
		Valor= 10,
		TipoProduto = TipoProduto.MercadoriaParaRevenda,
		Ativo = true	};
		
Criar metodo e instancia na entidade Programa.cs

	using CursoEFCore.Domain;
	using EFCore.Obejct.CursoEFCore.ValuesObject;
	using System;

	namespace EFCore
	{
	    class Program
	    {		
		static void Main(string[] args)
		{
		    InserirDados();
		}

		private static void InserirDados()
		{
		    var produto = new Produto
		    {
			Descricao = "Produto Teste",
			CodigoBarras = "123456789",
			Valor = 10,
			TipoProduto = TipoProduto.MercadoriaParaRevenda,
			Ativo = true

		    };

		    //preparar o objeto para instancia
		    using var db = new Data.ApplicationContext();
		    
		    //4 formas de adicionar instancia do objeto
		    db.Produtos.Add(produto);
		    db.Set<Produto>().Add(produto);
		    db.Entity(produto).State = EntityState.Added;
		    db.Add(produto);
		
		    //salvando o rastreamento que EFCore alocou em memoria
		    var registro = db.SaveChanges();

		    Console.WriteLine($"Total registro");
		}
	    }
	}
	
Inserindo os registro usando terminal 

	dotnet run

adicionar pacote de log

	todas as instruçoes geradas pelo efcore sejam escritas no console da aplicação
	
terminal console gerenciador de pacotes

	dotnet add package Microsoft.Extensions.Logging.Console --Version 3.1.5

AplicationContext informar a instancia do log criado

	class ApplicationContext : DbContext
	{
		private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());
		
AplicationContext metodo OnConfiguring informando o log a ser executado

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseLoggerFactory(_logger)
				.EnableSensitiveDataLogging()
				
Entidade AplicationContext

	using EFCore.Data.Configuration;
	using EFCore.Domain;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;

	namespace EFCore.Data
	{
		class ApplicationContext : DbContext
		{
			private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());

			public DbSet<Pedido> Pedidos { get; set; }
			public DbSet<Pedido> Produtos { get; set; }
			public DbSet<Pedido> Clientes { get; set; }

			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				optionsBuilder
					.UseLoggerFactory(_logger)
					.EnableSensitiveDataLogging()
					.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore; Integrated Security=true");
			}

			protected override void OnModelCreating(ModelBuilder modelBuilder)

			{

				modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

				modelBuilder.ApplyConfiguration(new ClienteConfiguration());
				modelBuilder.ApplyConfiguration(new PedidoConfiguration());
				modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
				modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
			}
		}
	}


inserindo registro em massa ao criar novo metodo na entidade Programa.cs

    public static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "123456789",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true

            };

            var cliente = new Cliente
            {
                Nome = "Alfredo Gomes",
                CEP = "31990055",
                Cidade = "Belo Horizonte",
                Estado = "MG",
                Telefone = "31995358198"
            };

criando instancia do contexto Programa.cs

            using var db = new Data.ApplicationContext();

            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");

        }  
  
alterar o  recebimento no static void main de InserirDados para InserirDadosEmMassa

	static void Main(string[] args)
		{
		    InserirDadosEmMassa();
		}
		
Entidade Programa.cs

	using EFCore.Domain;
	using EFCore.Obejct.EFCore.ValuesObject;
	using System;

	namespace EFCore
	{
	    class Program
	    {
		static void Main(string[] args)
		{
		    InserirDadosEmMassa();
		}
		
		public static void InserirDadosEmMassa()
		{
		    var produto = new Produto
		    {
			Descricao = "Produto Teste",
			CodigoBarras = "123456789",
			TipoProduto = TipoProduto.MercadoriaParaRevenda,
			Ativo = true

		    };

		    var cliente = new Cliente
		    {
			Nome = "Alfredo Gomes",
			CEP = "31990055",
			Cidade = "Belo Horizonte",
			Estado = "MG",
			Telefone = "31995358198"
		    };

		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    db.AddRange(produto, cliente);

		    var registros = db.SaveChanges();
		    Console.WriteLine($"Total Registro(s): {registros}");

		}       


		private static void InserirDados()
		{
		    var produto = new Produto
		    {
			Descricao = "Produto Teste",
			CodigoBarras = "123456789",
			TipoProduto = TipoProduto.MercadoriaParaRevenda,
			Ativo = true

		    };

		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();
		    //db.Produtos.Add(produto);
		    db.Set<Produto>().Add(produto);
		    //db.Entity(produto).State = EntityState.Added;
		    //db.Add(produto);

		    var registro = db.SaveChanges();

		    Console.WriteLine($"Total registro");
		}
	    }
	}
  
  
rodar as alterações no terminal 
  
  	dotnet run
	
consultando registro da base dados program.cs


	public static void ConsultarDados(){
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //consulta utilizando sintaxe
            // var consultaPorSintaxe = (from c in db.Clientes  where c.Id > 0 select c).ToList();

            //consulta utilizando metodo linqs
            var consultaPorSintaxe = db.Clientes.Where(p => p.Id > 0).ToList();
        }   

chamar no metodo ConsultarDados no metodo main

	   static void Main(string[] args)
		{
		    InserirDadosEmMassa();
		    ConsultarDados();
		}


Entidade Program.cs

	using EFCore.Domain;
	using EFCore.Obejct.EFCore.ValuesObject;
	using System;
	using System.Linq;

	namespace EFCore
	{
	    class Program
	    {
		static void Main(string[] args)
		{
		    InserirDadosEmMassa();
		    ConsultarDados();
		}

		public static void ConsultarDados()
		{
		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //consulta utilizando Linq
		    var consultaPorSintaxe = (from c in db.Clientes  where c.Id > 0 select c).ToList();
		    foreach(var Clientes in consultaPorSintaxe)
		    {
			db.Clientes.Find(Clientes.Id);
		    }

		    //consulta utilizando metodo query
		    //var consultaPorSintaxe = db.Clientes.Where(p => p.Id > 0).ToList();
		}   

		public static void InserirDadosEmMassa()
		{
		    var produto = new Produto
		    {
			Descricao = "Produto Teste",
			CodigoBarras = "123456789",
			TipoProduto = TipoProduto.MercadoriaParaRevenda,
			Ativo = true

		    };

		    var cliente = new Cliente
		    {
			Nome = "Alfredo Gomes",
			CEP = "31990055",
			Cidade = "Belo Horizonte",
			Estado = "MG",
			Telefone = "31995358198"
		    };

		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //db.AddRange(produto, cliente); 

		    var registros = db.SaveChanges();
		    Console.WriteLine($"Total Registro(s): {registros}");

		}       


		private static void InserirDados()
		{
		    var produto = new Produto
		    {
			Descricao = "Produto Teste",
			CodigoBarras = "123456789",
			TipoProduto = TipoProduto.MercadoriaParaRevenda,
			Ativo = true

		    };

		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();
		    //db.Produtos.Add(produto);
		    db.Set<Produto>().Add(produto);
		    //db.Entity(produto).State = EntityState.Added;
		    //db.Add(produto);

		    var registro = db.SaveChanges();

		    Console.WriteLine($"Total registro");
		}
	    }
	}
	
terminal 

	dotnet run

consulta com carregamento adiantado

        public static void CadastrarPedido()
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //consulta 
            var Cliente = db.Clientes.FirstOrDefault();
            var Produto = db.Produtos.FirstOrDefault();

            //criando um pedido
            var pedido = new Pedido
            {
                //preechendo a propriedade do pedido
                ClienteId = clienteid,
                IniciadoEm = DateTime.Now,
                FinalizadoEM = DateTime.Now,
                Observacao = "Pedido Test",
                Status = Obejct.StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,               
            };

        }

chamar no metodo CadastrarPedido no metodo main

    static void Main(string[] args)
        {
            InserirDadosEmMassa();
            ConsultarDados();
            CadastrarPedido();
        }

metodo de consulta carregamento adiantado

        private static void ConsultarPedidoCarregamentoAdiantado() 
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //chamando o metodo to list
            var pedidos = db.Pedidos.Include(p=>p.Intes).ToList();

            //retornado todo registro em memoria
            Console.WriteLine(pedidos.Count);
        }	


atualizado regristro no banco de dados

        public static void AtualizarDados()
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //consultando informacoes
            var Cliente = db.Clientes.Find(1);
            //alterar o nome do cliente

            Cliente.Nome = "Cliente alterado Passo 1";
            //instacia do contexto
            db.Clientes.Update(Cliente);
            db.SaveChanges();

        }

chamar no metodo AtualizarDados no metodo main

    static void Main(string[] args)
        {
            InserirDadosEmMassa();
            ConsultarDados();
            CadastrarPedido();
        }

entidade programa.cs

	using EFCore.Domain;
	using EFCore.Obejct.EFCore.ValuesObject;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Linq;

	namespace EFCore
	{
	    class Program
	    {
		private static int clienteid;

		//metodos de execucoes dos metodos criados
		static void Main(string[] args)
		{
		    InserirDadosEmMassa();
		    ConsultarDados();
		    CadastrarPedido();
		    ExcluirDados();
		}


		//metodo p/ consultar registro
		public static void ConsultarDados()
		{
		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //consulta utilizando Linq
		    var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
		    foreach (var Clientes in consultaPorSintaxe)
		    {
			db.Clientes.Find(Clientes.Id);
		    }

		    //consulta utilizando metodo query
		   //var consultaPorSintaxe = db.Clientes.Where(p => p.Id > 0).ToList();
		}


		//metodo p/ inserir registro
		private static void InserirDados()
		{
		    var produto = new Produto
		    {
			Descricao = "Produto Teste",
			CodigoBarras = "123456789",
			TipoProduto = TipoProduto.MercadoriaParaRevenda,
			Ativo = true

		    };

		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();
		    //db.Produtos.Add(produto);
		    db.Set<Produto>().Add(produto);
		    //db.Entity(produto).State = EntityState.Added;
		    //db.Add(produto);

		    var registro = db.SaveChanges();

		    Console.WriteLine($"Total registro");
		}


		//metodo p/ inserir registro em massa
		public static void InserirDadosEmMassa()
		{
		    var produto = new Produto
		    {
			Descricao = "Produto Teste",
			CodigoBarras = "123456789",
			TipoProduto = TipoProduto.MercadoriaParaRevenda,
			Ativo = true

		    };

		    var cliente = new Cliente
		    {
			Nome = "Alfredo Gomes",
			CEP = "31990055",
			Cidade = "Belo Horizonte",
			Estado = "MG",
			Telefone = "31995358198"
		    };

		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //db.AddRange(produto, cliente); 
		    var registros = db.SaveChanges();
		    Console.WriteLine($"Total Registro(s): {registros}");
		}


		//metodo p/ consultar registro adiatado
		private static void ConsultarPedidoCarregamentoAdiantado()
		{
		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //chamando o metodo to list
		    var pedidos = db.Pedidos.Include("Intes").ToList();

		    //retornado todo registro em memoria
		    Console.WriteLine(pedidos.Count);
		}


		//metodo p/ cadastrar registro 
		public static void CadastrarPedido()
		{
		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //consulta 
		    var Cliente = db.Clientes.FirstOrDefault();
		    var Produto = db.Produtos.FirstOrDefault();

		    //criando um pedido
		    var pedido = new Pedido
		    {
			//preechendo a propriedade do pedido
			ClienteId = clienteid,
			IniciadoEm = DateTime.Now,
			FinalizadoEM = DateTime.Now,
			Observacao = "Pedido Test",
			Status = Obejct.StatusPedido.Analise,
			TipoFrete = TipoFrete.SemFrete,
		    };

		}


		//metodo p/ atualizar registro
		public static void AtualizarDados()
		{
		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //consultando informacoes
		    var Cliente = db.Clientes.Find(1);
		    //alterar o nome do cliente

		    Cliente.Nome = "Cliente alterado Passo 1";
		    //instacia do contexto
		    db.Clientes.Update(Cliente);
		  //db.Entry(cliente).State = EntityState.Modified;

		    db.SaveChanges();

		}

		//metodo p/ atualizar registro
		public static void ExcluirDados()
		{
		    //criando instancia do contexto
		    using var db = new Data.ApplicationContext();

		    //consultando informacoes
		    var Cliente = db.Clientes.Find(2);
		    //alterar o nome do cliente

		    //Removendo instacia do contexto
		    db.Clientes.Remove(Cliente);

		    db.SaveChanges();
		}

	    }
	}
	
	
