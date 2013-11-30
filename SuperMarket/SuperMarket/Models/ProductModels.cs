using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarket.Models
{
    public class ProductModels
    {
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
}