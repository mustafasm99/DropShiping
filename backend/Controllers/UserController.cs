using backend.Data.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using backend.Model.Entities;
using backend.Model.UsersDtos;
using Microsoft.AspNetCore.Authorization;


namespace backend.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     [Authorize]
     public class UserController : ControllerBase{

          private readonly AppDBContext dbContext;
          public UserController(AppDBContext dbcontext)
          {
               this.dbContext = dbcontext;
          }

          [HttpGet]
          
          public ActionResult <IEnumerable<UserAuth>> GetUsers()
          {
               var alldata = dbContext.UsersAuths.ToList();
               return Ok(alldata);
          }

          [HttpGet("{id}")]
          public async Task<ActionResult<IEnumerable<UserAuth>>> FindUser(Guid id)
          {
               var user = await dbContext.UsersAuths.FindAsync(id);
               if (user  == null){
                    return NotFound();
               }

               return Ok(user);
          }


          [HttpPost]
          [AllowAnonymous]
          public ActionResult <IEmailSender<AddUsersDtos>> NewUser([FromForm]  AddUsersDtos newUser) // dto => data transfare object 
          {
               try {
                     var hashPassword = BCrypt.Net.BCrypt.HashPassword(newUser.password);
               var user = new UserAuth{
                    username = newUser.username,
                    email    = newUser.email,
                    phone_number = newUser.phone_number,

                    password = hashPassword,

                    first_name = newUser.first_name,
                    last_name  = newUser.last_name,

                    last_login = DateTime.UtcNow
               };

               dbContext.UsersAuths.Add(user);
               dbContext.SaveChanges();

               return Ok("successfily created");
               }catch {
                    return BadRequest("the username or the email is already excet ");
               }
              
          }

     
          [HttpPut("{id}")] 
          public async Task<ActionResult<IEnumerable<UserAuth>>> updateUser(Guid id,[FromBody] UpdateUser data)
          {
               var user = await dbContext.UsersAuths.FindAsync(id);
               if (user == null ){
                    return NotFound();
               }

               user.first_name = data.first_name;
               user.last_name  = data.last_name;
               
               await dbContext.SaveChangesAsync();

               return Ok(user);
          }

          [HttpDelete("{id}")]
          public async Task<ActionResult <IEnumerable<UserAuth>>> deleteUser(Guid id)
          {
               if(!Request.Headers.TryGetValue("UserId" , out var SenderId))
               {
                    return BadRequest("User Id header is missing");
               }
               if(!Guid.TryParse(SenderId , out var UserId))
               {
                    return BadRequest("Invalid user id");
               }
               var userToDelete = await dbContext.UsersAuths.FindAsync(UserId);
               if (userToDelete == null){
                    return NotFound();
               }

               dbContext.Remove(userToDelete);
               await dbContext.SaveChangesAsync();

               return Ok();

          }

     }

}