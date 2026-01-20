using AppEmpleo.Class.Cryptography;
using AppEmpleo.Models;

namespace AppEmpleo.Class.Utilities.DataProcessors
{
    public class UserDataProcessor
    {
        // Formatea un usuario para ser insertado en la base de datos
        public static UserAccount UserFormat(UserAccount user)
        {
            UserAccount userFormatted = new UserAccount()
            {
                FirstName = CapitalizeFirst(user.FirstName),
                LastName = CapitalizeFirst(user.LastName),
                Email = user.Email.ToUpper(),
                PasswordHash = EncryptService.HashPassword(user.PasswordHash),
                Role = user.Role.ToUpper(),
                RegisterDate = DateOnly.FromDateTime(DateTime.Now),
                BirthDate = user.BirthDate
            };

            UserTypeFormat(userFormatted);

            return userFormatted;
        }

        // Agrega el rol del usuario
        private static void UserTypeFormat(UserAccount user)
        {
            switch (user.Role)
            {
                case "CANDIDATO":
                    user.Candidate = new Candidate()
                    {
                        UserId = user.UserId
                    };
                    break;
                case "RECLUTADOR":
                    user.Recruiter = new Recruiter()
                    {
                        UserId = user.UserId
                    };
                    break;
            }
        }

        // Capitaliza solo la primera letra de un texto
        private static string CapitalizeFirst(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.Trim();
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
    }
}
