using ViveroEF2024.Datos;
using ViveroEF2024.Datos.Interfaces;
using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.Dto;
using ViveroEF2024.Servicios.Intefaces;

namespace ViveroEF2024.Servicios.Servicios
{
    public class TiposDeEnvasesService : ITiposDeEnvasesService
    {
        private readonly ITiposDeEnvasesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public TiposDeEnvasesService(ITiposDeEnvasesRepository repository,
            IUnitOfWork uniOfWork)
        {
            _repository = repository;
            _unitOfWork = uniOfWork;
        }

        public void Borrar(TipoDeEnvase tipoDeEnvase)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Borrar(tipoDeEnvase);
                _unitOfWork.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }        }

        public IEnumerable<EnvaseConCantidadDePlantasDTO> CantidadDePlantasPorTipoDeEnvase()
        {
            return _repository.CantidadDePlantasPorTipoDeEnvase();
        }

        public bool EstaRelacionado(TipoDeEnvase tipoDeEnvase)
        {
            return _repository.EstaRelacionado(tipoDeEnvase);
        }

        public bool Existe(TipoDeEnvase tipoEnvase)
        {
            return _repository.Existe(tipoEnvase);
        }

        public TipoDeEnvase? GetEnvasePorId(int idEditar)
        {
            return _repository.GetEnvasePorId(idEditar);
        }

        public List<TipoDeEnvase> GetLista()
        {
            return _repository.GetLista();
        }

        public TipoDeEnvase? GetTipoDeEnvasePorNombre(string tipoDeEnvase)
        {
            return _repository.GetTipoDeEnvasePorNombre(tipoDeEnvase);
        }

        public void Guardar(TipoDeEnvase tipoDeEnvase)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (tipoDeEnvase.TipoDeEnvaseId == 0)
                {
                    _repository.Agregar(tipoDeEnvase);
                }
                else
                {
                    _repository.Editar(tipoDeEnvase);
                }
                _unitOfWork.Commit();

            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            } 
        }
    }
}
