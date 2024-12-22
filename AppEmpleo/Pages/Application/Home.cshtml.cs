using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Services;
using AppEmpleo.Class.Utilities;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AppEmpleo.Pages.Application
{
    public class HomeModel : PageModel
    {
        private readonly OfferRepository _offerRepository;
        private readonly UserRepository _userRepository;
        private readonly ClaimsService _claimsService;

        public new Usuario User { get; set; }

        public Usuario Recruiter { get; set; }

        [BindProperty]
        public Oferta Offer { get; set; }

        public List<Oferta>? Offers { get; set; } = new List<Oferta>();

        public HomeModel(OfferRepository offerRepository, UserRepository userRepository, ClaimsService claimsService)
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
        {
            Offers = await _offerRepository.GetOffersAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            GetAuthenticatedUser();

            Recruiter = await _userRepository.GetRecruiterAsync(User.UsuarioId);

            Offer.ReclutadorId = Recruiter.Reclutador.ReclutadorId;

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Estado del modelo no válido");
                return Page();
            }

            Offer = OfferDataProcessor.OfferFormat(Offer, Recruiter);
            await _offerRepository.AddOfferAsync(Offer);

            return RedirectToPage();
        }
    }
}
