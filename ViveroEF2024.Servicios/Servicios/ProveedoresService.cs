using ViveroEF2024.Datos.Interfaces;
using ViveroEF2024.Datos;
using ViveroEF2024.Entidades;
using ViveroEF2024.Servicios.Intefaces;

namespace ViveroEF2024.Servicios.Servicios
{
    public class ProveedoresService : IProveedoresService
    {
        private readonly IProveedoresRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProveedoresService(IProveedoresRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Guardar(Proveedor proveedor)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (proveedor.ProveedorId == 0)
                {
                    if (!_repository.Existe(proveedor))
                    {
                        _repository.Agregar(proveedor);
                    }
                    else
                    {
                        throw new Exception("Proveedor ya existe.");
                    }
                }
                else
                {
                    _repository.Editar(proveedor);
                }
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void Borrar(Proveedor proveedor)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Borrar(proveedor);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                return _repository.Existe(proveedor);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<Proveedor> GetLista()
        {
            try
            {
                return _repository.GetLista();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public Proveedor? GetProveedorPorId(int id, bool incluyePlantas = false)
        {
            try
            {
                return _repository.GetProveedorPorId(id,incluyePlantas);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }

}
