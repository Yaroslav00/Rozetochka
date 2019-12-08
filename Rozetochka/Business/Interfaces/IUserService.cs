using System.Threading.Tasks;
using DataAccess.Dto;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Login(string username, string password);

        Task<UserDto> Register(string username, string password);

        Task ChangeUserCredentials(int userId, string username, string password);
    }
}
