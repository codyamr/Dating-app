using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models
{
    public class NewUser
    {
         [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}