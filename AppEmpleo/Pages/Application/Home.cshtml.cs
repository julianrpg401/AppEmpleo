using AppEmpleo.Class;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.LandingPage
{
    public class HomeModel : PageModel
    {
        private readonly OfferRepository _offerRepository;

        [BindProperty]
        public List<Oferta> Ofertas { get; set; } = new List<Oferta>();

        public HomeModel(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async void OnGetAsync()
        {
            Ofertas = await _offerRepository.GetListAsync(Ofertas);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage();
        }
    }
}
