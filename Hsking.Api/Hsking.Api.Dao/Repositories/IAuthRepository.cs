using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsking.Api.Dto.AuthUsers;

namespace Hsking.Api.Dao.Repositories
{
    public interface IAuthRepository : IDisposable
    {
        Task RegisterUser(ApplicationUser createUserDto);

        Task<ApplicationUser> FindUser(string userName, string password);
        Task<ApplicationUser> FindUser(long id);

        Task UpdateUser(ApplicationUser appUser);
    }
}
