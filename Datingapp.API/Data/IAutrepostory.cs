using System.Threading.Tasks;
using Datingapp.API.Models;

namespace Datingapp.API.Data

{
    public interface IAutrepostory
    {
         Task<User> Register(User user , string password);
         Task<User> Login (string username, string password);

         Task<bool> UserExist(string username); 

    }
}