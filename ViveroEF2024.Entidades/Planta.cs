
namespace ViveroEF2024.Entidades
{
    public class Planta
    {
        public int PlantaId { get; set; }

        public string Descripcion { get; set; } = null!;

        public int TipoDePlantaId { get; set; }

        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int TipoDeEnvaseId { get; set; }
        public TipoDePlanta? TipoDePlanta { get; set; }
        public TipoDeEnvase? TipoDeEnvase { get; set; }
        public ICollection<ProveedorPlanta> ProveedoresPlantas { get; set; } = new List<ProveedorPlanta>();
    }
}