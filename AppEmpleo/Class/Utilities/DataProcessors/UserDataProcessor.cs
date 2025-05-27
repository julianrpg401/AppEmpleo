using AppEmpleo.Class.Cryptography;
using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.DataProcessors
{
    public class UserDataProcessor
    {
        // Formatea un usuario para ser insertado en la base de datos
        public static Usuario UserFormat(Usuario user)
        {
            Usuario userFormatted = new Usuario()
            {
                Nombre = user.Nombre.ToUpper(),
                Apellido = user.Apellido.ToUpper(),
                Email = user.Email.ToUpper(),
                ClaveHash = EncryptService.HashPassword(user.ClaveHash),
                Rol = user.Rol.ToUpper(),
                FechaRegistro = DateOnly.FromDateTime(DateTime.Now),
                FechaNacimiento = user.FechaNacimiento
            };

            UserTypeFormat(userFormatted);

            return userFormatted;
        }

        // Agrega el rol del usuario
        private static void UserTypeFormat(Usuario user)
        {
            switch (user.Rol)
            {
                case "CANDIDATO":
                    user.Candidato = new Candidato()
                    {
                        UsuarioId = user.UsuarioId
                    };
                    break;
                case "RECLUTADOR":
                    user.Reclutador = new Reclutador()
                    {
                        UsuarioId = user.UsuarioId
                    };
                    break;
            }
        }
    }
}
