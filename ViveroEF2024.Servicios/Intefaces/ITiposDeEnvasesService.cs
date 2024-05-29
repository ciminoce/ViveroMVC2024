using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.Dto;

namespace ViveroEF2024.Servicios.Intefaces
{
    public interface ITiposDeEnvasesService
    {
        List<TipoDeEnvase> GetLista();
        void Guardar(TipoDeEnvase tipoDeEnvase);
        void Borrar(TipoDeEnvase tipoDeEnvase);
        bool Existe(TipoDeEnvase tipoEnvase);
        TipoDeEnvase? GetEnvasePorId(int idEditar);
        TipoDeEnvase? GetTipoDeEnvasePorNombre(string tipoDeEnvase);
        bool EstaRelacionado(TipoDeEnvase tipoDeEnvase);
        IEnumerable<EnvaseConCantidadDePlantasDTO> CantidadDePlantasPorTipoDeEnvase();
    }
}
