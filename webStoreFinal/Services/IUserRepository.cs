using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Services
{
    public interface IUserRepository
    {
        List<MyUser> Users();
        Task<MyUser> FindUserAuthenticated();
        Task<MyUser> FindUserById(int userId);
        Task<IdentityResult> AddUser(Register registerData);
        Task<IdentityResult> UpdateUser(Update updateData);

        // bool RemoveUser(int userId);//not in the specfication
    }
}
