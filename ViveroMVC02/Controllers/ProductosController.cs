using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViveroMVC02.Classes;
using ViveroMVC02.Context;
using ViveroMVC02.Models;
using ViveroMVC02.ViewModels;
using ViveroMVC02.ViewModels.Producto;

namespace ViveroMVC02.Controllers
{
    public class ProductosController:Controller
    {
        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<ProductoListViewModels> _listador;

        public ProductosController()
        {
            _dbContext = new ViveroDbContext();
        }

        // GET: Productos
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Productos.Count();

            var productos = _dbContext.Productos
                .Include(c => c.Categoria)
                .Include(m => m.Proveedor)
                .OrderBy(p => p.NombreProducto)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();

            var productosVm = Mapper
                .Map<List<Producto>, List<ProductoListViewModels>>(productos);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<ProductoListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = productosVm
            };


            return View(_listador);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = _dbContext.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        public ActionResult Create()
        {
            var productoVm = new ProductoEditViewModels
            {
                Categorias =CombosHelpers.GetCategorias(),
                Proveedores = CombosHelpers.GetProveedores()
                
            };
            return View(productoVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProductoEditViewModels productoVm)
        {
            if (!ModelState.IsValid)
            {
                productoVm.Categorias = CombosHelpers.GetCategorias();
                productoVm.Proveedores = CombosHelpers.GetProveedores();
               

                return View(productoVm);
            }

            var producto = Mapper.Map<ProductoEditViewModels, Producto>(productoVm);

            if (!_dbContext.Productos.Any(ct => ct.NombreProducto == producto.NombreProducto
                                                && ct.CategoriaId == producto.CategoriaId))
            {
                using (var tran = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _dbContext.Productos.Add(producto);
                        _dbContext.SaveChanges();

                        _dbContext.Entry(producto).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                        tran.Commit();
                        TempData["Msg"] = "Registro agregado";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        productoVm.Categorias = CombosHelpers.GetCategorias();
                        productoVm.Proveedores = CombosHelpers.GetProveedores();

                        ModelState.AddModelError(string.Empty, e.Message);
                        return View(productoVm);
                    }
                }
            }
            productoVm.Categorias = CombosHelpers.GetCategorias();
            productoVm.Proveedores = CombosHelpers.GetProveedores();

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(productoVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _dbContext.Productos
                .Include(c => c.Categoria)
                .SingleOrDefault(ct => ct.ProductoId == id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            var productoVm = Mapper.Map<Producto, ProductoListViewModels>(producto);
            return View(productoVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var producto = _dbContext.Productos
                .SingleOrDefault(ct => ct.ProductoId == id);
            try
            {
                _dbContext.Productos.Remove(producto);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                producto = _dbContext.Productos
                    .Include(c => c.Categoria)
                    .SingleOrDefault(ct => ct.ProductoId == id);

                var productoVm = Mapper
                    .Map<Producto, ProductoListViewModels>(producto);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(productoVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _dbContext.Productos
                .SingleOrDefault(ct => ct.ProductoId == id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            ProductoEditViewModels productoVm = Mapper
                .Map<Producto, ProductoEditViewModels>(producto);
            productoVm.Proveedores = CombosHelpers.GetProveedores();
            productoVm.Categorias = CombosHelpers.GetCategorias();

            return View(productoVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ProductoEditViewModels productoVm)
        {
            if (!ModelState.IsValid)
            {
                return View(productoVm);
            }

            var producto = Mapper.Map<ProductoEditViewModels, Producto>(productoVm);
            productoVm.Categorias = CombosHelpers.GetCategorias();
            productoVm.Proveedores = CombosHelpers.GetProveedores();
            try
            {
                if (_dbContext.Productos.Any(ct => ct.NombreProducto == producto.NombreProducto
                                                    && ct.ProductoId != producto.ProductoId))
                {
                    productoVm.Proveedores = CombosHelpers.GetProveedores();
                    productoVm.Categorias = CombosHelpers.GetCategorias();
                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(productoVm);
                }
                _dbContext.Entry(producto).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
               productoVm.Proveedores = CombosHelpers.GetProveedores();
               productoVm.Categorias = CombosHelpers.GetCategorias();

                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(productoVm);
            }
        }







    }
}