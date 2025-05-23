using AppEmpleo.Class.Services;
using AppEmpleo.Class.Utilities;
using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace AppEmpleo.Pages.Application
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly IApplicationRepository _appRepo;

        private readonly IOfferRepository _offerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ClaimsService _claimsService;

        public List<Postulacion> TodasPostulaciones { get; set; } = [];

        [BindProperty]
        public IFormFile? CVFile { get; set; }

        [BindProperty]
        public int OfertaEmpleoId { get; set; }

        [BindProperty]
        public new Usuario User { get; set; } = null!;

        [BindProperty]
        public Oferta Offer { get; set; } = null!;

        public List<Oferta> Offers { get; set; } = [];

        public HomeModel(IWebHostEnvironment env, IApplicationRepository appRepo,IOfferRepository offerRepository, IUserRepository userRepository, ClaimsService claimsService)
        {
            _env = env;
            _appRepo = appRepo;

            _offerRepository = offerRepository;
            _userRepository = userRepository;
            _claimsService = claimsService;
        }

        // Obtiene las ofertas y el usuario autenticado
        public async Task<IActionResult> OnGetAsync()
        {
            if (!_claimsService.AuthenticatedUser())
            {
                return RedirectToPage("/Login/Login");
            }

            User = new Usuario()
            {
                UsuarioId = _claimsService.GetId(),
                Nombre = _claimsService.GetName(),
                Email = _claimsService.GetEmail(),
                Rol = _claimsService.GetRole()
            };

            try
            {
                await GetOffersAsync();
                await OnGetPostulacionesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener las ofertas");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al obtener la página de inicio. Por favor, inténtelo de nuevo más tarde.");
            }

            return Page();
        }

        // Añade una oferta a la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (!_claimsService.AuthenticatedUser())
            {
                return RedirectToPage("/Login/Login");
            }

            try
            {
                User = new Usuario()
                {
                    UsuarioId = _claimsService.GetId(),
                    Nombre = _claimsService.GetName(),
                    Email = _claimsService.GetEmail(),
                    Rol = _claimsService.GetRole()
                };

                Offer = OfferDataProcessor.OfferFormat(Offer, User);

                await _offerRepository.AddAsync(Offer);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear la oferta");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la oferta. Por favor, inténtelo de nuevo más tarde.");
            }

            return Page();
        }

        // Obtiene las ofertas
        private async Task GetOffersAsync()
            => Offers = await _offerRepository.GetOffersAsync();

        public async Task<IActionResult> OnGetPostulacionesAsync()
        {
            if (!_claimsService.AuthenticatedUser())
                return RedirectToPage("/Login/Login");

            try
            {
                TodasPostulaciones = await _appRepo.GetAllPostulacionesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener las postulaciones");
                ModelState.AddModelError(string.Empty, "No se pudieron cargar las postulaciones.");
            }

            return Page();
        }

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

        // Obtiene el usuario desde la base de datos
        private async Task GetUserAsync()
            => User = await _userRepository.GetUserAsync(User);

        public async Task<IActionResult> OnPostApplyAsync()
        {
            if (!_claimsService.AuthenticatedUser())
            {
                return RedirectToPage("/Login/Login");
            }

            if (CVFile == null || CVFile.Length == 0)
            {
                ModelState.AddModelError("", "Debe seleccionar un archivo.");
                await OnGetAsync(); // recarga ofertas
                return Page();
            }

            int usuarioId = _claimsService.GetId();

            // Obtén el candidato asociado a ese usuario
            var candidato = await _userRepository.GetCandidatoByUsuarioIdAsync(usuarioId);
            if (candidato == null)
            {
                ModelState.AddModelError("", "No se encontró el candidato asociado al usuario.");
                await OnGetAsync();
                return Page();
            }

            // 1. Guardar archivo en wwwroot/curriculums
            var folder = Path.Combine(_env.WebRootPath, "curriculums");
            Directory.CreateDirectory(folder);

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(CVFile.FileName)}";
            var savePath = Path.Combine(folder, uniqueName);

            using var stream = new FileStream(savePath, FileMode.Create);
            await CVFile.CopyToAsync(stream);

            // 2. Crear Curriculum
            var curriculum = new Curriculum
            {
                CandidatoId = candidato.CandidatoId,
                NombreArchivo = CVFile.FileName,
                RutaArchivo = $"/curriculums/{uniqueName}",
                FechaCarga = DateOnly.FromDateTime(DateTime.UtcNow),
                EsPreferido = false
            };
            await _appRepo.AddCurriculumAsync(curriculum);

            // 3. Crear Postulacion
            var postulacion = new Postulacion
            {
                OfertaEmpleoId = OfertaEmpleoId,
                CandidatoId = candidato.CandidatoId,
                CurriculumId = curriculum.CurriculumId,
                FechaPostulacion = DateTime.UtcNow
            };
            await _appRepo.AddPostulacionAsync(postulacion);

            return RedirectToPage(); // recarga y mostrará mensaje si agregas TempData
        }
    }
}
