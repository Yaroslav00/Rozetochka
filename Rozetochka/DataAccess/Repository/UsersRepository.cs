using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public static class UsersRepository
    {
        public static async Task<UserDto> Login(string username, string password)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var user = await dbContext.ShopUsers
                    .Where(u => u.UserName.Equals(username) && u.Password.Equals(password)).Select(u => new UserDto
                    {
                        ID = u.ID,
                        UserName = u.UserName,
                        IsAdmin = u.IsAdmin
                    }).FirstOrDefaultAsync();
                return user;
            }
        }

        public static async Task<UserDto> Register(string username, string password)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (await dbContext.ShopUsers.CountAsync(u => u.UserName == username) > 0)
                {
                    return null;
                }
                var registeredUser = new User
                {
                    IsAdmin = false,
                    Password = password,
                    UserName = username
                };
                dbContext.ShopUsers.Add(registeredUser);
                await dbContext.SaveChangesAsync();
                return new UserDto
                {
                    ID = registeredUser.ID,
                    IsAdmin = registeredUser.IsAdmin,
                    UserName = registeredUser.UserName
                };
            }
        }
    }
}
