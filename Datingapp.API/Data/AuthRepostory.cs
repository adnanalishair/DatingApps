using System;
using System.Threading.Tasks;
using Datingapp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Datingapp.API.Data
{
    public class AuthRepostory : IAutrepostory
    {
        private readonly DataContext _context;
        public AuthRepostory(DataContext context)
        {
            _context =  context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if(user == null)
            {
                return null;
            }
            if(!verifyPasswordHash(password, user.PasswordHash, user.Passwordslt))
            {
                 return null;
            }
            return user;
        }
        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordslt)
        {
           using(var hmac = new System.Security.Cryptography.HMACSHA512()){

            var computedhash =  hmac.ComputeHash(System.Text.Encoding.UTF32.GetBytes(password));
            int lengthHash = computedhash.Length;
            for (int i = 0; i < lengthHash; i++)
            {
                if(computedhash[i] != passwordHash[i]) return false;

            }
           

        
           }
           return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordhash, passwordsalt;
            CreatePasswordHash(password, out passwordhash, out passwordsalt);
            user.PasswordHash = passwordhash;
            user.Passwordslt = passwordsalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
           using(var hmac = new System.Security.Cryptography.HMACSHA512()){
               passwordsalt =  hmac.Key;
               passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF32.GetBytes(password));
           }
        }

        public Task<bool> UserExist(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}