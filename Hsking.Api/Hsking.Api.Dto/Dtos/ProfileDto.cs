using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hsking.Api.Dto.Dtos
{
    public class ProfileDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string AboutMe { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public bool HideBirthday { get; set; }
        public string Gender { get; set; }


        public static  string GetGenderStringFromBool(bool gender)
        {
            if (gender) return "male";
            return "female";
        }
    }
}
