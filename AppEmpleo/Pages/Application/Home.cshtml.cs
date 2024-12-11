using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Services;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Pages.LandingPage
{
    public class HomeModel : PageModel
    {
        private readonly OfferRepository _offerRepository;
        private readonly ClaimsService _claimsService;

        [Required]
        public new Usuario User { get; set; }

        [BindProperty]
        public Oferta? Offer { get; set; }

        [BindProperty]
        public List<Oferta>? Offers { get; set; } = new List<Oferta>();

        public HomeModel(OfferRepository offerRepository, ClaimsService claimsService)
        {
            _offerRepository = offerRepository;
            _claimsService = claimsService;
        }

        public void OnGet()
        {
            GetAuthenticatedUser();
            GetOffersAsync();
        }

        private void GetAuthenticatedUser()
        {
            if (!_claimsService.AuthenticatedUser())
            {
                RedirectToPage("/Login");
            }

            User = new Usuario()
            {
                Nombre = _claimsService.GetName(),
                Email = _claimsService.GetEmail(),
                Rol = _claimsService.GetRole(),
            };
        }

        private async void GetOffersAsync()
        {
            Offers = await _offerRepository.GetListAsync(Offers);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage();
        }
    }
}
