namespace ViveroEF2024.Entidades.Dto
{
    public class PlantaListDto
    {
        public int PlantaId { get; set; }
        public string Nombre { get; set; }=null!;
        public string Tipo { get; set; }  =null!;
        public string Envase { get; set; } = null!;
        public decimal Precio { get; set; }
        public int CantidadProveedores { get; set; }
    }
}
