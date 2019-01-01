using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthorRepository
    {
      private readonly  DataContext _context ;
        public AuthRepository(DataContext context )
        {
            _context = context;
            
        }
        public async Task<NewUser> Login(string username, string password)
        {
            var user = await _context.NewUsers.FirstOrDefaultAsync(u=> u.Name == username);
            if(user == null) return null;

            if(!VerfingPassword(password,user.PasswordHash , user.PasswordSalt))
            return null;

            return user;

        }

       private bool VerfingPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using( var hashm = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               
               var computedHash = hashm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

               for(int i =0 ; i < computedHash.Length ; i ++)

               if(computedHash[i] !=passwordHash[i]) return false;
               
            }
            return true;
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

        public async Task<bool> UserExist(string name)
        {
            var user = await _context.NewUsers.AnyAsync(d=> d.Name == name);
            if(user) return true;
            
            return false;
        }
    }
}