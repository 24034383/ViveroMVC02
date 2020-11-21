using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ViveroMVC02.Models;

namespace ViveroMVC02.Context
{
    public class ViveroDbContext:DbContext
    {
        public ViveroDbContext() : base("DefaultConnection")
        {

        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ViveroDbContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Provincia>().ToTable("Provincias");
            modelBuilder.Entity<Localidad>().ToTable("Localidades");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Proveedor>().ToTable("Proveedores");
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Empleado>().ToTable("Empleados");
            modelBuilder.Entity<Venta>().ToTable("Ventas");
            modelBuilder.Entity<Compra>().ToTable("Compras");
            //modelBuilder.Entity<Empleado>().ToTable("Kardex");
            //modelBuilder.Entity<Venta>().ToTable("ItemVentas");
            //modelBuilder.Entity<Compra>().ToTable("ItemCompras");

        }

        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Empleado> Empleados{ get; set; }

        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Compra> Compras { get; set; }

        //public DbSet<ItemVenta> ItemVentas { get; set; }

        //public DbSet<ItemCompra> ItemCompras { get; set; }
        //public DbSet<Kardex> Kardices { get; set; }




    }
}