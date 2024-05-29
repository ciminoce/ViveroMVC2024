using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.Dto;

namespace ViveroEF2024.Datos.Interfaces
{
    public interface ITiposDeEnvasesRepository
    {
        void Agregar(TipoDeEnvase tipoDeEnvase);
        void Borrar(TipoDeEnvase tipoDeEnvase);
        void Editar(TipoDeEnvase tipoDeEnvase);
        bool Existe(TipoDeEnvase tipoEnvase);
        TipoDeEnvase? GetEnvasePorId(int idEditar);
        TipoDeEnvase? GetTipoDeEnvasePorNombre(string tipoDeEnvase);
        List<TipoDeEnvase> GetLista();
        bool EstaRelacionado(TipoDeEnvase tipoDeEnvase);
        IEnumerable<EnvaseConCantidadDePlantasDTO> CantidadDePlantasPorTipoDeEnvase();
    }
}
