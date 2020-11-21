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
using ViveroMVC02.ViewModels.Cliente;

namespace ViveroMVC02.Controllers
{
    public class ClientesController:Controller
    {


        private readonly ViveroDbContext _dbContext;
        private readonly int _registrosPorPagina = 10;
        private Listador<ClienteListViewModels> _listador;

        public ClientesController()
        {
            _dbContext = new ViveroDbContext();
        }

        // GET: Clientes
        public ActionResult Index(int pagina = 1)
        {
            int totalRegistros = _dbContext.Clientes.Count();

            var clientes = _dbContext.Clientes
                .Include(c => c.Provincia)
                .Include(c => c.Localidad)
                .OrderBy(c => c.Nombre)
                .Skip((pagina - 1) * _registrosPorPagina)
                .Take(_registrosPorPagina)
                .ToList();

            var clientesVm = Mapper
                .Map<List<Cliente>, List<ClienteListViewModels>>(clientes);
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);
            _listador = new Listador<ClienteListViewModels>()
            {
                RegistrosPorPagina = _registrosPorPagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                Registros = clientesVm
            };


            return View(_listador);
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _dbContext.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult Create()
        {
            var clienteVm = new ClienteEditViewModels
            {
                Provincias = CombosHelpers.GetProvincia(),
                Localidades = CombosHelpers.GetLocalidades(),
              
            };
            return View(clienteVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ClienteEditViewModels clienteVm)
        {
            if (!ModelState.IsValid)
            {
                clienteVm.Provincias = CombosHelpers.GetProvincia();
                clienteVm.Localidades = CombosHelpers.GetLocalidades(clienteVm.ProvinciaId);
                return View(clienteVm);
            }

            var cliente = Mapper.Map<ClienteEditViewModels, Cliente>(clienteVm);

            if (!_dbContext.Clientes.Any(ct => ct.Nombre == cliente.Nombre ||
                                               ct.CorreoElectronico == cliente.CorreoElectronico))
            {
              
                _dbContext.Clientes.Add(cliente);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro agregado";

                return RedirectToAction("Index");

            }
            clienteVm.Provincias = CombosHelpers.GetProvincia();
            clienteVm.Localidades = CombosHelpers.GetLocalidades(clienteVm.ProvinciaId);

            ModelState.AddModelError(string.Empty, "Registro repetido...");
            return View(clienteVm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cliente = _dbContext.Clientes
                .Include(c => c.Provincia)
                .Include(c => c.Localidad)
                .SingleOrDefault(ct => ct.ClienteId == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            var clienteVm = Mapper.Map<Cliente, ClienteListViewModels>(cliente);
            return View(clienteVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var cliente = _dbContext.Clientes
                .SingleOrDefault(ct => ct.ClienteId == id);
            try
            {
                _dbContext.Clientes.Remove(cliente);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro eliminado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                cliente = _dbContext.Clientes
                    .Include(c => c.Provincia)
                    .Include(c => c.Localidad)
                    .SingleOrDefault(ct => ct.ClienteId == id);

                var clienteVm = Mapper
                    .Map<Cliente, ClienteListViewModels>(cliente);

                ModelState.AddModelError(string.Empty, "Error al intentar dar de baja un registro");
                return View(clienteVm);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cliente = _dbContext.Clientes
                .SingleOrDefault(ct => ct.ClienteId == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ClienteEditViewModels clienteVm = Mapper
                .Map<Cliente, ClienteEditViewModels>(cliente);
            clienteVm.Provincias = CombosHelpers.GetProvincia();
            clienteVm.Localidades = CombosHelpers.GetLocalidades(clienteVm.ProvinciaId);

            return View(clienteVm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ClienteEditViewModels clienteVm)
        {
            if (!ModelState.IsValid)
            {
                return View(clienteVm);
            }

            var cliente = Mapper.Map<ClienteEditViewModels, Cliente>(clienteVm);
            clienteVm.Provincias = CombosHelpers.GetProvincia();
            clienteVm.Localidades = CombosHelpers.GetLocalidades(clienteVm.ProvinciaId);
            try
            {
                if (_dbContext.Clientes.Any(ct => ct.Nombre == cliente.Nombre
                                                    && ct.ClienteId != cliente.ClienteId))
                {
                    clienteVm.Provincias = CombosHelpers.GetProvincia();
                    clienteVm.Localidades = CombosHelpers.GetLocalidades(clienteVm.ProvinciaId);
                    ModelState.AddModelError(string.Empty, "Registro repetido");
                    return View(clienteVm);
                }
                //TODO:Ver si existe como usuario, caso contrario darlo de alta
                //TODO:Ver si cambió el mail=>cambiar en la tabla de users.
                _dbContext.Entry(cliente).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Msg"] = "Registro editado";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                clienteVm.Provincias = CombosHelpers.GetProvincia();
                clienteVm.Localidades = CombosHelpers.GetLocalidades(clienteVm.ProvinciaId);
                //clienteVm.TiposDeDocumentos = CombosHelpers.GetTipoDocumento();

                ModelState.AddModelError(string.Empty, "Error inesperado al intentar editar un registro");
                return View(clienteVm);
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