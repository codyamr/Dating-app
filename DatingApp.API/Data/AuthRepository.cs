using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthorRepository
    {
      private readonly  DataContext _context ;
        public AuthRepository(DataContext context )
        {
            _context = context;
            
        }
        public Task<NewUser> Login(string username, string PassWord)
        {
            throw new System.NotImplementedException();
        }

        public Task<NewUser> Register(NewUser user, string Password)
        {
            byte[] passwordHash , passwordSalt ;
           CreatePassWordHash( Password, out passwordHash , out passwordSalt);
           user.PasswordHash =passwordHash;
           user.PasswordSalt =passwordSalt;
           _context.NewUsers.AddAsync(user);
           _context.SaveChangesAsync();

            throw new System.NotImplementedException();
        }

        public void CreatePassWordHash(string password, out byte[] passwordHash ,out byte[] passwordSalt)
        {
            using( var hashm = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hashm.Key;
                passwordHash =hashm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
            }

        }

        public Task<NewUser> UserExist(string Name)
        {
            throw new System.NotImplementedException();
        }
    }
}