using ViveroEF2024.Datos;
using ViveroEF2024.Datos.Interfaces;
using ViveroEF2024.Datos.Repositories;
using ViveroEF2024.Entidades;
using ViveroEF2024.Servicios.Intefaces;

namespace ViveroEF2024.Servicios.Servicios
{
    public class TiposDePlantaService : ITiposDePlantasService
    {
        private readonly ITiposDePlantaRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public TiposDePlantaService(ITiposDePlantaRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(TipoDePlanta tipoDePlanta)
        {

            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Borrar(tipoDePlanta);
                _unitOfWork.Commit();

            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool EstaRelacionado(TipoDePlanta tipoDePlanta)
        {
            return _repository.EstaRelacionado(tipoDePlanta);
        }

        public bool Existe(TipoDePlanta tipoDePlanta)
        {
            return _repository.Existe(tipoDePlanta);
        }

        public int GetCantidad()
        {
            return _repository.GetCantidad();
        }

        public List<TipoDePlanta> GetLista()
        {
            return _repository.GetLista();
        }

        public TipoDePlanta? GetPlantaPorNombre(string tipoDescripcion)
        {
            return _repository.GetPlantaPorNombre(tipoDescripcion);
        }

        public List<Planta>? GetPlantas(TipoDePlanta? tipoDePlanta)
        {
            return _repository.GetPlantas(tipoDePlanta);
        }

        public TipoDePlanta? GetTipoDePlantaPorNombre(string tipoDePlanta)
        {
            return _repository.GetPlantaPorNombre(tipoDePlanta);
        }

        public TipoDePlanta? GetTipoDePlantaPorId(int tipoDePlantaId)
        {
            return _repository.GetTipoDePlantaPorId(tipoDePlantaId);
        }

        public void Guardar(TipoDePlanta tipoDePlanta)
        {

            try
            {
                _unitOfWork.BeginTransaction();
                if (tipoDePlanta.TipoDePlantaId == 0)
                {
                    _repository.Agregar(tipoDePlanta);
                }
                else
                {
                    _repository.Editar(tipoDePlanta);
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
