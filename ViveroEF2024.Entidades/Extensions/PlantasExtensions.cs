using ViveroEF2024.Entidades.Dto;

namespace ViveroEF2024.Entidades.Extensions
{
    public static class PlantasExtensions
    {
        public static Planta FromPlantaEditDtoToPlanta(this PlantaEditDto plantaDto)
        {
            return new Planta
            {
                PlantaId = plantaDto.PlantaId,
                Descripcion = plantaDto.Descripcion,
                PrecioCosto = plantaDto.PrecioCosto,
                PrecioVenta = plantaDto.PrecioVenta,
                TipoDePlantaId = plantaDto.TipoDePlantaId,
                TipoDeEnvaseId = plantaDto.TipoDeEnvaseId
            };
        }
    }
}
