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
                FirstName = user.FirstName.ToUpper(),
                LastName = user.LastName.ToUpper(),
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
    }
}
