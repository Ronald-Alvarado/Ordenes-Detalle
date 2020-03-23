using Microsoft.EntityFrameworkCore;
using OrdenDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdenDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Productos>().HasData(new Productos { ProductoId = 1, NombreProducto = "Pepino", Precio = 5000});
            modelBuilder.Entity<Productos>().HasData(new Productos { ProductoId = 2, NombreProducto = "Azucar", Precio = 500 });
            modelBuilder.Entity<Productos>().HasData(new Productos { ProductoId = 3, NombreProducto =  "Sal",   Precio = 50  });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Ordenes.db");

        }

    }
}
