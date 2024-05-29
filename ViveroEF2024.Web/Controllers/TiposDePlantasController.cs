using Microsoft.AspNetCore.Mvc;
using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.ViewModels.TipoDePlanta;
using ViveroEF2024.Servicios.Intefaces;

namespace ViveroEF2024.Web.Controllers
{
    public class TiposDePlantasController : Controller
    {
        private readonly ITiposDePlantasService? _servicios;

        public TiposDePlantasController(ITiposDePlantasService? servicios)
        {
            _servicios = servicios;
        }

        public IActionResult Index()
        {
            var listaTipos = _servicios?.GetLista();
            return View(listaTipos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TipoDePlantaEditVm tipoVm)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoVm);
            }
            TipoDePlanta tipo = new TipoDePlanta
            {
                TipoDePlantaId = tipoVm.TipoDePlantaId,
                Descripcion = tipoVm.Descripcion ?? string.Empty
            };
            if (_servicios?.Existe(tipo)??true)
            {
                ModelState.AddModelError(string.Empty, "Registro duplicado!!!");
                return View(tipoVm);
            }
            _servicios.Guardar(tipo);
            TempData["success"] = "Registro agregado satisfactoriamente!!!";
            return RedirectToAction("Index");
        }
    }
}
