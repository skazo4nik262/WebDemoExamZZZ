using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        User06Context context = new User06Context();
        //--------------------------------------------------\\
        [HttpPost("AddUser")]
        public async Task AddUser(User user)
        {
            await DB.Instance.Adduser(user);
        }

        [HttpPost("EditUser")]
        public async Task EditUser(User user)
        {
            await DB.Instance.EditUser(user);
        }

        [HttpPost("DeleteUser")]
        public async Task DeleteUser(User user)
        {
            await DB.Instance.DeleteUser(user);
        }

        [HttpPost("GetAllUser")]
        public async Task<List<User>> GetAllUser()
        {
            return await DB.Instance.GetAllUser();
        }
        //--------------------------------------------------\\
        [HttpPost("GetAllRole")]
        public async Task<List<Role>> GetAllRole()
        {
            return await DB.Instance.GetAllRole();
        }

        [HttpPost("CheckUserInDB")]
        public async Task<bool> CheckUserInDB(string login, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(s => s.Login == login && s.Password == password);
            if (user != null) return true; else return false;
        }

        [HttpPost("CheckUserIsBlocked")]
        public async Task<bool> CheckUserIsBlocked(string login, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(s => s.Login == login && s.Password == password);

            if (DateTime.Now.Month - user.LastVisit.Value.Month > 1 && DateTime.Now.Year == user.LastVisit.Value.Year)
                return true;
            else return false;
        }

        [HttpPost("CheckUserRole")]
        public async Task<bool> CheckUserRole(string login, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(s => s.Login == login && s.Password == password);
            if (user.RoleId == 2) return true; else return false;
        }

        [HttpPost("CheckFirstSign")]
        public async Task<bool> CheckFirstSign(string login, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(s => s.Login == login && s.Password == password);
            if (user.FirstSign) return true; else return false;
        }
        //--------------------------------------------------\\
    }
}
