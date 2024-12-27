using AppEmpleo.Class.Services;
using AppEmpleo.Class.Utilities;
using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.Application
{
    public class HomeModel : PageModel
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ClaimsService _claimsService;

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

        public async Task OnGetAsync()
        {
            GetAuthenticatedUser();
            await GetOffersAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            GetAuthenticatedUser();
            await GetUserAsync();

            Offer = OfferDataProcessor.OfferFormat(Offer, User);

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Estado del modelo no v�lido");
                return Page();
            }

            await _offerRepository.AddAsync(Offer);

            return RedirectToPage();
        }

        private void GetAuthenticatedUser()
        {
            if (!_claimsService.AuthenticatedUser())
            {
                RedirectToPage("/Login");
            }

            User = new Usuario()
            {
                UsuarioId = _claimsService.GetId(),
                Nombre = _claimsService.GetName(),
                Email = _claimsService.GetEmail(),
                Rol = _claimsService.GetRole()
            };
        }

        private async Task GetOffersAsync()
            => Offers = await _offerRepository.GetOffersAsync();

        private async Task GetUserAsync()
            => User = await _userRepository.GetUserAsync(User);
    }
}
