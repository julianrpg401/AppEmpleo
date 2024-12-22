using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities
{
    public class OfferDataProcessor
    {
        public static Oferta OfferFormat(Oferta offer, Usuario recruiter)
        {
            Oferta offerFormatted = new Oferta()
            {
                ReclutadorId = recruiter.Reclutador.ReclutadorId,
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
