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

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }
        public int TotalOffers { get; set; }

        public HomeModel(IUserService userService, IOfferService offerService, IPostulationService postulationService)
        {
            _userService = userService;
            _offerService = offerService;
            _postulationService = postulationService;

            User = _userService.GetUserClaims();
        }

        // Obtiene las ofertas paginadas
        public async Task<IActionResult> OnGetAsync(int? pageNumber)
        {
            CurrentPage = pageNumber ?? 1;
            await GetOffersPagedAsync();
            return Page();
        }

        private async Task GetOffersPagedAsync()
        {
            (List<Oferta> offers, int totalCount) = await _offerService.GetOffersPagedAsync(CurrentPage, PageSize);
            Offers = offers;
            TotalOffers = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        }

        // Añade una oferta a la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            await _offerService.AddOfferAsync(Offer, User);
            return Page();
        }

        // Aplica a una oferta de empleo
        public async Task<IActionResult> OnPostApplyAsync()
        {
            if ((CVFile == null || CVFile.Length == 0) || await _userService.GetCandidateAsync(User.UsuarioId) == null)
            {
                ModelState.AddModelError(string.Empty, "Debe subir un archivo de currículum y tener un candidato asociado.");
                await OnGetAsync(CurrentPage);
                return Page();
            }
            var candidate = await _userService.GetCandidateAsync(User.UsuarioId);
            await _postulationService.CreatePostulation(OfertaEmpleoId, candidate!, CVFile);
            return RedirectToPage(new { pageNumber = CurrentPage });
        }
    }
}
