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
            return View(producto);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoProductoID = new SelectList(db.TiposProductos, "TipoProductoId", "Tipo", producto.TipoProductoID);
            ViewBag.SucursalId = new SelectList(db.Sucursales, "SucursalId", "Nombre", producto.SucursalId);
            ViewBag.SuplidorId = new SelectList(db.Suplidores, "SuplidorId", "Nombre", producto.SuplidorId);
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
            List<TipoProducto> tiposProductos = db.TiposProductos.Where(t => t.SucursalId == sucursalId).ToList();
            return RedirectToAction("Suplidores", tiposProductos);
        }

    }
}