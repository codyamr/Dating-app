using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthorRepository
    {
         Task<NewUser> Register (NewUser user ,string PassWord);
         Task<NewUser> Login (string username ,string PassWord);
         Task<bool> UserExist (string Name);

    }
}