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
        private readonly IProfileRepository _profileRepository;
    


        public AccountController(CustomUserManager userManager, IProfileRepository profileRepository)
        {

            _userManager = userManager;
            _profileRepository = profileRepository;
          
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
                UserName = userModel.Email,
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
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

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return ErrorApiResult(1, "Bad request");
                }
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                return ErrorApiResult(100, errorsMessages);
            }

            return null;
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
        [System.Web.Http.Route("GetProfile")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetProfile()
        {
            var userId = long.Parse(User.Identity.GetUserId());
            var profile = await _profileRepository.GetProfile(userId);
            if (profile == null)
            {
                return ErrorApiResult(12, "Profile not finded");
            }
            else
            {
                return SuccessApiResult(profile);
            }
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("UpdateProfile")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateProfile(ProfileDto profileDto)
        {
            var userId = long.Parse(User.Identity.GetUserId());
            profileDto.Id = userId;
           
            await _profileRepository.UpdateProfile(profileDto);
          

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
            var user = _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ErrorApiResult(101, "User not finded");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Result.Id);

            var callbackUrl = String.Format("{0}/#/recover?email={1}&token={2}", "http://localhost.ru", model.Email, WebUtility.UrlEncode(token));
            await _userManager.SendEmailAsync(user.Result.Id, "RecoverPassword", callbackUrl);
            return EmptyApiResult();
        }

        [System.Web.Http.Route("ResetPassword")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "No user found.");
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }
            IdentityResult result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return EmptyApiResult();
            }
            else
            {
                var errorsMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return ErrorApiResult(1, errorsMessages);
            }

        }




    }
}
