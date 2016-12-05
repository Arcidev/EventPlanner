using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.BL.DTO
{
    public class DateAttendDTO
    {
        public string DateString { get; set; }
        
        public bool IsUserAttending { get; set; }
        public override bool Equals(object obj)
        {
            DateAttendDTO fooItem = obj as DateAttendDTO;
            if (fooItem == null)
            {
                return false;
            }
            return fooItem.DateString == this.DateString && fooItem.IsUserAttending == this.IsUserAttending;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.IsUserAttending.GetHashCode();
            hash = (hash * 7) + this.DateString.GetHashCode();
            return hash;
        }
    }
}
