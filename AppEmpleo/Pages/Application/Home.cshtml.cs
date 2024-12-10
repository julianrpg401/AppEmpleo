using AppEmpleo.Class;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using System.ComponentModel;

namespace AppEmpleo.Pages.LandingPage
{
    public class HomeModel : PageModel
    {
        private readonly OfferRepository _offerRepository;
        private readonly ClaimsService _claimsService;

        public new Usuario User { get; set; }

        [BindProperty]
        public Oferta Offer { get; set; }

        [BindProperty]
        public List<Oferta> Offers { get; set; } = new List<Oferta>();

        public HomeModel(OfferRepository offerRepository, ClaimsService claimsService)
        {
            _offerRepository = offerRepository;
            _claimsService = claimsService;
        }

        public async void OnGetAsync()
        {
            GetAuthenticatedUser();

            Offers = await _offerRepository.GetListAsync(Offers);
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

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage();
        }
    }
}
