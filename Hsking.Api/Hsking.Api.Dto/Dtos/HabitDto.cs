using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsking.Api.Dto.Dtos
{
    public class HabitDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Solution { get; set; }
        public string Feature { get; set; }
        public string ImageUrl { get; set; }
        public string PushText { get; set; }
        public int NumberOfReminder { get; set; }

        public CategoryDto Category { get; set; }

        public TypeDto Type { get; set; }

        public List<DefaultSheduleDto> DefaultShedules { get; set; } 
    }
}
