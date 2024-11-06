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

        [HttpPost("BlockUser")]
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
        public async Task<bool> CheckUserInDB(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Login) || !string.IsNullOrWhiteSpace(user.Password))
            {
                var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
                if (localuser != null) return true; else return false;
            }
            else return false;
        }


        [HttpPost("CheckUserIsBlocked")]
        public async Task<bool> CheckUserIsBlocked(User user)
        {
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);

            if (DateTime.Now.Month - localuser.LastVisit.Value.Month > 1 && DateTime.Now.Year == user.LastVisit.Value.Year || user.ErrorCount>= 3)
            {
                localuser.IsBlocked = true; await context.SaveChangesAsync(); return false;
            }
            else return true;
        }



        [HttpPost("CheckUserRole")]
        public async Task<bool> CheckUserRole(User user)
        {
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
            if (localuser.RoleId == 2) return true; else return false;
        }

        [HttpPost("CheckFirstSign")]
        public async Task<bool> CheckFirstSign(User user)
        {
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
            if (localuser.FirstSign) return true; else return false;
        }
        //--------------------------------------------------\\
    }
}
