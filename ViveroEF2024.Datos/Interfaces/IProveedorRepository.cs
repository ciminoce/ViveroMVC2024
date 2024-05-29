using ViveroEF2024.Entidades;

namespace ViveroEF2024.Datos.Interfaces
{
    public interface IProveedoresRepository
    {
        void Agregar(Proveedor proveedor);
        void AgregarProveedorPlanta(ProveedorPlanta nuevaRelacion);
        void Borrar(Proveedor proveedor);
        void Editar(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        List<Proveedor> GetLista();
        Proveedor? GetProveedorPorId(int id, bool incluyePlantas=false);
    }
}
