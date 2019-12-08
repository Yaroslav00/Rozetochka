using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                return user?? UserDto.ErrorUser;
            }
        }

        public static async Task<UserDto> Register(string username, string password)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (await dbContext.ShopUsers.CountAsync(u => u.UserName == username) > 0)
                {
                    return UserDto.ErrorUser;
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

        public static async Task ChangeUserCredentials(int userId, string username, string password)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var user = await dbContext.ShopUsers.FirstOrDefaultAsync(u => u.ID.Equals(userId));

                if (user != null)
                {
                    user.UserName = username;
                    user.Password = password;

                    dbContext.ShopUsers.AddOrUpdate(user);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
