using Microsoft.EntityFrameworkCore;
using ViveroEF2024.Datos.Interfaces;
using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.Dto;

namespace ViveroEF2024.Datos.Repositories
{
    public class ProveedoresRepository : IProveedoresRepository
    {
        private readonly ViveroDbContext _context;

        public ProveedoresRepository(ViveroDbContext context)
        {
            _context = context;
        }

        public void Agregar(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
        }

        public void AgregarProveedorPlanta(ProveedorPlanta nuevaRelacion)
        {
            _context.Set<ProveedorPlanta>().Add(nuevaRelacion);
        }

        public void Borrar(Proveedor proveedor)
        {
            _context.Proveedores.Remove(proveedor);
        }

        public void Editar(Proveedor proveedor)
        {
            _context.Proveedores.Update(proveedor);
        }

        public bool Existe(Proveedor proveedor)
        {
            return _context.Proveedores
                .Any(p => p.Nombre == proveedor.Nombre 
                && p.ProveedorId != proveedor.ProveedorId);
        }

        public List<Proveedor> GetLista()
        {
            return _context.Proveedores.ToList();
        }

        public Proveedor? GetProveedorPorId(int id, bool incluyePlantas = false)
        {
            var query = _context.Proveedores;
            if (incluyePlantas)
            {
                return query.Include(p => p.ProveedoresPlantas)
                    .ThenInclude(pp=>pp.Planta)
                    .FirstOrDefault(p => p.ProveedorId == id);
            }
            return query
                .FirstOrDefault(p => p.ProveedorId == id);
        }
    }
}
