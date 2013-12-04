using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Threading;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperMarket.Models;

namespace SuperMarket.Controllers
{
    public class ProductController : Controller
    {

        private MarketContext db = new MarketContext();
  
        //
        // GET: /Product/

        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.TipoProducto).Include(p => p.Sucursal).Include(p => p.Suplidor).ToList();
            return View(productos);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.TipoProductoID = new SelectList(db.TiposProductos, "TipoProductoId", "Tipo");
            ViewBag.SucursalId = new SelectList(db.Sucursales, "SucursalId", "Nombre");
            ViewBag.SuplidorId = new SelectList(db.Suplidores, "SuplidorId", "Nombre");
            ViewBag.MarcaProductoId = new SelectList(db.MarcaProductos, "MarcaProductoId", "Nombre");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Producto producto)
        {
            if (ModelState.IsValid)
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images"), fileName);

                //Save File in Folder
                file.SaveAs(path);

                producto.Foto = "/Images/" + fileName;
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoProductoID = new SelectList(db.TiposProductos, "TipoProductoId", "Tipo", producto.TipoProductoID);
            ViewBag.SucursalId = new SelectList(db.Sucursales, "SucursalId", "Nombre", producto.SucursalId);
            ViewBag.SuplidorId = new SelectList(db.Suplidores, "SuplidorId", "Nombre", producto.SuplidorId);
            ViewBag.MarcaProductoId = new SelectList(db.MarcaProductos, "MarcaProductoId", "Nombre");
            return View(producto);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoProductoID = new SelectList(db.TiposProductos, "TipoProductoId", "Tipo", producto.TipoProductoID);
            ViewBag.SucursalId = new SelectList(db.Sucursales, "SucursalId", "Nombre", producto.SucursalId);
            ViewBag.SuplidorId = new SelectList(db.Suplidores, "SuplidorId", "Nombre", producto.SuplidorId);
            ViewBag.MarcaProductoId = new SelectList(db.MarcaProductos, "MarcaProductoId", "Nombre", producto.MarcaProductoId);
            return View(producto);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);
                    producto.Foto = "/Images/" + fileName;
                }
                
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoProductoID = new SelectList(db.TiposProductos, "TipoProductoId", "Tipo", producto.TipoProductoID);
            ViewBag.SucursalId = new SelectList(db.Sucursales, "SucursalId", "Nombre", producto.SucursalId);
            ViewBag.SuplidorId = new SelectList(db.Suplidores, "SuplidorId", "Nombre", producto.SuplidorId);
            ViewBag.MarcaProductoId = new SelectList(db.MarcaProductos, "MarcaProductoId", "Nombre");
            return View(producto);
        }

        [HttpPost]
        public PartialViewResult MostrarResultados(int id)
        {
            Thread.Sleep(2000);
            var productos = db.Productos.Where(p => p.SucursalId == id).ToList();
            return PartialView(productos);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Suplidores(int productId = 0)
        {
            List<Producto> products = db.Productos.Where(p => p.Id == productId).ToList();
            return View(products);
        }

        public ActionResult Sucursales(int sucursalId = 0)
        {
            ViewBag.actualSucursal = db.Sucursales.Where(s => s.SucursalId == sucursalId).FirstOrDefault();

            List<TipoProducto> tiposProductos = db.TiposProductos.Where(t => t.SucursalId == sucursalId).ToList();
            return View(tiposProductos);
        }

        public ActionResult Inventario()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Inventario(int sucursalId, int tipoProductoId, int cantidad, int sucursal2Id)
        {
            List<Producto> productos = db.Productos.Where(p => p.TipoProductoID == tipoProductoId && p.SucursalId == sucursalId).Take(cantidad).ToList();

            foreach (var product in productos)
            {
                product.SucursalId = sucursal2Id;
                db.Entry(product).State = EntityState.Modified;
            }

            return View();
        }

        public JsonResult FindProductosBySucursalId(int id)
        {
            var tiposProductos = db.TiposProductos.Where(t => t.SucursalId == id).ToList();

            return new JsonResult() { 
                Data = tiposProductos,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult FindCantidadProductos(int idSucursal, int tipoProductoId)
        {
            var cantidadProductos = db.Productos.Where(p => p.SucursalId == idSucursal && p.TipoProductoID == 
                tipoProductoId).Count();

            return new JsonResult() { 
                Data = cantidadProductos,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}