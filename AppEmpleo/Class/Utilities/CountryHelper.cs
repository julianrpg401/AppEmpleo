using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities
{
    public static class CountryHelper
    {
        public static readonly Dictionary<Country, string> CountryName = new()
        {
            { Country.MEX, "México" },
            { Country.COL, "Colombia" },
            { Country.ARG, "Argentina" },
            { Country.BRA, "Brasil" },
            { Country.PER, "Perú" },
            { Country.CHL, "Chile" },
            { Country.URY, "Uruguay" },
            { Country.DOM, "República Dominicana" },
            { Country.VEN, "Venezuela" },
            { Country.PRY, "Paraguay" },
            { Country.BOL, "Bolivia" },
            { Country.CRI, "Costa Rica" },
            { Country.GTM, "Guatemala" },
            { Country.HND, "Honduras" },
            { Country.PAN, "Panamá" },
            { Country.CUB, "Cuba" },
            { Country.ECU, "Ecuador" },
            { Country.NIC, "Nicaragua" },
            { Country.SLV, "El Salvador" }
        };
    }
}
