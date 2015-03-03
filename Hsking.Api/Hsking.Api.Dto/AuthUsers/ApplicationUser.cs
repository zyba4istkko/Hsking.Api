using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Hsking.Api.Dto.AuthUsers
{
    public class ApplicationUser : IUser<long>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool ConfirmPhone { get; set; }
    }
}
