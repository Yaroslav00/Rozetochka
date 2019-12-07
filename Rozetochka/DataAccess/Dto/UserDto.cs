using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class UserDto
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        public static readonly UserDto ErrorUser = new UserDto
        {
            ID = -1
        };
    }
}
