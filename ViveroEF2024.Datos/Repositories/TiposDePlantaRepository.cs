using Microsoft.EntityFrameworkCore;
using ViveroEF2024.Datos.Interfaces;
using ViveroEF2024.Entidades;

namespace ViveroEF2024.Datos.Repositories
{
    public class TiposDePlantaRepository : ITiposDePlantaRepository
    {
        private readonly ViveroDbContext _context;
        public TiposDePlantaRepository(ViveroDbContext context)
        {
            _context = context;
        }
        public TipoDePlanta? GetTipoDePlanta(string tipoDePlanta)
        {
            return _context.TiposDePlantas
                .FirstOrDefault(te => te.Descripcion == tipoDePlanta);
        }

        public void Agregar(TipoDePlanta tipoDePlanta)
        {
            _context.TiposDePlantas.Add(tipoDePlanta);
            
        }

        public void Borrar(TipoDePlanta tipoDePlanta)
        {
            _context.TiposDePlantas.Remove(tipoDePlanta);
            
        }

        public void Editar(TipoDePlanta tipoDePlanta)
        {
            _context.TiposDePlantas.Update(tipoDePlanta);
            
        }

        public bool EstaRelacionado(TipoDePlanta tipoDePlanta)
        {
            return _context.Plantas
                .Any(p => p.TipoDePlantaId == tipoDePlanta.TipoDePlantaId);
        }

        public bool Existe(TipoDePlanta tipoDePlanta)
        {
            if (tipoDePlanta.TipoDePlantaId == 0)
            {
                return _context.TiposDePlantas
                    .Any(t => t.Descripcion == tipoDePlanta.Descripcion);
            }
            return _context.TiposDePlantas
                .Any(t => t.Descripcion == tipoDePlanta.Descripcion &&
                    t.TipoDePlantaId != tipoDePlanta.TipoDePlantaId);
        }

        public int GetCantidad()
        {
            return _context.TiposDePlantas.Count();
        }

        public List<TipoDePlanta> GetLista()
        {
            return _context.TiposDePlantas
                .AsNoTracking()
                .ToList();
        }

        public TipoDePlanta? GetPlantaPorNombre(string tipoDescripcion)
        {
            return _context.TiposDePlantas.AsNoTracking()
                .FirstOrDefault(t => t.Descripcion == tipoDescripcion);
        }


        public TipoDePlanta? GetTipoDePlantaPorId(int tipoDePlantaId)
        {
            return _context.TiposDePlantas
                .FirstOrDefault(t => t.TipoDePlantaId == tipoDePlantaId);
        }

        public List<Planta>? GetPlantas(TipoDePlanta? tipoDePlanta)
        {
            throw new NotImplementedException();
        }
    }

}
