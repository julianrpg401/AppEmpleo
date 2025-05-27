using AppEmpleo.Interfaces;
using AppEmpleo.Interfaces.Services;
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
        private readonly IUserService _userService;
        private readonly IOfferService _offerService;
        private readonly IPostulationService _postulationService;

        public List<Postulacion> TodasPostulaciones { get; set; } = [];

        [BindProperty]
        public IFormFile? CVFile { get; set; }

        [BindProperty]
        public int OfertaEmpleoId { get; set; }

        public new Usuario User { get; set; } = null!;

        [BindProperty]
        public Oferta Offer { get; set; } = null!;

        public List<Oferta> Offers { get; set; } = [];

        public HomeModel(IUserService userService, IOfferService offerService, IPostulationService postulationService)
        {
            _userService = userService;
            _offerService = offerService;
            _postulationService = postulationService;

            User = _userService.GetUserClaims();
        }

        // Obtiene las ofertas
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await GetOffersAsync();
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
            try
            {
                await _offerService.AddOfferAsync(Offer, User);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear la oferta");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la oferta. Por favor, inténtelo de nuevo más tarde.");
            }

            return Page();
        }

        // Aplica a una oferta de empleo
        public async Task<IActionResult> OnPostApplyAsync()
        {
            if ((CVFile == null || CVFile.Length == 0) || await _userService.GetCandidateAsync(User.UsuarioId) == null)
            {
                Log.Warning("El usuario {UserId} no tiene un candidato asociado o no se ha subido un archivo.", User.UsuarioId);
                ModelState.AddModelError(string.Empty, "Debe subir un archivo de currículum y tener un candidato asociado.");
                
                await OnGetAsync();

                return Page();
            }

            var candidate = await _userService.GetCandidateAsync(User.UsuarioId);
            await _postulationService.CreatePostulation(OfertaEmpleoId, candidate!, CVFile);

            return RedirectToPage();
        }

        // Obtiene las ofertas
        private async Task GetOffersAsync()
            => Offers = await _offerService.GetAllOffersAsync();
    }
}
