using Application.Contracts.Repos;
using Domain.Entity.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new();
            var usersEntites = await context.Users.ToListAsync();

            foreach (var user in usersEntites)
            {
                User userToAdd = new User
                {
                    Id = user.Id,
                    Name = user.Name
                };

                users.Add(userToAdd);
            }

            return users;
        }
    }
}
