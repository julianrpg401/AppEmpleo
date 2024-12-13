using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities
{
    public static class CurrencyHelper
    {
        public static readonly Dictionary<CurrencyType, string> CurrencyName = new()
        {
            { CurrencyType.USD, "Dólar Estadounidense" },
            { CurrencyType.MXN, "Peso Mexicano" },
            { CurrencyType.COP, "Peso Colombiano" },
            { CurrencyType.ARS, "Peso Argentino" },
            { CurrencyType.BRL, "Real Brasileño" },
            { CurrencyType.PEN, "Sol Peruano" },
            { CurrencyType.CLP, "Peso Chileno" },
            { CurrencyType.UYU, "Peso Uruguayo" },
            { CurrencyType.DOP, "Peso Dominicano" },
            { CurrencyType.VES, "Bolívar Venezolano" },
            { CurrencyType.PYG, "Guaraní Paraguayo" },
            { CurrencyType.BOB, "Boliviano" },
            { CurrencyType.CRC, "Colón Costarricense" },
            { CurrencyType.GTQ, "Quetzal Guatemalteco" },
            { CurrencyType.HNL, "Lempira Hondureño" },
            { CurrencyType.PAB, "Balboa Panameño" }
        };
    }
}
