using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hsking.Api.Dto.Dtos;
using Hsking.Api.EfDao.Base;

namespace Hsking.Api.EfDao.Repositories
{
    public class HabitsRepository : GenericRepository<Habit>, IHabitsRepository
    {
        public HabitsRepository(Hsking_dbEntities3 context)
            : base(context)
        {

        }

        public async Task<IEnumerable<HabitDto>> GetCommonHabits()
        {
            return Db.Set<Habit>().Select(x=>new HabitDto()
            {
                Category = new CategoryDto(){Description = x.Category.Description,Name = x.Category.Name},
                Description = x.Description,
                Feature = x.Feature,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Solution = x.Solution,
                Type = new TypeDto() {Description = x.Type.Description,Name = x.Type.Name}
            }).ToList();
        }

   
    }
}
