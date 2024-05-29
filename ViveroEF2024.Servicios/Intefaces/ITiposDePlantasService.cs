using ViveroEF2024.Datos;
using ViveroEF2024.Entidades;

namespace ViveroEF2024.Servicios.Intefaces
{
    public interface ITiposDePlantasService
    {
        void Guardar(TipoDePlanta tipoDePlanta);
        void Borrar(TipoDePlanta tipoDePlanta);
        bool EstaRelacionado(TipoDePlanta tipoDePlanta);
        bool Existe(TipoDePlanta tipoDePlanta);
        int GetCantidad();
        List<TipoDePlanta> GetLista();
        TipoDePlanta? GetTipoDePlantaPorId(int tipoDePlantaId);
        List<Planta>? GetPlantas(TipoDePlanta? tipoDePlanta);
        TipoDePlanta? GetTipoDePlantaPorNombre(string tipoDePlanta);

    }
}
