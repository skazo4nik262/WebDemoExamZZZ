using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
        //--------------------------------------------------\\
    }
}
