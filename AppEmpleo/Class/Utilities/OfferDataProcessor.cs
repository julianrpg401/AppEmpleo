using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities
{
    public class OfferDataProcessor
    {
        public static Oferta OfferFormat(Oferta offer, Usuario user)
        {
            Oferta offerFormatted = new Oferta()
            {
                ReclutadorId = user.Reclutador?.ReclutadorId,
                NombreOferta = offer.NombreOferta.ToUpper(),
                FechaInicio = offer.FechaInicio,
                FechaCierre = offer.FechaCierre,
                Pais = offer.Pais.ToUpper(),
                Moneda = offer.Moneda.ToUpper(),
                Salario = offer.Salario,
                Descripcion = offer.Descripcion.ToUpper(),
                ModalidadTrabajo = offer.ModalidadTrabajo.ToUpper()
            };

            return offerFormatted;
        }
    }
}
