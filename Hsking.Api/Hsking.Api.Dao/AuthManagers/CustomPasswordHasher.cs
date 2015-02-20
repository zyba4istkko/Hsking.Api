using Microsoft.AspNet.Identity;

namespace Hsking.Api.Dao.AuthManagers
{
    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

     
    }
}
