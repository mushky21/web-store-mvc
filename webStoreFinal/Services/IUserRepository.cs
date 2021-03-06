﻿using Microsoft.AspNetCore.Identity;
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
        Task<MyUser> FindUserAuthAsync();
        MyUser FindUserAuth();
        Task<MyUser> FindUserById(string userId);
        Task<IdentityResult> AddUser(Register registerData);
        Task<IdentityResult> UpdateUser(Register updateData);
        Task<IdentityResult> UpdatePassword(string password);
        Task<MyUser> FindUserByName(string username);
        Task<Login> RecognizeUser();

        // bool RemoveUser(int userId);//not in the specfication
    }
}
