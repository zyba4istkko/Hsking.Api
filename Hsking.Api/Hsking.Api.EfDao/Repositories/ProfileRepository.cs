using System;

using System.Linq;

using System.Threading.Tasks;
using Hsking.Api.Dao.Repositories;
using Hsking.Api.Dto.Dtos;
using Hsking.Api.EfDao.Base;

namespace Hsking.Api.EfDao.Repositories
{
    public class ProfileRepository : GenericRepository<Profile>,IProfileRepository
    {
        public ProfileRepository(Hsking_dbEntities3 context)
            : base(context)
        {

        }

        public async Task<ProfileDto> GetProfile(long userId)
        {
            var profile = Db.Set<Profile>().FirstOrDefault(x => x.Id == userId);

            if (profile == null)
            {
                return null;
            }
            return new ProfileDto()
            {
                AboutMe = profile.AboutMe,
                AvatarUrl = profile.AvatarUrl,
                Birthday = profile.Birthday,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                HideBirthday = profile.HideBirthday,
                Gender = ProfileDto.GetGenderStringFromBool(profile.Gender)
            };
        }

        public Task UpdateProfile(ProfileDto profile)
        {
            var findedProfile = Db.Set<Profile>().FirstOrDefault(x => x.Id == profile.Id);
            if (findedProfile == null)
            {
                return null;
            }

            findedProfile.AboutMe = profile.AboutMe;
            findedProfile.AvatarUrl = profile.AvatarUrl;
            findedProfile.Birthday = profile.Birthday;
            findedProfile.Gender = ParseGenderStr(profile.Gender);
            findedProfile.FirstName = profile.FirstName;
            findedProfile.LastName = profile.LastName;
            findedProfile.HideBirthday = profile.HideBirthday;
            base.Update(findedProfile);
            base.Save();
            return Task.FromResult(0);
        }

        private bool ParseGenderStr(string gender)
        {
            if (gender.ToLower() == "male")
            {
                return true;
            }
            return false;
        }

        private string GetGenderStringFromBool(bool gender)
        {
            if (gender) return "male";
            return "female";
        }
    }
}
