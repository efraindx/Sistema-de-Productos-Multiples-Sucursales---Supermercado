using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SuperMarket.Models
{
    public class MarketContext : DbContext
    {
        public MarketContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<TipoProducto> TiposProductos { get; set; }
        public DbSet<Suplidor> Suplidores { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<MarcaProducto> MarcaProductos { get; set; }
    }
}