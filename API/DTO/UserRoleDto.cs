using System.Collections.Generic;

namespace API.DTO
{
    public class UserRoleDto
    {
        public memberDto user { get; set; }

        public IList<string> role { get; set; }
    }
}