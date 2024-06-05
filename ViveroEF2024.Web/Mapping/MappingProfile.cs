using AutoMapper;
using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.ViewModels.TipoDePlanta;

namespace ViveroEF2024.Web.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadTipoDePlantasMapping();
        }

        private void LoadTipoDePlantasMapping()
        {
            CreateMap<TipoDePlanta, TipoDePlantaEditVm>()
                .ReverseMap();
        }
    }
}
