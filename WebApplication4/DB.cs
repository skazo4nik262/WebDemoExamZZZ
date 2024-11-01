using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4
{
    public class DB
    {
        private static DB instance;

        public static DB Instance { get { return instance ??= new DB(); } }

        readonly User06Context context = new User06Context();

        public DB()
        {
            context.Roles.Add(new Role() { RoleName = "Пользователь", });
            context.Roles.Add(new Role() { RoleName = "Администратор", });
            context.Users.Add(new User() { Login = "123", Password = "123", RoleId = 1});
            context.Users.Add(new User() { Login = "321", Password = "321", RoleId = 2});
        }
        //--------------------------------------------------\\
        public async Task Adduser(User user)
        {
            if (user != null)
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            else return;
        }

        public async Task EditUser(User user)
        {
            var localUser = await context.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            if (user != null)
            {
                context.Entry(localUser).CurrentValues.SetValues(user);
                await context.SaveChangesAsync();
            }
            else return;
        }

        public async Task DeleteUser(User user)
        {
            var localUser = await context.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            if (user != null)
            {
                context.Users.Remove(localUser);
                await context.SaveChangesAsync();
            }
            else return;
        }
        public async Task<List<User>> GetAllUser()
        {
            return await context.Users.ToListAsync();
        }
        //--------------------------------------------------\\
        public async Task<List<Role>> GetAllRole()
        {
            return await context.Roles.ToListAsync();
        }
    }
}
