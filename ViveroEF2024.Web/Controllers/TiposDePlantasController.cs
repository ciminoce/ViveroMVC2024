using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ViveroEF2024.Entidades;
using ViveroEF2024.Entidades.ViewModels.TipoDePlanta;
using ViveroEF2024.Servicios.Intefaces;
using X.PagedList;

namespace ViveroEF2024.Web.Controllers
{
    public class TiposDePlantasController : Controller
    {
        private readonly ITiposDePlantasService? _servicios;
        private readonly IMapper? _mapper;
        public TiposDePlantasController(ITiposDePlantasService? servicios,
            IMapper mapper)
        {
            _servicios = servicios;
            _mapper = mapper;
        }

        public IActionResult Index(int? page=1)
        {
            int currentPage = page ?? 1;
            int pageSize = 10;
            var listaTipos = _servicios?.GetLista()
                .ToPagedList(currentPage,pageSize);
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
        public IActionResult Edit(int? id)
        {
            if(id is null || id.Value == 0)
            {
                return NotFound();
            }
            if (_servicios is null || _mapper is null)
            {
                return StatusCode(StatusCodes
                    .Status500InternalServerError,
                    "Dependencias no están configuradas correctamente");
            }
            TipoDePlanta? tipo = _servicios.GetTipoDePlantaPorId(id.Value);
            if (tipo is null)
            {
                return NotFound();
            }
            TipoDePlantaEditVm tipoVm=_mapper
                .Map<TipoDePlantaEditVm>(tipo);
            return View(tipoVm);
       }
        [HttpPost]
        public IActionResult Edit(TipoDePlantaEditVm tipoVm) 
        {
            if (!ModelState.IsValid)
            {
                return View(tipoVm);

            }
            if (_servicios is null || _mapper is null)
            {
                return StatusCode(StatusCodes
                    .Status500InternalServerError,
                    "Dependencias no están configuradas correctamente");
            }
            TipoDePlanta tipo=_mapper.Map<TipoDePlanta>(tipoVm);
            try
            {
                if (_servicios.Existe(tipo))
                {
                    ModelState.AddModelError(string.Empty, "Registro duplicado!!!");
                    return View(tipoVm);
                }
                _servicios.Guardar(tipo);
                TempData["success"] = "Registro editado satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty,ex.Message);
                return View(tipoVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value == 0)
            {
                return NotFound();
            }
            if (_servicios is null || _mapper is null)
            {
                return StatusCode(StatusCodes
                    .Status500InternalServerError,
                    "Dependencias no están configuradas correctamente");
            }
            TipoDePlanta? tipo = _servicios.GetTipoDePlantaPorId(id.Value);
            if (tipo is null)
            {
                return NotFound();
            }
            try
            {
                if (_servicios.EstaRelacionado(tipo))
                {
                    return Json(new { success = false, message = "Registro relacionado... Baja denegada" });
                }
                _servicios.Borrar(tipo);
                return Json(new { success = true, message = "Registro borrado satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
