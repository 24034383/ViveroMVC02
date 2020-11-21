using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViveroMVC02.Models;
using ViveroMVC02.ViewModels.Categoria;
using ViveroMVC02.ViewModels.Cliente;
using ViveroMVC02.ViewModels.Producto;
using AutoMapper;
using ViveroMVC02.ViewModels.Localidad;
using ViveroMVC02.ViewModels.Provincia;
using ViveroMVC02.ViewModels.Proveedor;
using ViveroMVC02.ViewModels.Empleado;
using ViveroMVC02.ViewModels.Compra;
using ViveroMVC02.ViewModels.Venta;
using ViveroMVC02.ViewModels.ItemCompra;

namespace ViveroMVC02.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Provincia, ProvinciaEditViewModels>();
            CreateMap<Provincia, ProvinciaListViewModels>();
            CreateMap<ProvinciaEditViewModels, Provincia>();



            CreateMap<Localidad, LocalidadListViewModels>()
                .ForMember(dest => dest.NombreProvincia, c => c.MapFrom(s => s.Provincia.NombreProvincia));
            CreateMap<Localidad, LocalidadEditViewModels>();
            CreateMap<LocalidadEditViewModels, Localidad>();

            CreateMap<Categoria, CategoriaEditViewModels>();
            CreateMap<Categoria, CategoriaListViewModels>();
            CreateMap<CategoriaEditViewModels, Categoria>();

            CreateMap<Cliente, ClienteListViewModels>()
                .ForMember(dest => dest.Provincia, c => c.MapFrom(s => s.Provincia.NombreProvincia))
                .ForMember(dest => dest.Localidad, c => c.MapFrom(s => s.Localidad.NombreLocalidad));
            CreateMap<Cliente, ClienteEditViewModels>();
            CreateMap<ClienteEditViewModels, Cliente>();

            CreateMap<Producto, ProductoListViewModels>()
                .ForMember(dest => dest.Categoria, c => c.MapFrom(s => s.Categoria.NombreCategoria));
            CreateMap<Producto, ProductoEditViewModels>();
            CreateMap<ProductoEditViewModels, Producto>();



            CreateMap<Proveedor, ProveedorListViewModels>()
                .ForMember(dest => dest.Provincia, c => c.MapFrom(s => s.Provincia.NombreProvincia))
                .ForMember(dest => dest.Localidad, c => c.MapFrom(s => s.Localidad.NombreLocalidad));
            CreateMap<Proveedor, ProveedorEditViewModels>();
            CreateMap<ProveedorEditViewModels, Proveedor>();



            CreateMap<Empleado, EmpleadoListViewModels>()
                .ForMember(dest => dest.Provincia, c => c.MapFrom(s => s.Provincia.NombreProvincia))
                .ForMember(dest => dest.Localidad, c => c.MapFrom(s => s.Localidad.NombreLocalidad));
            CreateMap<Empleado, EmpleadoEditViewModels>();
            CreateMap<EmpleadoEditViewModels, Empleado>();



            CreateMap<Compra, CompraListViewModels>()
                .ForMember(dest => dest.Proveedor, c => c.MapFrom(s => s.Proveedor.RazonSocial));
            CreateMap<Compra, CompraEdiViewModels>();
            CreateMap<CompraEdiViewModels, Compra>();


            CreateMap<Venta, VentaListViewModels>()
                .ForMember(dest => dest.Cliente, c => c.MapFrom(s => s.Cliente.Nombre));
            CreateMap<Venta, VentaEditViewModels>();
            CreateMap<VentaEditViewModels, Venta>();


            CreateMap<ItemCompra, ItemCompraListViewModels>()
               .ForMember(dest => dest.Producto, c => c.MapFrom(s => s.Producto.NombreProducto))
               .ForMember(dest => dest.Compra, c => c.MapFrom(s => s.Compra.CompraId));
            CreateMap<ItemCompra, ItemCompraEditViewModels>();
            CreateMap<ItemCompraEditViewModels, ItemCompra>();

        }
    }
}