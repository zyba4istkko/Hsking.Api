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
    
    public partial class UserHabit
    {
        public long Id { get; set; }
        public long HabitId { get; set; }
        public long UserId { get; set; }
        public int StatusId { get; set; }
        public Nullable<int> СoefficientMinHabit { get; set; }
    
        public virtual Habit Habit { get; set; }
        public virtual UserHabitStatus UserHabitStatus { get; set; }
        public virtual User User { get; set; }
    }
}