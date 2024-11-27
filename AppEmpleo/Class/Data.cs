﻿using AppEmpleo.Models;
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
                FechaRegistro = DateTime.Now,
                FechaNacimiento = usuario.FechaNacimiento
            };

            return userFormatted;
        }
    }
}