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
using ViveroMVC02.ViewModels.Proveedor;

namespace ViveroMVC02.Controllers
{
    public class ProveedoresController:Controller
    {

        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<ProveedorListViewModels> _listador;

        public ProveedoresController()
        {
            _dbContext = new ViveroDbContext();
        }

        // GET
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Proveedores.Count();

            var proveedores = _dbContext.Proveedores
                .Include(c => c.Provincia)
                .Include(c => c.Localidad)
                .OrderBy(p => p.RazonSocial)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();

            var provedorVm = Mapper
                .Map<List<Proveedor>, List<ProveedorListViewModels>>(proveedores);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<ProveedorListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = provedorVm
            };


            return View(_listador);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = _dbContext.Proveedores.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        public ActionResult Create()
        {
            var proveedorVm = new ProveedorEditViewModels
            {
                Provincias = CombosHelpers.GetProvincia(),
                Localidades = CombosHelpers.GetLocalidades(),
               

            };
            return View(proveedorVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProveedorEditViewModels proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Provincias = CombosHelpers.GetProvincia();
                proveedorVm.Localidades = CombosHelpers.GetLocalidades(proveedorVm.ProvinciaId);
                //clienteVm.TiposDeDocumentos = CombosHelpers.GetTipoDocumento();
                return View(proveedorVm);
            }

            var proveedor = Mapper.Map<ProveedorEditViewModels, Proveedor>(proveedorVm);

            if (!_dbContext.Proveedores.Any(ct => ct.RazonSocial == proveedor.RazonSocial ||
                                               ct.CorreoElectronico == proveedor.CorreoElectronico))
            {

                _dbContext.Proveedores.Add(proveedor);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }
            proveedorVm.Provincias = CombosHelpers.GetProvincia();
            proveedorVm.Localidades = CombosHelpers.GetLocalidades(proveedorVm.ProvinciaId);
            //clienteVm.TiposDeDocumentos = CombosHelpers.GetTipoDocumento();

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(proveedorVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var proveedor = _dbContext.Proveedores
                .Include(c => c.Provincia)
                .Include(c => c.Localidad)
                .SingleOrDefault(ct => ct.ProveedorId == id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }

            var proveedorVm = Mapper.Map<Proveedor, ProveedorListViewModels>(proveedor);
            return View(proveedorVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var proveedor = _dbContext.Proveedores
                .SingleOrDefault(ct => ct.ProveedorId == id);
            try
            {
                _dbContext.Proveedores.Remove(proveedor);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                proveedor = _dbContext.Proveedores
                    .Include(c => c.Provincia)
                    .Include(c => c.Localidad)
                    .SingleOrDefault(ct => ct.ProveedorId == id);

                var proveedorVm = Mapper
                    .Map<Proveedor, ProveedorListViewModels>(proveedor);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(proveedorVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var proveedor = _dbContext.Proveedores
                .SingleOrDefault(ct => ct.ProveedorId == id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }

            ProveedorEditViewModels proveedorVm = Mapper
                .Map<Proveedor, ProveedorEditViewModels>(proveedor);
           proveedorVm.Provincias = CombosHelpers.GetProvincia();
           proveedorVm.Localidades = CombosHelpers.GetLocalidades(proveedorVm.ProvinciaId);

            return View(proveedorVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ProveedorEditViewModels proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                return View(proveedorVm);
            }

            var proveedor = Mapper.Map<ProveedorEditViewModels, Proveedor>(proveedorVm);
            proveedorVm.Provincias = CombosHelpers.GetProvincia();
            proveedorVm.Localidades = CombosHelpers.GetLocalidades(proveedorVm.ProvinciaId);
            try
            {
                if (_dbContext.Proveedores.Any(ct => ct.RazonSocial == proveedor.RazonSocial
                                                    && ct.ProveedorId != proveedor.ProveedorId))
                {
                    proveedorVm.Provincias = CombosHelpers.GetProvincia();
                    proveedorVm.Localidades = CombosHelpers.GetLocalidades(proveedorVm.ProvinciaId);
                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(proveedorVm);
                }
                _dbContext.Entry(proveedor).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                proveedorVm.Provincias = CombosHelpers.GetProvincia();
                proveedorVm.Localidades = CombosHelpers.GetLocalidades(proveedorVm.ProvinciaId);

                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(proveedorVm);
            }
        }

        public JsonResult GetLocalidades(int provinciaId)
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;
            var localidades = _dbContext.Localidades.Where(c => c.ProvinciaId == provinciaId).ToList();
            return Json(localidades, JsonRequestBehavior.AllowGet);
        }



    }
}