using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.Dto;
using ViveroEF2024.Entidades.Enums;

namespace ViveroEF2024.Datos.Interfaces
{
    public interface IPlantasRepository
    {
        int GetCantidad(Func<Planta, bool>? filtro=null);
        void Agregar(Planta planta);
        void Editar(Planta planta);
        void Borrar(Planta planta);
        List<PlantaListDto> GetListaPaginadaOrdenadaFiltrada(int page,
            int pageSize, Orden? orden = null, TipoDePlanta? tipoPlantaFiltro = null,
            TipoDeEnvase? tipoEnvaseFiltro=null);
        List<Planta> GetLista();
        IEnumerable<object> GetListaAnonima();
        bool Existe(Planta planta);
        List<PlantaListDto> GetListaDto();
        Planta? GetPlantaPorId(int plantaId);
        List<Planta>? GetPlantas(TipoDePlanta? tipoDePlanta);
        List<PlantaListDto>? GetPlantasSinProveedor();
        void AgregarProveedorPlanta(ProveedorPlanta nuevaRelacion);
        void Editar(Planta planta, int? proveedorId);
        IEnumerable<IGrouping<int, Planta>> GetPlantasAgrupadasPorTipoDePlanta();
        List<Proveedor>? GetProveedoresPorPlanta(int plantaId);
        bool ExisteRelacion(Planta planta, Proveedor proveedor);
        void AgregarProveedoresPlanta(Planta planta, List<Proveedor> proveedores);
        //void EditarProveedoresPlanta(Planta planta, List<Proveedor> proveedores);
        void EliminarRelaciones(Planta planta);
    }
}
