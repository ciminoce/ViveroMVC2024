using Microsoft.EntityFrameworkCore;
using ViveroEF2024.Datos.Interfaces;
using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.Dto;
using ViveroEF2024.Entidades.Enums;

namespace ViveroEF2024.Datos.Repositories
{
    public class PlantasRepository : IPlantasRepository
    {
        private readonly ViveroDbContext _context;
        public PlantasRepository(ViveroDbContext context)
        {
            _context = context;
        }
        public void AgregarProveedoresPlanta(Planta planta, List<Proveedor> proveedores)
        {
            foreach (var proveedor in proveedores)
            {
                var proveedorExistente = _context.Proveedores
                    .FirstOrDefault(p => p.ProveedorId == proveedor.ProveedorId);

                if (proveedorExistente == null)
                {
                    _context.Proveedores.Add(proveedor); // Agregar nuevo proveedor
                    proveedorExistente = proveedor; // Establecer proveedorExistente como el nuevo proveedor
                }
                else
                {
                    _context.Proveedores.Attach(proveedorExistente); // Attach si el proveedor ya existe y está detached
                }

                if (!ExisteRelacion(planta, proveedorExistente))
                {
                    _context.ProveedoresPlantas.Add(new ProveedorPlanta
                    {
                        PlantaId = planta.PlantaId,
                        ProveedorId = proveedorExistente.ProveedorId
                    });
                }
            }
        }

        public void Agregar(Planta planta)
        {
            // Verificar si el TipoDePlanta asociado
            // a la planta ya existe en la base de datos
            var tipoDePlantaExistente = _context.TiposDePlantas
                .FirstOrDefault(t => t.TipoDePlantaId == planta.TipoDePlantaId);

            // Si el TipoDePlanta ya existe,
            // adjuntarlo al contexto en lugar de agregarlo nuevamente
            if (tipoDePlantaExistente != null)
            {
                _context.Attach(tipoDePlantaExistente);
                planta.TipoDePlanta = tipoDePlantaExistente;
            }
            // Verificar si el TipoDeEnvase asociado
            // a la planta ya existe en la base de datos

            var tipoDeEnvaseExistente = _context
                .TiposDeEnvases.FirstOrDefault(t => t.TipoDeEnvaseId == planta.TipoDeEnvaseId);
            if (tipoDeEnvaseExistente != null)
            {
                _context.Attach(tipoDeEnvaseExistente);
                planta.TipoDeEnvase = tipoDeEnvaseExistente;
            }

            // Agregar la planta al contexto de la base de datos
            _context.Plantas.Add(planta);
        }
        public void Borrar(Planta planta)
        {
            _context.Plantas.Remove(planta);
        }

        public void Editar(Planta planta)
        {
            //TODO:Ver que pasa con nuevo tipos 


            // a la planta ya existe en la base de datos
            var tipoDePlantaExistente = _context.TiposDePlantas
                .FirstOrDefault(t => t.TipoDePlantaId == planta.TipoDePlantaId);

            // Si el TipoDePlanta ya existe,
            // adjuntarlo al contexto en lugar de agregarlo nuevamente
            if (tipoDePlantaExistente != null)
            {
                _context.Attach(tipoDePlantaExistente);
                planta.TipoDePlanta = tipoDePlantaExistente;
            }

            var tipoDeEnvaseExistente = _context
                .TiposDeEnvases.FirstOrDefault(t => t.TipoDeEnvaseId == planta.TipoDeEnvaseId);
            if (tipoDeEnvaseExistente != null)
            {
                _context.Attach(tipoDeEnvaseExistente);
                planta.TipoDeEnvase = tipoDeEnvaseExistente;
            }

            // edita la planta al contexto de la base de datos
            _context.Plantas.Update(planta);

        }

        public int GetCantidad(Func<Planta, bool>? filtro = null)
        {
            if (filtro != null)
            {
                return _context.Plantas.Count(filtro);
            }
            else
            {
                return _context.Plantas.Count();
            }
        }

