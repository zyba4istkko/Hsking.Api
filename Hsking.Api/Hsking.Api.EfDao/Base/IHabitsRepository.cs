using System.Collections.Generic;
using System.Threading.Tasks;
using Hsking.Api.Dto.Dtos;

namespace Hsking.Api.EfDao.Base
{
    public interface IHabitsRepository
    {
        Task<IEnumerable<HabitDto>> GetCommonHabits();
        
    }
}