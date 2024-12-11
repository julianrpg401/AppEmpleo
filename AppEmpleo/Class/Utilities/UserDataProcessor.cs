using AppEmpleo.Class.Services;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class.Utilities
{
    public class UserDataProcessor
    {
        // Procesa los datos del usuario para presentarlos en un formato adecuado en la BD
        public static Usuario UserFormat(Usuario user)
        {
            Usuario userFormatted = new Usuario()
            {
                Nombre = user.Nombre.ToUpper(),
                Apellido = user.Apellido.ToUpper(),
                Email = user.Email.ToUpper(),
                ClaveHash = EncryptService.GetSHA256(user.ClaveHash),
                Rol = user.Rol.ToUpper(),
                FechaRegistro = DateOnly.FromDateTime(DateTime.Now),
                FechaNacimiento = user.FechaNacimiento
            };

            UserTypeFormat(userFormatted);

            return userFormatted;
        }

        // Agrega el usuario a la tabla candidato o reclutador según su rol
        public static void UserTypeFormat(Usuario user)
        {
            switch (user.Rol)
            {
                case "CANDIDATO":
                    user.Candidato = new Candidato()
                    {
                        CandidatoId = user.UsuarioId,
                        UsuarioId = user.UsuarioId
                    };
                    break;
                case "RECLUTADOR":
                    user.Reclutador = new Reclutador()
                    {
                        ReclutadorId = user.UsuarioId,
                        UsuarioId = user.UsuarioId
                    };
                    break;
            }
        }
    }
}