        public List<PlantaListDto> GetListaPaginadaOrdenadaFiltrada(int page,
            int pageSize, Orden? orden = null, TipoDePlanta? tipoPlantaFiltro = null,
            TipoDeEnvase? tipoEnvaseFiltro = null)
        {
            IQueryable<Planta> query = _context.Plantas
                .Include(p => p.TipoDePlanta)
                .Include(p => p.TipoDeEnvase)
                .Include(p=>p.ProveedoresPlantas)
                .AsNoTracking();

            // Aplicar filtro si se proporciona un tipo de planta
            if (tipoPlantaFiltro != null)
            {
                query = query
                    .Where(p => p.TipoDePlantaId == tipoPlantaFiltro.TipoDePlantaId);
            }

            // Aplicar filtro si se proporciona un tipo de envase
            if (tipoEnvaseFiltro != null)
            {
                query = query
                    .Where(p => p.TipoDeEnvaseId == tipoEnvaseFiltro.TipoDeEnvaseId);
            }

            // Aplicar orden si se proporciona
            if (orden != null)
            {
                switch (orden)
                {
                    case Orden.AZ:
                        query = query.OrderBy(p => p.Descripcion);
                        break;
                    case Orden.ZA:
                        query = query.OrderByDescending(p => p.Descripcion);
                        break;
                    case Orden.MenorPrecio:
                        query = query.OrderBy(p => p.PrecioVenta);
                        break;
                    case Orden.MayorPrecio:
                        query = query.OrderByDescending(p => p.PrecioVenta);
                        break;
                    default:
                        break;
                }
            }

            // Paginar los resultados
            List<Planta> listaPaginada = query
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            // Mapear los resultados a PlantaListDto
            List<PlantaListDto> listaDto = listaPaginada
                .Select(p => new PlantaListDto
                {
                    PlantaId = p.PlantaId,
                    Nombre = p.Descripcion,
                    Tipo = p.TipoDePlanta?.Descripcion??"N/A",
                    Envase = p.TipoDeEnvase?.Descripcion??"N/A",
                    Precio = p.PrecioVenta,
                    CantidadProveedores=p.ProveedoresPlantas.Count()
                })
                .ToList();

            return listaDto;
        }

        public IEnumerable<object> GetListaAnonima()
        {
            return _context.Plantas
                .Include(p => p.TipoDePlanta)
                .Include(p => p.TipoDeEnvase)
                .Select(n => new
                {
                    Id = n.PlantaId,
                    Planta = n.Descripcion,
                    TipoPlanta = n.TipoDePlanta != null ? n.TipoDePlanta.Descripcion : string.Empty,
                    TipoEnvase = n.TipoDeEnvase != null ? n.TipoDeEnvase.Descripcion : string.Empty,
                    PrecioVenta = n.PrecioVenta
                }).ToList();
        }



        public bool Existe(Planta planta)
        {
            if (planta.PlantaId == 0)
            {
                return _context.Plantas.Any(
                    p => p.Descripcion == planta.Descripcion &&
                    p.TipoDePlantaId == planta.TipoDePlantaId &&
                    p.TipoDeEnvaseId == planta.TipoDeEnvaseId);
            }
            return _context.Plantas.Any(
                p => p.Descripcion == planta.Descripcion &&
                p.TipoDePlantaId == planta.TipoDePlantaId &&
                p.TipoDeEnvaseId == planta.TipoDeEnvaseId &&
                p.PlantaId != planta.PlantaId);

        }

        public List<PlantaListDto> GetListaDto()
        {
            return _context.Plantas
                .Include(p => p.TipoDePlanta)
                .Include(p => p.TipoDeEnvase)
                .Select(n => new PlantaListDto
                {
                    PlantaId = n.PlantaId,
                    Nombre = n.Descripcion,
                    Tipo = n.TipoDePlanta != null ? n.TipoDePlanta.Descripcion : string.Empty,
                    Envase = n.TipoDeEnvase != null ? n.TipoDeEnvase.Descripcion : string.Empty,
                    Precio = n.PrecioVenta
                }).ToList();
        }

