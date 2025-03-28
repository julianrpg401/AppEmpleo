using AppEmpleo.Class.Services;
using AppEmpleo.Class.Utilities;
using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.Application
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ClaimsService _claimsService;

        [BindProperty]
        public new Usuario User { get; set; } = null!;

        [BindProperty]
        public Oferta Offer { get; set; } = null!;

        public List<Oferta> Offers { get; set; } = [];

        public HomeModel(IOfferRepository offerRepository, IUserRepository userRepository, ClaimsService claimsService)
        {
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

            await GetOffersAsync();

            return Page();
        }

        // Añade una oferta a la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Rol != "RECLUTADOR")
            {
                return Forbid();
            }

            Offer = OfferDataProcessor.OfferFormat(Offer, User);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _offerRepository.AddAsync(Offer);

            return RedirectToPage();
        }

        // Obtiene las ofertas
        private async Task GetOffersAsync()
            => Offers = await _offerRepository.GetOffersAsync();

        // Obtiene el usuario desde la base de datos
        private async Task GetUserAsync()
            => User = await _userRepository.GetUserAsync(User);
    }
}
