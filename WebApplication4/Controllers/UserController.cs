using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        User06Context context;

        public UserController(User06Context context)
        {
            this.context = context;
        }
        //--------------------------------------------------\\
        [HttpPost("AddUser")]
        public async Task AddUser(UserModel user)
        {
            await DB.Instance.Adduser((User)user);
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
        public async Task<bool> CheckUserInDB(UserModel user)
        {
            if (!string.IsNullOrWhiteSpace(user.Login) || !string.IsNullOrWhiteSpace(user.Password))
            {
                var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
                if (localuser != null) return true; else return false;
            }
            else return false;
        }


        [HttpPost("CheckUserIsBlocked")]
        public async Task<bool> CheckUserIsBlocked(UserModel user)
        {
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
            if (localuser != null)
                if (DateTime.Now.Month - localuser.LastVisit.Value.Month > 1 && DateTime.Now.Year == user.LastVisit.Value.Year || user.ErrorCount >= 3)
                {
                    localuser.IsBlocked = true; await context.SaveChangesAsync(); return false;
                }
                else return true;
            return true;
        }



        [HttpPost("CheckUserRole")]
        public async Task<bool> CheckUserRole(UserModel user)
        {
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
            if (localuser != null)
                if (localuser.RoleId == 2)
                    return true;
            return false;
        }

        [HttpPost("CheckFirstSign")]
        public async Task<bool> CheckFirstSign(UserModel user)
        {
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
            if (localuser.FirstSign) { localuser.FirstSign = false; await context.SaveChangesAsync(); return true; } else return false;
        }
        //--------------------------------------------------\\
    }
}
