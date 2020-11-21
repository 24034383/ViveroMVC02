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
using ViveroMVC02.ViewModels.Provincia;

namespace ViveroMVC02.Controllers
{
    public class ProvinciasController:Controller
    {

        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<ProvinciaListViewModels> _listador;

        public ProvinciasController()
        {
            _dbContext = new ViveroDbContext();
        }

        // GET: Paises
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Provincias.Count();

            var provincias = _dbContext.Provincias
                .OrderBy(p => p.NombreProvincia)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();
            var ProvinciaVm = Mapper
                .Map<List<Provincia>, List<ProvinciaListViewModels>>(provincias);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<ProvinciaListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = ProvinciaVm
            };


            return View(_listador);
        }

        // GET: Paises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = _dbContext.Provincias.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProvinciaEditViewModels ProvinciaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(ProvinciaVm);
            }

            var provincia = Mapper.Map<ProvinciaEditViewModels, Provincia>(ProvinciaVm);

            if (!_dbContext.Provincias.Any(p => p.NombreProvincia == ProvinciaVm.NombreProvincia))
            {
                _dbContext.Provincias.Add(provincia);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(ProvinciaVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var provincia = _dbContext.Provincias.SingleOrDefault(p => p.ProvinciaId == id);
            if (provincia == null)
            {
                return HttpNotFound();
            }

            var provinciaVm = Mapper.Map<Provincia, ProvinciaListViewModels>(provincia);
            return View(provinciaVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var provincia = _dbContext.Provincias.SingleOrDefault(t => t.ProvinciaId == id);
            try
            {
                _dbContext.Provincias.Remove(provincia);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var provinciaVm = Mapper.Map<Provincia, ProvinciaListViewModels>(provincia);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(provinciaVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var provincia = _dbContext.Provincias.SingleOrDefault(p => p.ProvinciaId == id);
            if (provincia == null)
            {
                return HttpNotFound();
            }

            ProvinciaEditViewModels provinciaVm = Mapper.Map<Provincia, ProvinciaEditViewModels>(provincia);
            return View(provinciaVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ProvinciaEditViewModels provinciaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(provinciaVm);
            }

            var provincia = Mapper.Map<ProvinciaEditViewModels, Provincia>(provinciaVm);
            try
            {
                if (_dbContext.Provincias.Any(p => p.NombreProvincia ==provincia.NombreProvincia
                                               && p.ProvinciaId != provincia.ProvinciaId))
                {
                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(provinciaVm);
                }

                _dbContext.Entry(provincia).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(provinciaVm);
            }
        }



    }
}