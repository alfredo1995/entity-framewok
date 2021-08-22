using EFCore.Data.Configuration;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFCore.Data
{
	class ApplicationContext : DbContext
	{
		public DbSet<Pedido> Pedidos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore; Integrated Security=true");
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
