using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Hsking.Api.Controllers.ApiResults;
using Hsking.Api.Controllers.Models;
using Hsking.Api.Dao.AuthManagers;
using Hsking.Api.Dao.Repositories;
using Hsking.Api.Dto.AuthUsers;
using Hsking.Api.Dto.Dtos;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace Hsking.Api.Controllers.Controllers
{
    [System.Web.Http.RoutePrefix("api/Account")]
   
    public class AccountController : CustomApiController
    {
        private readonly CustomUserManager _userManager;

        public AccountController(CustomUserManager userManager)
        {
            _userManager = userManager;
        }

        // POST api/Account/Register
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("Register")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }


            var user = new ApplicationUser()
            {
                UserName = userModel.Phone,
            };
            var password = await _userManager.GeneratePassword();
            
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                try
                {
                    await _userManager.SendSmsAsync(user.Id, password);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                return EmptyApiResult();
            }
            else
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                if (!errorsMessages.Any()) return ErrorApiResult(12, "User exist");
                return ErrorApiResult(1, errorsMessages);
            }

        }

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("GetOrders")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetOrders()
        {
            return EmptyApiResult();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }
            var name = User.Identity.GetUserName();
            if (name == null)
            {
                return ErrorApiResult(100, "User not exists");
            }
            var userId = long.Parse(User.Identity.GetUserId());
            var result = await _userManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return EmptyApiResult();
            }
            else
            {
                return ErrorApiResult(2, result.Errors);
            }

        }

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("Test")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Test()
        {
            var userId = long.Parse(User.Identity.GetUserId());
            await _userManager.SendSmsAsync(userId, "hallo");
            return EmptyApiResult();
        }

        [System.Web.Http.Route("RecoverPassword")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> RecoverPassword(RecoverPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }
         
            var user =await _userManager.FindByNameAsync(model.Phone);
            await _userManager.RemovePasswordAsync(user.Id);
            var newPassword = await _userManager.GeneratePassword();
            await _userManager.AddPasswordAsync(user.Id, newPassword);
            await _userManager.SendSmsAsync(user.Id, newPassword);
            return EmptyApiResult();
        }

    }
}
