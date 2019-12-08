using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Dto;
using DataAccess.Repository;

namespace Business.Services
{
    public class UserService: IUserService
    {
        public async Task<UserDto> Login(string username, string password)
        {
            return await UsersRepository.Login(username, password);
        }

        public async Task<UserDto> Register(string username, string password)
        {
            return await UsersRepository.Register(username, password);
        }
    }
}
