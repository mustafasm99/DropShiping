using backend.Data.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.Auth
{
     [Route("api/[controller]")]
     [ApiController]
     [AllowAnonymous]
     public class login:ControllerBase{
          private readonly AppDBContext dbContext ;
          public login(AppDBContext dbcontext){
               dbContext = dbcontext;
          }// connection to the 

          [HttpPost]
          public async Task<ActionResult<IEnumerable<LoginForm>>> LoginUser([FromForm] LoginForm data)
          {
               var User = await dbContext.UsersAuths.FirstOrDefaultAsync(
                    u => u.username == data.Username
               );
               if(User == null){
                    return NotFound("the user name is not found ");
               }
              return Ok($"the user is :{User.username}"); 
          } 
     }
}