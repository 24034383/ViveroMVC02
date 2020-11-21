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
using ViveroMVC02.ViewModels.Empleado;

namespace ViveroMVC02.Controllers
{
    public class EmpleadosController:Controller
    {

        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<EmpleadoListViewModels> _listador;

        public EmpleadosController()
        {
            _dbContext = new ViveroDbContext();
        }

       
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Empleados.Count();

            var empleados = _dbContext.Empleados
                .Include(c => c.Provincia)
                .Include(c => c.Localidad)
                .OrderBy(c => c.Nombre)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();

            var empleadoVm = Mapper
                .Map<List<Empleado>, List<EmpleadoListViewModels>>(empleados);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<EmpleadoListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = empleadoVm
            };


            return View(_listador);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = _dbContext.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        public ActionResult Create()
        {
            var empleadoVm = new EmpleadoEditViewModels
            {
                Provincias = CombosHelpers.GetProvincia(),
                Localidades = CombosHelpers.GetLocalidades(),
            };
            return View(empleadoVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(EmpleadoEditViewModels empleadoVm)
        {
            if (!ModelState.IsValid)
            {
                empleadoVm.Provincias = CombosHelpers.GetProvincia();
                empleadoVm.Localidades = CombosHelpers.GetLocalidades(empleadoVm.ProvinciaId);
                return View(empleadoVm);
            }

            var empleado = Mapper.Map<EmpleadoEditViewModels, Empleado>(empleadoVm);

            if (!_dbContext.Empleados.Any(ct => ct.Nombre == empleado.Nombre ||
                                               ct.CorreoElectronico == empleado.CorreoElectronico))
            {

                _dbContext.Empleados.Add(empleado);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }
            empleadoVm.Provincias = CombosHelpers.GetProvincia();
            empleadoVm.Localidades = CombosHelpers.GetLocalidades(empleadoVm.ProvinciaId);
            //clienteVm.TiposDeDocumentos = CombosHelpers.GetTipoDocumento();

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(empleadoVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empleado = _dbContext.Empleados
                .Include(c => c.Provincia)
                .Include(c => c.Localidad)
                .SingleOrDefault(ct => ct.EmpleadoId == id);
            if (empleado == null)
            {
                return HttpNotFound();
            }

            var empleadoVm = Mapper.Map<Empleado, EmpleadoListViewModels>(empleado);
            return View(empleadoVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var empleado = _dbContext.Empleados
                .SingleOrDefault(ct => ct.EmpleadoId == id);
            try
            {
                _dbContext.Empleados.Remove(empleado);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                empleado = _dbContext.Empleados
                    .Include(c => c.Provincia)
                    .Include(c => c.Localidad)
                    .SingleOrDefault(ct => ct.EmpleadoId == id);

                var empleadoVm = Mapper
                    .Map<Empleado, EmpleadoListViewModels>(empleado);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(empleadoVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empleado = _dbContext.Empleados
                .SingleOrDefault(ct => ct.EmpleadoId == id);
            if (empleado == null)
            {
                return HttpNotFound();
            }

            EmpleadoEditViewModels empleadoVm = Mapper
                .Map<Empleado, EmpleadoEditViewModels>(empleado);
            empleadoVm.Provincias = CombosHelpers.GetProvincia();
            empleadoVm.Localidades = CombosHelpers.GetLocalidades(empleadoVm.ProvinciaId);
            return View(empleadoVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EmpleadoEditViewModels empleadoVm)
        {
            if (!ModelState.IsValid)
            {
                return View(empleadoVm);
            }

            var empleado = Mapper.Map<EmpleadoEditViewModels, Empleado>(empleadoVm);
            empleadoVm.Provincias = CombosHelpers.GetProvincia();
            empleadoVm.Localidades = CombosHelpers.GetLocalidades(empleadoVm.ProvinciaId);
            try
            {
                if (_dbContext.Empleados.Any(ct => ct.Nombre == empleado.Nombre
                                                    && ct.EmpleadoId != empleado.EmpleadoId))
                {
                   empleadoVm.Provincias = CombosHelpers.GetProvincia();
                   empleadoVm.Localidades = CombosHelpers.GetLocalidades(empleadoVm.ProvinciaId);
                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(empleadoVm);
                }
                _dbContext.Entry(empleado).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                empleadoVm.Provincias = CombosHelpers.GetProvincia();
                empleadoVm.Localidades = CombosHelpers.GetLocalidades(empleadoVm.ProvinciaId);
                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(empleadoVm);
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