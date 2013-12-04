using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarket.Models
{
    public class ProductModels
    {
    }

    public class Producto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Foto { get; set; }
        public int TipoProductoID { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public int SuplidorId { get; set; }
        public virtual Suplidor Suplidor { get; set; }
        public int MarcaProductoId { get; set; }
        public virtual MarcaProducto MarcaProducto { get; set; }
    }

    public class TipoProducto
    {
        public int TipoProductoId { get; set; }
        public string Tipo { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }

    public class Suplidor
    {
        public int SuplidorId { get; set; }
        public string Nombre { get; set; }
    }

    public class Sucursal
    {
        public int SucursalId { get; set; }
        public string Nombre { get; set; }
    }

    public class MarcaProducto
    {
        public int MarcaProductoId { get; set; }
        public string Nombre { get; set; }
    }
}