using System.Threading.Tasks;
using WhileLearningCzech.Domain.Services.Users.Dto;

namespace WhileLearningCzech.Domain.Services.Users
{
    public interface IUserService
    {
        Task<UserDto> GetUser(string username, string passwordHash);
    }
}
