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

        public async Task OnGet()
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
        }

        private async Task GetPostulationsAsync()
            => AllPostulations = await _postulationService.GetAllPostulationsAsync(User.UsuarioId);

        public async Task<IActionResult> OnGetDescargarCVAsync(int curriculumId)
        {
            var curriculum = await _appRepo.GetCurriculumByIdAsync(curriculumId);
            if (curriculum == null)
                return NotFound();

            var filePath = Path.Combine(_env.WebRootPath, curriculum.RutaArchivo.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var contentType = "application/octet-stream";
            var fileName = curriculum.NombreArchivo;

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, contentType, fileName);
        }
    }
}
