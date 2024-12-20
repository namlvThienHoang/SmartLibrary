using System;
using System.Collections.Generic;

namespace SmartLibrary.Models.EntityModels
{
    public class Membership
    {
        public int MembershipId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

}