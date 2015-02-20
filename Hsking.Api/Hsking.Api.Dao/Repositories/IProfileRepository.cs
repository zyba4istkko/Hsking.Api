using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsking.Api.Dto.Dtos;

namespace Hsking.Api.Dao.Repositories
{
    public interface IProfileRepository
    {
        Task<ProfileDto> GetProfile(long userId);
        Task UpdateProfile(ProfileDto profile);
    }
}
