using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViveroMVC02.Context;
using ViveroMVC02.Models;

namespace ViveroMVC02.Classes
{
    public class CombosHelpers:IDisposable
    {



        private static readonly ViveroDbContext Db = new ViveroDbContext();

        public static List<Provincia> GetProvincia()
        {
            var defaultProvincia = new Provincia
            {
                ProvinciaId = 0,
                NombreProvincia = "[Seleccione Provincia]"
            };
            var listaProvincia = Db.Provincias.OrderBy(p => p.NombreProvincia).ToList();
            listaProvincia.Insert(0, defaultProvincia);
            return listaProvincia;
        }

        public static List<Localidad> GetLocalidades(int provinciaId = 0)
        {
            var defaultLocalidad = new Localidad
            {
                LocalidadId = 0,
                NombreLocalidad = "[Seleccione Localidad]"
            };
            var ListaLocalidades = Db.Localidades
                .Where(c => c.ProvinciaId == provinciaId)
                .OrderBy(c => c.NombreLocalidad).ToList();
            ListaLocalidades.Insert(0, defaultLocalidad);
            return ListaLocalidades;
        }
        public void Dispose()
        {
            Db.Dispose();
        }

        public static List<Categoria> GetCategorias()
        {
            var defaultCategoria = new Categoria
            {
                CategoriaId = 0,
                NombreCategoria = "[Seleccione Categoría]"
            };
            var listaCategorias = Db.Categorias
                .OrderBy(c => c.NombreCategoria).ToList();
            listaCategorias.Insert(0, defaultCategoria);
            return listaCategorias;
        }

        //public static List<TiposDeDocumentos> GetTipoDocumento()
        //{
        //    var defaultTipoDoc = new TiposDeDocumentos
        //    {
        //        TipoDocumentoId = 0,
        //        Descripcion = "[Seleccione Tipo Documento]"
        //    };
        //    var ListaTipoDoc = Db.TiposDeDocumentos
        //        .OrderBy(c => c.Descripcion).ToList();
        //    ListaTipoDoc.Insert(0, defaultTipoDoc);
        //    return ListaTipoDoc;
        //}






        public static List<Proveedor> GetProveedores()
        {
            var defaultProveedor = new Proveedor
            {
                ProveedorId = 0,
                RazonSocial = "[Seleccione Proveedor]"
            };
            var lista = Db.Proveedores
                .OrderBy(c => c.RazonSocial).ToList();
            lista.Insert(0, defaultProveedor);
            return lista;
        }



        public static List<Cliente> GetClientes()
        {
            var defaultCliente = new Cliente
            {
                ClienteId = 0,
                Nombre = "[Seleccione Cliente]"
            };
            var lista = Db.Clientes
                .OrderBy(c => c.Nombre).ToList();
            lista.Insert(0, defaultCliente);
            return lista;
        }


        public static List<Producto> GetProductos()
        {
            var defaultProductos = new Producto
            {
                ProductoId = 0,
                NombreProducto = "[Seleccione Producto]"
            };
            var lista = Db.Productos
                .OrderBy(c => c.NombreProducto).ToList();
            lista.Insert(0, defaultProductos);
            return lista;
        }


    }







}