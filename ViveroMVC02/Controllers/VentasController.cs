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
using ViveroMVC02.ViewModels.Venta;

namespace ViveroMVC02.Controllers
{
    public class VentasController:Controller
    {

        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<VentaListViewModels> _listador;

        public VentasController()
        {
            _dbContext = new ViveroDbContext();
        }

        // GET: Clientes
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Ventas.Count();

            var ventas = _dbContext.Ventas
                .Include(c => c.Cliente)
                .OrderBy(p => p.VentaId)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();

            var ventaVm = Mapper
                .Map<List<Venta>, List<VentaListViewModels>>(ventas);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<VentaListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = ventaVm
            };


            return View(_listador);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = _dbContext.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        public ActionResult Create()
        {
            var ventaVm = new VentaEditViewModels
            {
                Cliente = CombosHelpers.GetClientes(),


            };
            return View(ventaVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(VentaEditViewModels ventaVm)
        {
            if (!ModelState.IsValid)
            {
                ventaVm.Cliente = CombosHelpers.GetClientes();

                return View(ventaVm);
            }

            var venta = Mapper.Map<VentaEditViewModels, Venta>(ventaVm);

            if (!_dbContext.Ventas.Any(ct => ct.VentaId == venta.VentaId ||
                                               ct.Fecha == venta.Fecha))
            {

                _dbContext.Ventas.Add(venta);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }
            ventaVm.Cliente = CombosHelpers.GetClientes();

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(ventaVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var venta = _dbContext.Ventas
                .Include(c => c.Cliente)
                .SingleOrDefault(ct => ct.VentaId == id);
            if (venta == null)
            {
                return HttpNotFound();
            }

            var ventaVm = Mapper.Map<Venta, VentaListViewModels>(venta);
            return View(ventaVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var venta = _dbContext.Ventas
                .SingleOrDefault(ct => ct.VentaId == id);
            try
            {
                _dbContext.Ventas.Remove(venta);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                venta = _dbContext.Ventas
                    .Include(c => c.Cliente)
                    .SingleOrDefault(ct => ct.VentaId == id);

                var ventaVm = Mapper
                    .Map<Venta, VentaListViewModels>(venta);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(ventaVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var venta = _dbContext.Ventas
                .SingleOrDefault(ct => ct.VentaId == id);
            if (venta == null)
            {
                return HttpNotFound();
            }

            VentaEditViewModels ventaVm = Mapper
                .Map<Venta, VentaEditViewModels>(venta);
            ventaVm.Cliente = CombosHelpers.GetClientes();


            return View(ventaVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(VentaEditViewModels ventaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(ventaVm);
            }

            var venta = Mapper.Map<VentaEditViewModels, Venta>(ventaVm);
            ventaVm.Cliente = CombosHelpers.GetClientes();


            try
            {
                if (_dbContext.Ventas.Any(ct => ct.Fecha == venta.Fecha
                                                    && ct.VentaId != venta.VentaId))
                {
                    ventaVm.Cliente = CombosHelpers.GetClientes();

                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(ventaVm);
                }
                //TODO:Ver si existe como usuario, caso contrario darlo de alta
                //TODO:Ver si cambió el mail=>cambiar en la tabla de users.
                _dbContext.Entry(venta).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ventaVm.Cliente = CombosHelpers.GetClientes();
                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(ventaVm);
            }
        }


    }
}