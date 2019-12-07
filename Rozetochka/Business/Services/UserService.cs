using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Dto;

namespace Business.Services
{
    public class UserService: IUserService
    {
        public async Task<UserDto> Login(string username, string password)
        {

        }
        public async Task<UserDto> Register(string username, string password)
        {

        }
    }
}
