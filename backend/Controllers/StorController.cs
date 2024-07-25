using backend.Data.DBContext;           // for connecting to the database
using Microsoft.AspNetCore.Identity;    // Identity -> for handel the database events 
using Microsoft.AspNetCore.Mvc;         // mange the requests 
using backend.Model.Entities;
using backend.Model;
using Microsoft.EntityFrameworkCore;           // backend models 
using backend.Model.UsersDtos;

namespace backend.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class StoreController : ControllerBase 
     {
          private readonly AppDBContext dBContext; // => database connection
          public StoreController(AppDBContext dbcontext)
          {
               // constracuter to connect the class to the database 
               this.dBContext = dbcontext;
          }


          // function To Creaet the endpoints
          [HttpGet]
          public ActionResult <IEnumerable <Store>> GetStors()
          {
               var alldata = dBContext.Stores
               .Include(store => store.User)
               .Include(store => store.StoreTags)
               .Select(store => new StoreDto{
                    Id = store.Id,
                    name = store.title,
                    User = store.User == null ? null : new UserDto
                    {
                         username = store.User.username,
                         phone_number = store.User.phone_number,
                         Id = store.User.Id,
                         email = store.User.email
                    },
                    Categories = store.StoreTags.Select(tag => new CategoryDtoForStore{
                         Id = tag.Id,
                         name = tag.name  
                    }).ToList()
               })
               .ToList();
               return Ok(alldata);
          }       

          [HttpGet("{id}")]
          public async Task<ActionResult <IEnumerable <Store>>> GetStoreById(Guid id)
          {
               var obj = await dBContext.Stores.FindAsync(id);
               if (obj == null)
               {
                    return NotFound();
               }

               return Ok(obj);
          }

          [HttpPost]
          public async Task<ActionResult<IEnumerable<CreaetStoer>>> CreateStore([FromQuery] CreaetStoer NStore)
          {
               var filename = $"{Guid.NewGuid()}{Path.GetExtension(NStore.Image.FileName)}";
               var filepaht = Path.Combine("Images/Store" , filename);

               Directory.CreateDirectory(Path.GetDirectoryName(filepaht));

               //save the file 
               using(var stream = new FileStream(filepaht , FileMode.Create))
               {
                    await NStore.Image.CopyToAsync(stream);
               }

               var categories = await dBContext.Categorys
                                .Where(c=> NStore.CatIDs.Contains(c.Id))
                                .ToListAsync();

               var user = dBContext.UsersAuths.Find(NStore.user);
               if (user == null){
                    return NotFound();
               }
               var obj = new Store{
                    User = user,
                    title = NStore.title,
                    image_url = $"/Images/Store/{filename}",
                    StoreTags = categories
               };

               dBContext.Stores.Add(obj);
               await dBContext.SaveChangesAsync();


               return Ok(obj);
          }

          [HttpDelete("{id}")]
          public async Task<ActionResult<IEnumerable<Store>>> DeleteStore(Guid id)
          {
               
               var store = await  dBContext.Stores.FindAsync(id);
               Request.Headers.TryGetValue("UserId" , out var userIDFromHeader);
               foreach(var h in Request.Headers)
               {
                    Console.WriteLine(h);
               }
               // to check if the user is excest 
               if(!Guid.TryParse(userIDFromHeader , out var parsedID)){ return Unauthorized();}
               var user = await dBContext.UsersAuths.FindAsync(parsedID);
               Console.WriteLine(parsedID);
               if (user == null){return NotFound();}
               if(store == null){return NotFound();}
               
               if(user.Id == store.User.Id){
                    dBContext.Stores.Remove(store);
                    dBContext.SaveChanges();
               }
               
               return Ok();
          }


          [HttpPut("{id}")]
          public async Task<ActionResult<IEnumerable<Store>>> UpdateStore(Guid id , [FromForm] UpdateStore data)
          {

               string ImageName; 
               string ImagePath;

               var oldStore = await dBContext.Stores.FindAsync(id);
               if (oldStore == null){return NotFound();}
               oldStore.title = data.title;
               if (data.Image != null)
               {
                    ImageName = $"{Guid.NewGuid()}{Path.GetExtension(data.Image.FileName)}";
                    ImagePath = $"Images/Store/{ImageName}";
                    using (var stream = new FileStream(ImagePath , FileMode.Create)) {
                         await data.Image.CopyToAsync(stream);
                    }
                    string oldImagePath = $"{oldStore.image_url}";
                    Console.WriteLine(oldImagePath , ImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                         System.IO.File.Delete(oldImagePath);
                    }

                    oldStore.image_url = ImagePath;
               }
               
               await dBContext.SaveChangesAsync();
               return Ok();
          }
     }


}
