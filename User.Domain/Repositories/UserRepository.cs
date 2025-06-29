﻿
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;

namespace User.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataContext _userdatacontext;

        public UserRepository(UserDataContext userdatacontext)
        {
            _userdatacontext = userdatacontext;
        }

        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            return await _userdatacontext.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
        }

    }
}
