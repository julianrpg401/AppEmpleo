using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.Helpers
{
    public static class CountryHelper
    {
        // Diccionario de códigos de país
        public static readonly Dictionary<CountryCode, string> CountryName = new()
        {
            { CountryCode.MEX, "México" },
            { CountryCode.COL, "Colombia" },
            { CountryCode.ARG, "Argentina" },
            { CountryCode.BRA, "Brasil" },
            { CountryCode.PER, "Perú" },
            { CountryCode.CHL, "Chile" },
            { CountryCode.URY, "Uruguay" },
            { CountryCode.DOM, "República Dominicana" },
            { CountryCode.VEN, "Venezuela" },
            { CountryCode.PRY, "Paraguay" },
            { CountryCode.BOL, "Bolivia" },
            { CountryCode.CRI, "Costa Rica" },
            { CountryCode.GTM, "Guatemala" },
            { CountryCode.HND, "Honduras" },
            { CountryCode.PAN, "Panamá" },
            { CountryCode.CUB, "Cuba" },
            { CountryCode.ECU, "Ecuador" },
            { CountryCode.NIC, "Nicaragua" },
            { CountryCode.SLV, "El Salvador" }
        };
    }
}
