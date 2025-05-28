using AppEmpleo.Interfaces.Services;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace AppEmpleo.Pages.Application
{
    [Authorize(Roles = "RECLUTADOR")]
    public class EvaluationsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPostulationService _postulationService;

        public new Usuario User { get; set; } = null!;

        public List<Postulacion> AllPostulations { get; set; } = [];

        public EvaluationsModel(IUserService userService, IPostulationService postulationService)
        {
            _userService = userService;
            _postulationService = postulationService;

            User = _userService.GetUserClaims();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await GetPostulationsAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener las postulaciones");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar las postulaciones. Por favor, inténtelo de nuevo más tarde.");
            }

            return Page();
        }

        private async Task GetPostulationsAsync()
            => AllPostulations = await _postulationService.GetAllPostulationsAsync(User.UsuarioId);

        public async Task<IActionResult> OnGetDescargarCVAsync(int curriculumId)
        {
            try
            {
                var curriculum = await _postulationService.GetCurriculumByIdAsync(curriculumId);

                if (curriculum == null)
                {
                    Log.Warning("Currículum con ID {CurriculumId} no encontrado", curriculumId);
                    return NotFound();
                }

                var filePath = _postulationService.FilePath(curriculum);

                if (!System.IO.File.Exists(filePath))
                {
                    Log.Warning("Archivo de currículum no encontrado en la ruta {FilePath}", filePath);
                    return NotFound();
                }

                return await _postulationService.DownloadCurriculum(curriculum, filePath);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al descargar el CV con ID {CurriculumId}", curriculumId);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al descargar el CV. Por favor, inténtelo de nuevo más tarde.");
                
                return Page();
            }
        }
    }
}