        public List<Planta> GetLista()
        {
            return _context.Plantas
                .Include(p => p.TipoDePlanta)
                .Include(p => p.TipoDeEnvase)
                .ToList();

        }

        public Planta? GetPlantaPorId(int plantaId)
        {
            return _context.Plantas.Include(p => p.TipoDePlanta)
                .Include(p => p.TipoDeEnvase)
                .FirstOrDefault(p => p.PlantaId == plantaId);
        }

        public List<Planta>? GetPlantas(TipoDePlanta? tipoDePlanta)
        {
            if (tipoDePlanta != null)
            {
                // Cargar las plantas asociadas al tipo de planta específico
                _context.Entry(tipoDePlanta)
                                .Collection(tp => tp.Plantas)
                                .Query()
                                .Include(p => p.TipoDePlanta)
                                .Include(p => p.TipoDeEnvase)
                                .Load();
                // Obtener las plantas cargadas
                var plantas = tipoDePlanta.Plantas.ToList();

                return plantas;

            }
            return null;
        }

        public List<PlantaListDto> GetPlantasSinProveedor()
        {
            return _context.Plantas
                .Include(p => p.TipoDePlanta)
                .Include(p => p.TipoDeEnvase)
                .Where(p => !p.ProveedoresPlantas.Any())
                .Select(p => new PlantaListDto
                {
                    PlantaId = p.PlantaId,
                    Nombre = p.Descripcion,
                    Envase = p.TipoDeEnvase==null?"N/A":p.TipoDeEnvase.Descripcion,
                    Tipo = p.TipoDePlanta==null?"N/A":p.TipoDePlanta.Descripcion,
                    Precio = p.PrecioVenta
                })
                .ToList();
        }


        public void AgregarProveedorPlanta(ProveedorPlanta nuevaRelacion)
        {
            _context.Set<ProveedorPlanta>().Add(nuevaRelacion);
        }

        public void Editar(Planta planta, int? proveedorId)
        {
            _context.Plantas.Update(planta);
        }

        public IEnumerable<IGrouping<int, Planta>> GetPlantasAgrupadasPorTipoDePlanta()
        {
            return _context.Plantas.GroupBy(p => p.TipoDePlantaId)
                .ToList();
        }

        public List<Proveedor>? GetProveedoresPorPlanta(int plantaId)
        {
            return _context.ProveedoresPlantas
                .Include(pp=>pp.Proveedor)
                .Where(pp=>pp.PlantaId == plantaId)
                .Select(pp=>pp.Proveedor)
                .ToList();

        }

        public bool ExisteRelacion(Planta planta, 
            Proveedor proveedor)
        {
            if (planta == null || proveedor == null) return false;

            return _context.ProveedoresPlantas
                .Any(pp => pp.PlantaId == planta.PlantaId 
                && pp.ProveedorId == proveedor.ProveedorId);
        }

        //public void EditarProveedoresPlanta(Planta planta, List<Proveedor> proveedores)
        //{
        //    // Si hay proveedores, agregar las relaciones
        //    if (proveedores != null && proveedores.Any())
        //    {

        //        foreach (var proveedor in proveedores)
        //        {
        //            //TODO:considerar que el proveedor podría ser nuevo
        //            // Attach si el proveedor ya existe
        //            _context.Proveedores.Attach(proveedor); 

        //            if (!ExisteRelacion(planta, proveedor))
        //            {
        //                _context.ProveedoresPlantas.Add(new ProveedorPlanta
        //                {
        //                    PlantaId = planta.PlantaId,
        //                    ProveedorId = proveedor.ProveedorId
        //                });

        //            }
        //        }
                
        //    }
        //}

        public void EliminarRelaciones(Planta planta)
        {
            var relacionesPasadas = _context.ProveedoresPlantas
                .Where(pp => pp.PlantaId == planta.PlantaId)
                .ToList();

            _context.ProveedoresPlantas
                .RemoveRange(relacionesPasadas);
        }

    }
}
