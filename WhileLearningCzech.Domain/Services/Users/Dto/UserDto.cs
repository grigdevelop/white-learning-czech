using WhileLearningCzech.Domain.Core.Abstract;

namespace WhileLearningCzech.Domain.Services.Users.Dto
{
    public class UserDto : IEntityDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
