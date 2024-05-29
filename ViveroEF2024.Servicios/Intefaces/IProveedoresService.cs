using ViveroEF2024.Entidades;

namespace ViveroEF2024.Servicios.Intefaces
{
    public interface IProveedoresService
    {
        void Guardar(Proveedor proveedor);
        void Borrar(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        List<Proveedor> GetLista();
        Proveedor? GetProveedorPorId(int id, bool incluyePlantas = false);
    }
}
