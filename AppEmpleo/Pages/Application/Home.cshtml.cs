using AppEmpleo.Interfaces.Services;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.Application
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IOfferService _offerService;
        private readonly IJobApplicationService _applicationService;

        [BindProperty]
        public IFormFile? CVFile { get; set; }

        [BindProperty]
        public int JobOfferId { get; set; }

        public UserAccount CurrentUser { get; private set; } = null!;

        [BindProperty]
        public JobOffer Offer { get; set; } = new() { Country = string.Empty, Currency = string.Empty };

        public List<JobOffer> Offers { get; set; } = [];

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }
        public int TotalOffers { get; set; }

        public HomeModel(IUserService userService, IOfferService offerService, IJobApplicationService applicationService)
        {
            _userService = userService;
            _offerService = offerService;
            _applicationService = applicationService;
        }

        // Retrieves paged offers.
        public async Task<IActionResult> OnGetAsync(int? pageNumber)
        {
            LoadCurrentUser();
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

        // Creates a new job offer.
        public async Task<IActionResult> OnPostAsync()
        {
            LoadCurrentUser();
            if (!ModelState.IsValid)
            {
                await OnGetAsync(CurrentPage);
                return Page();
            }

            await _offerService.AddOfferAsync(Offer, CurrentUser);
            // Redirect-to-GET to refresh paged list and show the new offer in the same page.
            return RedirectToPage(new { pageNumber = 1 });
        }

        // Applies to a job offer.
        public async Task<IActionResult> OnPostApplyAsync()
        {
            LoadCurrentUser();
            var candidate = await _userService.GetCandidateByUserIdAsync(CurrentUser.UserId);

            if (CVFile == null || CVFile.Length == 0 || candidate == null)
            {
                ModelState.AddModelError(string.Empty, "Debe subir un archivo de curr�culum y tener un candidato asociado.");
                await OnGetAsync(CurrentPage);
                return Page();
            }
            await _applicationService.CreateApplicationAsync(JobOfferId, candidate!, CVFile);
            return RedirectToPage(new { pageNumber = CurrentPage });
        }

        private void LoadCurrentUser()
        {
            CurrentUser = _userService.GetCurrentUserFromClaims();
        }
    }
}
