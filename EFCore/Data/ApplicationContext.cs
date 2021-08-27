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
