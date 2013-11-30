using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Models
{
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
    }
}
