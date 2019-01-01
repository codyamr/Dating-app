using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserRegsiterDto
    {
        [Required]

        public string UserName { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Sorry Password Must Be 8 digits")]
        public string PassWord { get; set; }
    }
}