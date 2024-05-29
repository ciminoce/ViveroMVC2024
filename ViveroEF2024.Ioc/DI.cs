using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViveroEF2024.Datos;
using ViveroEF2024.Datos.Interfaces;
using ViveroEF2024.Datos.Repositories;
using ViveroEF2024.Servicios.Intefaces;
using ViveroEF2024.Servicios.Servicios;

namespace ViveroEF2024.Ioc
{
    public static class DI
    {
        public static void ConfigurarServicios(IServiceCollection servicios)
        {
            

            servicios.AddScoped<ITiposDePlantaRepository,
                TiposDePlantaRepository>();
            servicios.AddScoped<ITiposDeEnvasesRepository, 
                TiposDeEnvasesRepository>(); 
            servicios.AddScoped<IPlantasRepository,PlantasRepository>();

            servicios.AddScoped<IProveedoresRepository, ProveedoresRepository>();

            servicios.AddScoped<ITiposDePlantasService,
                TiposDePlantaService>();
            servicios.AddScoped<ITiposDeEnvasesService, TiposDeEnvasesService>();
            servicios.AddScoped<IPlantasService, PlantasService>();
            servicios.AddScoped<IProveedoresService, ProveedoresService>();

            servicios.AddScoped<IUnitOfWork, UnitOfWork>();
            servicios.AddDbContext<ViveroDbContext>(optiones =>
            {
                optiones.UseSqlServer(@"Data Source=.; 
                        Initial Catalog=ViveroEF2024; 
                        Trusted_Connection=true; 
                        TrustServerCertificate=true;");
            });

            
        }

    }
}
