using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.Helpers
{
    public static class CurrencyHelper
    {
        // Diccionario de nombres de monedas
        public static readonly Dictionary<CurrencyCode, string> CurrencyName = new()
        {
            { CurrencyCode.USD, "Dólar Estadounidense" },
            { CurrencyCode.MXN, "Peso Mexicano" },
            { CurrencyCode.COP, "Peso Colombiano" },
            { CurrencyCode.ARS, "Peso Argentino" },
            { CurrencyCode.BRL, "Real Brasileño" },
            { CurrencyCode.PEN, "Sol Peruano" },
            { CurrencyCode.CLP, "Peso Chileno" },
            { CurrencyCode.UYU, "Peso Uruguayo" },
            { CurrencyCode.DOP, "Peso Dominicano" },
            { CurrencyCode.VES, "Bolívar Venezolano" },
            { CurrencyCode.PYG, "Guaraní Paraguayo" },
            { CurrencyCode.BOB, "Boliviano" },
            { CurrencyCode.CRC, "Colón Costarricense" },
            { CurrencyCode.GTQ, "Quetzal Guatemalteco" },
            { CurrencyCode.HNL, "Lempira Hondureño" },
            { CurrencyCode.PAB, "Balboa Panameño" }
        };
    }
}
