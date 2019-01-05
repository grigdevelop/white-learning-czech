using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Data;
using WhileLearningCzech.Domain.Services.Users.Dto;
using WhileLearningCzech.Domain.Mapper;

namespace WhileLearningCzech.Domain.Services.Users
{
    public class UserService : IUserService
    {
        private readonly LearningDbContext _db;

        public UserService(LearningDbContext db)
        {
            _db = db;
        }

        public async Task<UserDto> GetUser(string username, string passwordHash)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username
                           && u.PasswordHash == passwordHash);


            return user.ToEntityDto<UserDto, User>();
        }
    }
}
