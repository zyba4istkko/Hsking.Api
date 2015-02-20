using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsking.Api.Dao.Repositories;
using Hsking.Api.Dto.AuthUsers;
using Hsking.Api.EfDao.Base;

namespace Hsking.Api.EfDao.Repositories
{
    public class EfAuthRepository : GenericRepository<User>, IAuthRepository
    {
        private readonly Hsking_dbEntities3 _context;


        public EfAuthRepository(Hsking_dbEntities3 context)
            : base(context)
        {
            _context = context;
        }

        public Task RegisterUser(ApplicationUser appUser)
        {
            var user = Db.Set<User>().Create();
            user.Email = appUser.UserName;
            user.Password = appUser.PasswordHash;
            user.DateRegister = DateTime.Now;
          
            user.Profile = Db.Set<Profile>().Create();
            user.Profile.Id = user.Id;

            base.Insert(user);
            base.Save();
            appUser.Id = user.Id;
            return Task.FromResult(0);
        }

        public Task UpdateUser(ApplicationUser appUser)
        {
            var user = Db.Set<User>().FirstOrDefault(x => x.Id == appUser.Id);
            user.Email = appUser.UserName;
            user.Password = appUser.PasswordHash;
         
            base.Update(user);
            base.Save();

            return Task.FromResult(0);
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            var user = Db.Set<User>().FirstOrDefault(x => x.Email == userName);

            if (user == null)
            {
                return null;
            }

            return new ApplicationUser() { Id = user.Id, PasswordHash = user.Password, UserName = user.Email};
        }




        public async Task<ApplicationUser> FindUser(long id)
        {
            var user = Db.Set<User>().FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return new ApplicationUser() { Id = user.Id, PasswordHash = user.Password, UserName = user.Email };
        }
    }
}
