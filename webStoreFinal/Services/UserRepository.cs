using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Services
{
    //this class is resoinsible for Modeling all 'CRUD' operations of user manager
    //so, the controllers which contain user's operations will be Well modeled
    public class UserRepository : IUserRepository
    {
        private UserManager<MyUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;//this for FindUserAuthenticated,need http context

        public UserRepository(UserManager<MyUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> AddUser(Register registerData)
        {
            MyUser user = new MyUser
            {
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
                UserName = registerData.Username,
                BirthDate = registerData.BirthDate,
                Email = registerData.Email
            };
            var result = await _userManager.CreateAsync(user, registerData.Password);
            return result;
        }

        public MyUser FindUserAuthenticated()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (userId != null)//more safe
            {
                return _userManager.FindByIdAsync(userId).Result;

            }
            return null;
        }

        public MyUser FindUserById(string userId)
        {
          return _userManager.Users.FirstOrDefault(u => u.Id == userId);
        }

        public Task<IdentityResult> UpdateUser(Update updateData)
        {
            //check if user changed his password
            string password;
            if (updateData.Password != null) password = updateData.Password;
            else password = updateData.CurrentPassword;

            MyUser updatedUser=new MyUser
            {
                FirstName = updateData.FirstName,
                LastName = updateData.LastName,
                BirthDate = updateData.BirthDate,
                Email = updateData.Email,
                PasswordHash = password
            };
            return _userManager.UpdateAsync(updatedUser);

        }
    
        public List<MyUser> Users()
        {
            return _userManager.Users.ToList();
        }
    }
}
