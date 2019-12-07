using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DataAccess
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


        public bool IsAdmin { get; set; }
    }
}
