
namespace ViveroEF2024.Entidades.Dto
{
    public class PlantaEditDto
    {
        public int PlantaId { get; set; }
        public string Descripcion { get; set; } = null!;
        public int TipoDePlantaId { get; set; }
        public int TipoDeEnvaseId { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }

    }
}
