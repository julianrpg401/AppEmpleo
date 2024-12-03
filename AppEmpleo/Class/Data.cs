using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Class
{
    public class Data
    {
        // Procesa los datos para presentarlos en un formato adecuado en la BD
        public static Usuario UserFormat(Usuario usuario)
        {
            Usuario userFormatted = new Usuario()
            {
                Nombre = usuario.Nombre.ToUpper(),
                Apellido = usuario.Apellido.ToUpper(),
                Email = usuario.Email.ToUpper(),
                ClaveHash = Encrypt.GetSHA256(usuario.ClaveHash),
                Rol = usuario.Rol.ToUpper(),
                FechaRegistro = DateOnly.FromDateTime(DateTime.Now),
                FechaNacimiento = usuario.FechaNacimiento
            };

            UserTypeFormat(userFormatted);

            return userFormatted;
        }

        public static void UserTypeFormat(Usuario usuario)
        {
            switch (usuario.Rol)
            {
                case "CANDIDATO":
                    usuario.Candidato = new Candidato()
                    {
                        CandidatoId = usuario.UsuarioId,
                        UsuarioId = usuario.UsuarioId
                    };
                    break;
                case "EMPLEADOR":
                    usuario.Reclutador = new Reclutador()
                    {
                        ReclutadorId = usuario.UsuarioId,
                        UsuarioId = usuario.UsuarioId
                    };
                    break;
            }
        }
    }
}
