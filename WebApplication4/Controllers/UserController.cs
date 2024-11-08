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
            if (user != null)
            {
                if (await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.RoleId == user.RoleId) == null)
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                }
            }
            else return;
        }

        [HttpPost("ChangePassword")]
        public async Task<string> ChangePassword(ChangePasswordModel ChangePasswordModel)
        {
            var localUser = await context.Users.FirstOrDefaultAsync(s => s.Password == ChangePasswordModel.CurrentPassword);
            if (localUser != null)
            {
                if (ChangePasswordModel.NewPassword == ChangePasswordModel.NewNewPassword)
                {
                    context.Users.Entry(localUser).CurrentValues.SetValues(ChangePasswordModel.NewPassword);
                    return "Пароль успешно изменён";
                }
                else return "Новые пароли различаются";
            }
            else return "Текущий пароль введен неверно";
        }

        [HttpPost("EditUser")]
        public async Task EditUser(User user)
        {
            var localUser = await context.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            if (localUser != null)
            {
                if (user.IsBlocked == false)
                {
                    localUser.IsBlocked = false;
                    localUser.ErrorCount = 0;
                }
                context.Users.Entry(localUser).CurrentValues.SetValues(user);
                await context.SaveChangesAsync();
            }
            else return;
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
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login);
            if (localuser != null)
            {
                if (DateTime.Now.Month - localuser.LastVisit.Value.Month >= 1 && DateTime.Now.Year == user.LastVisit.Value.Year || user.ErrorCount >= 3 || localuser.IsBlocked == true)
                {
                    localuser.IsBlocked = true; await context.SaveChangesAsync(); return false;
                }
                else if (localuser.Password != user.Password)
                {
                    localuser.ErrorCount++;
                    if (localuser.ErrorCount >= 3)
                    {
                        //если че добавить localuser.ErrorCount = 0;
                        localuser.IsBlocked = true;
                        await context.SaveChangesAsync();
                        return false;
                    }
                    await context.SaveChangesAsync(); return true;
                }
                else return true;
            }
            return true;
        }

        [HttpPost("LastVisitChange")]
        public async Task LastVisitChange(UserModel user)
        {
            var localuser = await context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Password);
            if (localuser != null)
                localuser.LastVisit = user.LastVisit;
            await context.SaveChangesAsync();
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
