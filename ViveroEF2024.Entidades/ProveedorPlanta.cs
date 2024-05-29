namespace ViveroEF2024.Entidades
{
    public class ProveedorPlanta
    {
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } = null!;
        public int PlantaId { get; set; }
        public Planta Planta { get; set; } = null!;
    }

}
