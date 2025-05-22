using AppEmpleo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.Application
{
    [Authorize(Roles = "RECLUTADOR")]
    public class DownloadCVModel : PageModel
    {
        private readonly IApplicationRepository _appRepo;
        private readonly IWebHostEnvironment _env;

        public DownloadCVModel(IApplicationRepository appRepo, IWebHostEnvironment env)
        {
            _appRepo = appRepo;
            _env = env;
        }

        // Para poder reutilizar en la vista
        [BindProperty(SupportsGet = true)]
        public int CurriculumId { get; set; }

        // Mensaje de error (si algo falla antes de devolver el FileResult)
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var cv = await _appRepo.GetCurriculumByIdAsync(CurriculumId);
            if (cv == null)
            {
                ErrorMessage = "No se encontró el curriculum solicitado.";
                return Page();
            }

            var filePhysical = Path.Combine(_env.WebRootPath, cv.RutaArchivo.TrimStart('/'));
            if (!System.IO.File.Exists(filePhysical))
            {
                ErrorMessage = "El archivo ya no está disponible en el servidor.";
                return Page();
            }

            // Todo OK: devolvemos el archivo
            var mime = cv.NombreArchivo.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)
                       ? "application/pdf"
                       : "application/octet-stream";
            var stream = System.IO.File.OpenRead(filePhysical);
            return File(stream, mime, cv.NombreArchivo);
        }
    }
}
