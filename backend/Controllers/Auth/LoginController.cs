using backend.Data.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.Auth
{
     [Route("api/[controller]")]
     [ApiController]
     [AllowAnonymous]
     public class Login:ControllerBase{
          private readonly AppDBContext dbContext ;
          private readonly IConfiguration conf;
          public Login(AppDBContext dbcontext , IConfiguration conf){
               dbContext = dbcontext;
               this.conf = conf;
          }// connection to the 

          [HttpPost]
          [AllowAnonymous]
          public async Task<ActionResult<IEnumerable<LoginForm>>> LoginUser([FromForm] LoginForm data)
          {
               var user = await dbContext.UsersAuths.FirstOrDefaultAsync(u=>u.username == data.Username);
               if(user == null){return BadRequest("user not found");}
               if (!BCrypt.Net.BCrypt.Verify(data.password , user.password)){
                    return BadRequest("wrong password");
               }
           
               if(User == null){
                    return NotFound("the user name is not found ");
               }
               string token = new AuthHelper(this.conf).GenerateJWTToken(user);
              return Ok(new {token}); 
          } 
     }
}