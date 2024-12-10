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

        public Usuario Usuario { get; set; }

        [BindProperty]
        public Oferta Oferta { get; set; }

        [BindProperty]
        public List<Oferta> Ofertas { get; set; } = new List<Oferta>();

        public HomeModel(OfferRepository offerRepository, ClaimsService claimsService)
        {
            _offerRepository = offerRepository;
            _claimsService = claimsService;
        }

        public async void OnGetAsync()
        {
            GetAuthenticatedUser();

            Ofertas = await _offerRepository.GetListAsync(Ofertas);
        }

        private void GetAuthenticatedUser()
        {
            if (!_claimsService.AuthenticatedUser())
            {
                RedirectToPage("/Login");
            }

            Usuario = new Usuario()
            {
                Nombre = _claimsService.ObtenerNombre(),
                Email = _claimsService.ObtenerEmail(),
                Rol = _claimsService.ObtenerRol(),
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage();
        }
    }
}
