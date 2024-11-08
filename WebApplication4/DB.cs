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
                context.Users.Entry(localUser).CurrentValues.SetValues(user);
                await context.SaveChangesAsync();
            }
            else return;
        }

        public async Task DeleteUser(User user)
        {
            var localUser = await context.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            localUser.IsBlocked = true;
            if (user != null)
            {
                context.Users.Entry(localUser).CurrentValues.SetValues(user);
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
