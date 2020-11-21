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
using ViveroMVC02.ViewModels.ItemCompra;

namespace ViveroMVC02.Controllers
{
    public class ItemComprasController:Controller
    {

        //private readonly ViveroDbContext _dbContext;
        //private readonly int _registrosPorPagina = 10;
        //private Listador<ItemCompraListViewModels> _listador;

        //public ItemComprasController()
        //{
        //    _dbContext = new ViveroDbContext();
        //}

        //// GET: Clientes
        //public ActionResult Index(int pagina = 1)
        //{
        //    int totalRegistros = _dbContext.ItemCompras.Count();

        //    var item = _dbContext.ItemCompras
        //        .Include(c => c.Producto)
        //        .Include(c => c.Compra)
        //        .OrderBy(p => p.ItemCompraId)
        //        .Skip((pagina - 1) * _registrosPorPagina)
        //        .Take(_registrosPorPagina)
        //        .ToList();

        //    var itemVm = Mapper
        //        .Map<List<ItemCompra>, List<ItemCompraListViewModels>>(item);
        //    var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
        //    _listador = new Listador<ItemCompraListViewModels>()
        //    {
        //        RegistrosPorPagina = _registrosPorPagina,
        //        TotalPaginas = totalPaginas,
        //        TotalRegistros = totalRegistros,
        //        PaginaActual = pagina,
        //        Registros = itemVm
        //    };


        //    return View(_listador);
        //}


        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ItemCompra item = _dbContext.ItemCompras.Find(id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(item);
        //}

        //public ActionResult Create()
        //{
        //    var itemVm = new ItemCompraEditViewModels
        //    {
        //        Productos = CombosHelpers.GetProductos(),


        //    };
        //    return View(itemVm);
        //}

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult Create(ItemCompraEditViewModels itemVm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        itemVm.Productos = CombosHelpers.GetProductos();

        //        return View(itemVm);
        //    }

        //    var item = Mapper.Map<ItemCompraEditViewModels, ItemCompra>(itemVm);

        //    if (!_dbContext.ItemCompras.Any(ct => ct.ItemCompraId == item.ItemCompraId ||
        //                                       ct.Producto == item.Producto))
        //    {

        //        _dbContext.ItemCompras.Add(item);
        //        _dbContext.SaveChanges();
        //        TempData["Msg"] = "Registro agregado";

        //        return RedirectToAction("Index");

        //    }
        //    itemVm.Productos = CombosHelpers.GetProductos();

        //    ModelState.AddModelError(string.Empty, "Registro repetido...");
        //    return View(itemVm);

        //}

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var item = _dbContext.ItemCompras
        //        .Include(c => c.Producto)
        //        .SingleOrDefault(ct => ct.ItemCompraId == id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var itemVm = Mapper.Map<ItemCompra, ItemCompraListViewModels>(item);
        //    return View(itemVm);
        //}

        //[ValidateAntiForgeryToken]
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirm(int id)
        //{
        //    var item = _dbContext.ItemCompras
        //        .SingleOrDefault(ct => ct.ItemCompraId == id);
        //    try
        //    {
        //        _dbContext.ItemCompras.Remove(item);
        //        _dbContext.SaveChanges();
        //        TempData["Msg"] = "Registro eliminado";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        item = _dbContext.ItemCompras
        //            .Include(c => c.Producto)
        //            .SingleOrDefault(ct => ct.ItemCompraId == id);

        //        var itemVm = Mapper
        //            .Map<ItemCompra, ItemCompraListViewModels>(item);

        //        ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
        //        return View(itemVm);
        //    }
        //}

        //[HttpGet]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var item = _dbContext.ItemCompras
        //        .SingleOrDefault(ct => ct.ItemCompraId == id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ItemCompraEditViewModels itemVm = Mapper
        //        .Map<ItemCompra, ItemCompraEditViewModels>(item);
        //    itemVm.Productos = CombosHelpers.GetProductos();


        //    return View(itemVm);
        //}

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult Edit(ItemCompraEditViewModels itemVm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(itemVm);
        //    }

        //    var item = Mapper.Map<ItemCompraEditViewModels, ItemCompra>(itemVm);
        //    itemVm.Productos = CombosHelpers.GetProductos();


        //    try
        //    {
        //        if (_dbContext.ItemCompras.Any(ct => ct.Cantidad == item.Cantidad
        //                                            && ct.ItemCompraId != item.ItemCompraId))
        //        {
        //            itemVm.Productos = CombosHelpers.GetProductos();

        //            ModelState.AddModelError(string.Empty, "Registro repetido");
        //            return View(itemVm);
        //        }
        //        //TODO:Ver si existe como usuario, caso contrario darlo de alta
        //        //TODO:Ver si cambió el mail=>cambiar en la tabla de users.
        //        _dbContext.Entry(item).State = EntityState.Modified;
        //        _dbContext.SaveChanges();
        //        TempData["Msg"] = "Registro editado";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        itemVm.Productos = CombosHelpers.GetProductos();
        //        ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
        //        return View(itemVm);
        //    }
        //}







    }
}