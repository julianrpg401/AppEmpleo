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

        [BindProperty]
        public IFormFile? CVFile { get; set; }

        [BindProperty]
        public int JobOfferId { get; set; }

        public UserAccount CurrentUser { get; set; } = null!;

        [BindProperty]
        public JobOffer Offer { get; set; } = new() { Country = string.Empty, Currency = string.Empty };

        public List<JobOffer> Offers { get; set; } = [];

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }
        public int TotalOffers { get; set; }

        public HomeModel(IUserService userService, IOfferService offerService, IPostulationService postulationService)
        {
            _userService = userService;
            _offerService = offerService;
            _postulationService = postulationService;

            CurrentUser = _userService.GetUserClaims();
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
            (List<JobOffer> offers, int totalCount) = await _offerService.GetOffersPagedAsync(CurrentPage, PageSize);
            Offers = offers;
            TotalOffers = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        }

        // A�ade una oferta a la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(CurrentPage);
                return Page();
            }

            await _offerService.AddOfferAsync(Offer, CurrentUser);
            // Redirect-to-GET to refresh paged list and show the new offer in the same page.
            return RedirectToPage(new { pageNumber = 1 });
        }

        // Aplica a una oferta de empleo
        public async Task<IActionResult> OnPostApplyAsync()
        {
            if ((CVFile == null || CVFile.Length == 0) || await _userService.GetCandidateAsync(CurrentUser.UserId) == null)
            {
                ModelState.AddModelError(string.Empty, "Debe subir un archivo de curr�culum y tener un candidato asociado.");
                await OnGetAsync(CurrentPage);
                return Page();
            }
            var candidate = await _userService.GetCandidateAsync(CurrentUser.UserId);
            await _postulationService.CreatePostulation(JobOfferId, candidate!, CVFile);
            return RedirectToPage(new { pageNumber = CurrentPage });
        }
    }
}
