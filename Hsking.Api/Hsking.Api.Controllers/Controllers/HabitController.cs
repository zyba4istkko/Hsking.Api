using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Hsking.Api.Controllers.ApiResults;
using Hsking.Api.EfDao.Base;

namespace Hsking.Api.Controllers.Controllers
{
    [System.Web.Http.RoutePrefix("api/Habits")]
    public class HabitController:CustomApiController
    {
         
        private readonly IHabitsRepository _habitsRepository;

        public HabitController(IHabitsRepository habitsRepository)
        {
            _habitsRepository = habitsRepository;
        }

        //[System.Web.Http.Authorize]
        //[System.Web.Http.Route("GetCountries")]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("GetHabits")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetHabits()
        {
            var habits = await _habitsRepository.GetCommonHabits();
            return SuccessApiResult(habits);
        }
    }
}
