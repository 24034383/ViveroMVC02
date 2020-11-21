using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViveroMVC02.Context;
using ViveroMVC02.Models;
using ViveroMVC02.ViewModels;
using ViveroMVC02.ViewModels.Localidad;

namespace ViveroMVC02.Controllers
{
    public class LocalidadesController:Controller
    {


        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<LocalidadListViewModels> _listador;

        public LocalidadesController()
        {
            _dbContext = new ViveroDbContext();
        }

        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Localidades.Count();

            var localidades = _dbContext.Localidades
                .Include(c => c.Provincia)
                .OrderBy(c => c.Provincia.NombreProvincia)
                .ThenBy(c => c.NombreLocalidad)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();
            var localidadVm = Mapper
                .Map<List<Localidad>, List<LocalidadListViewModels>>(localidades);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<LocalidadListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = localidadVm
            };


            return View(_listador);
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = _dbContext.Localidades.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

      
        public ActionResult Create()
        {
            var LocalidadVm = new LocalidadEditViewModels
            {
                Provincias = _dbContext.Provincias
                    .OrderBy(p => p.NombreProvincia).ToList()
            };
            return View(LocalidadVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(LocalidadEditViewModels localidadVm)
        {
            if (!ModelState.IsValid)
            {
                localidadVm.Provincias = _dbContext.Provincias
                    .OrderBy(p => p.NombreProvincia).ToList();

                return View(localidadVm);
            }

            var localidad = Mapper.Map<LocalidadEditViewModels, Localidad>(localidadVm);

            if (!_dbContext.Localidades.Any(c => c.NombreLocalidad == localidadVm.NombreLocalidad
                            && c.ProvinciaId != localidadVm.ProvinciaId))
            {
                _dbContext.Localidades.Add(localidad);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }

            localidadVm.Provincias = _dbContext.Provincias
                    .OrderBy(p => p.NombreProvincia).ToList();

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(localidadVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var localidad = _dbContext.Localidades.SingleOrDefault(t => t.LocalidadId == id);
            if (localidad == null)
            {
                return HttpNotFound();
            }

            var localidadVm = Mapper.Map<Localidad, LocalidadListViewModels>(localidad);
            return View(localidadVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var localidad = _dbContext.Localidades.SingleOrDefault(t => t.LocalidadId == id);
            try
            {
                _dbContext.Localidades.Remove(localidad);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var localidadVm = Mapper.Map<Localidad, LocalidadListViewModels>(localidad);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro, se encuentra asociado con otra tabla.");
                return View(localidadVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var localidad = _dbContext.Localidades.SingleOrDefault(t => t.LocalidadId == id);
            if (localidad == null)
            {
                return HttpNotFound();
            }

            LocalidadEditViewModels localidadVm = Mapper
                .Map<Localidad, LocalidadEditViewModels>(localidad);
            localidadVm.Provincias = _dbContext.Provincias
                .OrderBy(p => p.NombreProvincia).ToList();
            return View(localidadVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(LocalidadEditViewModels localidadVm)
        {
            if (!ModelState.IsValid)
            {
                localidadVm.Provincias = _dbContext.Provincias
                    .OrderBy(p => p.NombreProvincia).ToList();

                return View(localidadVm);
            }

            var localidad = Mapper.Map<LocalidadEditViewModels, Localidad>(localidadVm);
            try
            {
                if (_dbContext.Localidades.Any(c => c.NombreLocalidad == localidadVm.NombreLocalidad
                                                  && c.ProvinciaId != localidadVm.ProvinciaId))
                {
                    localidadVm.Provincias = _dbContext.Provincias
                        .OrderBy(p => p.NombreProvincia).ToList();

                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(localidadVm);
                }

                _dbContext.Entry(localidad).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                localidadVm.Provincias = _dbContext.Provincias
                    .OrderBy(p => p.NombreProvincia).ToList();

                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(localidadVm);
            }
        }









    }
}