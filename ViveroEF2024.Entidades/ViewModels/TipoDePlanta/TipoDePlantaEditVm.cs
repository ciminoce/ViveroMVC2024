using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViveroEF2024.Entidades.ViewModels.TipoDePlanta
{
    public class TipoDePlantaEditVm
    {
        public int TipoDePlantaId { get; set; }
        [Required(ErrorMessage ="{0} es requerido")]
        [StringLength(50,ErrorMessage ="{0} debe contener entre {2} y {1} caracteres",MinimumLength =3)]
        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }
    }
}
