using System.Collections.Generic;
using API.Entities;

namespace API.helpers
{
    public class GetUserObject
    {
        public   IEnumerable<AppUser> results { get; set; }

        public int count { get; set; }
    }
}