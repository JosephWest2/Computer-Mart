using Computer_Mart.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace Computer_Mart.Statics
{
    public static class AuthenticateUser
    {

        public static string CreatePasswordHash(string password)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User u = new User();
            string hashedPassword = passwordHasher.HashPassword(u, password);

            return hashedPassword;
        }

        public static bool CheckPassword(string password, string hashedPassword)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User u = new User();
            var result = passwordHasher.VerifyHashedPassword(u, hashedPassword, password);
            if ((int)result == 1 || (int)result == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
