//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hsking.Api.EfDao
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserHabitStatus
    {
        public UserHabitStatus()
        {
            this.UserHabits = new HashSet<UserHabit>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<UserHabit> UserHabits { get; set; }
    }
}
