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
using ViveroMVC02.ViewModels.Categoria;

namespace ViveroMVC02.Controllers
{
    public class CategoriasController : Controller
    {


        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<CategoriaListViewModels> _listador;

        public CategoriasController()
        {
            _dbContext = new ViveroDbContext();
        }

        // GET: Categorias
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Categorias.Count();

            var categorias = _dbContext.Categorias
                .OrderBy(p => p.NombreCategoria)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();
            var categoriasVm = Mapper
                .Map<List<Categoria>, List<CategoriaListViewModels>>(categorias);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<CategoriaListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = categoriasVm
            };


            return View(_listador);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = _dbContext.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CategoriaEditViewModels categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }

            var categoria = Mapper.Map<CategoriaEditViewModels, Categoria>(categoriaVm);

            if (!_dbContext.Categorias.Any(ct => ct.NombreCategoria == categoriaVm.NombreCategoria))
            {
                _dbContext.Categorias.Add(categoria);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(categoriaVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categoria = _dbContext.Categorias
                .SingleOrDefault(ct => ct.CategoriaId == id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            var categoriaVm = Mapper.Map<Categoria, CategoriaListViewModels>(categoria);
            return View(categoriaVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var categoria = _dbContext.Categorias
                .SingleOrDefault(ct => ct.CategoriaId == id);
            try
            {
                _dbContext.Categorias.Remove(categoria);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var categoriaVm = Mapper
                    .Map<Categoria, CategoriaListViewModels>(categoria);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(categoriaVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categoria = _dbContext.Categorias
                .SingleOrDefault(ct => ct.CategoriaId == id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            CategoriaEditViewModels categoriaVm = Mapper.Map<Categoria, CategoriaEditViewModels>(categoria);
            return View(categoriaVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(CategoriaEditViewModels categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }

            var categoria = Mapper.Map<CategoriaEditViewModels, Categoria>(categoriaVm);
            try
            {
                if (_dbContext.Categorias.Any(ct => ct.NombreCategoria == categoria.NombreCategoria
                                                    && ct.CategoriaId != categoria.CategoriaId))
                {
                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(categoriaVm);
                }

                _dbContext.Entry(categoria).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(categoriaVm);
            }
        }






    }
}