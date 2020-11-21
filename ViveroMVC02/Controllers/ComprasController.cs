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
using ViveroMVC02.ViewModels.Compra;

namespace ViveroMVC02.Controllers
{
    public class ComprasController:Controller
    {

        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<CompraListViewModels> _listador;

        public ComprasController()
        {
            _dbContext = new ViveroDbContext();
        }

        // GET: Clientes
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Compras.Count();

            var compras = _dbContext.Compras
                .Include(c => c.Proveedor)
                .OrderBy(p => p.CompraId)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();

            var comprasVm = Mapper
                .Map<List<Compra>, List<CompraListViewModels>>(compras);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<CompraListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = comprasVm
            };


            return View(_listador);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compras = _dbContext.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        public ActionResult Create()
        {
            var comprasVm = new CompraEdiViewModels
            {
                Proveedor = CombosHelpers.GetProveedores(),
               

            };
            return View(comprasVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CompraEdiViewModels compraVm)
        {
            if (!ModelState.IsValid)
            {
                compraVm.Proveedor = CombosHelpers.GetProveedores();
               
                return View(compraVm);
            }

            var compra = Mapper.Map<CompraEdiViewModels, Compra>(compraVm);

            if (!_dbContext.Compras.Any(ct => ct.CompraId == compra.CompraId ||
                                               ct.Fecha == compra.Fecha))
            {

                _dbContext.Compras.Add(compra);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }
            compraVm.Proveedor = CombosHelpers.GetProveedores();

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(compraVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var compra = _dbContext.Compras
                .Include(c => c.Proveedor)
                .SingleOrDefault(ct => ct.CompraId == id);
            if (compra == null)
            {
                return HttpNotFound();
            }

            var compraVm = Mapper.Map<Compra, CompraListViewModels>(compra);
            return View(compraVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var compra = _dbContext.Compras
                .SingleOrDefault(ct => ct.CompraId == id);  
            try
            {
                _dbContext.Compras.Remove(compra);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                compra = _dbContext.Compras
                    .Include(c => c.Proveedor)
                    .SingleOrDefault(ct => ct.CompraId == id);

                var compraVm = Mapper
                    .Map<Compra, CompraListViewModels>(compra);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(compraVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var compra = _dbContext.Compras
                .SingleOrDefault(ct => ct.CompraId == id);
            if (compra == null)
            {
                return HttpNotFound();
            }

            CompraEdiViewModels compraVm = Mapper
                .Map<Compra, CompraEdiViewModels>(compra);
            compraVm.Proveedor = CombosHelpers.GetProveedores();
           

            return View(compraVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(CompraEdiViewModels compraVm)
        {
            if (!ModelState.IsValid)
            {
                return View(compraVm);
            }

            var compra = Mapper.Map<CompraEdiViewModels, Compra>(compraVm);
            compraVm.Proveedor = CombosHelpers.GetProveedores();
            

            try
            {
                if (_dbContext.Compras.Any(ct => ct.Fecha == compra.Fecha
                                                    && ct.CompraId != compra.CompraId))
                {
                    compraVm.Proveedor = CombosHelpers.GetProveedores();
                   
                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(compraVm);
                }
                //TODO:Ver si existe como usuario, caso contrario darlo de alta
                //TODO:Ver si cambió el mail=>cambiar en la tabla de users.
                _dbContext.Entry(compra).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                compraVm.Proveedor = CombosHelpers.GetProveedores();
                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(compraVm);
            }
        }

        




    }
}