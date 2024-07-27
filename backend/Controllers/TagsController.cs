using backend.Data.DBContext; // using the database manager
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using backend.Model.Entities;
using backend.Migrations;
using Microsoft.EntityFrameworkCore;
using backend.Model;
using Microsoft.AspNetCore.Authorization;



namespace backend.Controllers
{
     [Route("api/[controller]")]
     [ApiController , Authorize(Roles = "Stuff")]
     public class CategoryController : ControllerBase 
     {
          private readonly AppDBContext dBContext;
          public CategoryController(AppDBContext db)
          {
               dBContext = db;
          }

          [HttpGet] // Get all the Tags 
          public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
          {    

               var data = await dBContext.Categorys
               .Include(C=>C.Store)
               .Select(C=> new CategoryDto {
                    Id = C.Id,
                    name = C.name,
                    Store = C.Store == null ? null : new StoreDtoForCategories{
                         Id = C.Store.Id,
                         name = C.Store.title
                    }
               })
               .ToListAsync();
               return  Ok(data);
          }

          [HttpGet("{id}")] // Get Tag by its id
          public async Task<ActionResult<IEnumerable<Category>>> GetById(string id)
          {
               if(!Guid.TryParse(id , out var parsedID))
               {
                    return BadRequest(new {message = "Invalid Input"});
               }

               var tag = await dBContext.Categorys.FindAsync(parsedID);

               if(tag == null){return NotFound();}
               
               return Ok(tag);
          }


          [HttpPost] // Create new Category
          [Authorize]
          public async Task<ActionResult<IEnumerable<CreateNewCategory>>> CreateNewCatogry([FromForm] CreateNewCategory data)
          {
               
               var obj   = new Category{
                    name = data.name
               };
               await dBContext.Categorys.AddAsync(obj);
               await dBContext.SaveChangesAsync();
               return Ok(obj);
          }

          [HttpDelete("{id}")]
          public async Task<ActionResult <IEnumerable<Category>>> DeleteCategory(string id){
               if(Guid.TryParse(id , out var token))
               {
                    var obj = await dBContext.Categorys.FindAsync(token);
                    if(obj != null)
                    {
                         dBContext.Categorys.Remove(obj);
                         await dBContext.SaveChangesAsync();
                         return Ok();
                    }
                    else
                    {return NotFound();}
               }
               return BadRequest();
          }


          [HttpPut("{id}")]
          public async Task<ActionResult<IEnumerable<Category>>> UpdateCategory(string id, [FromForm] UpdateCategory input)
          {
               if (!Guid.TryParse(id, out var categoryId))
                    {
                         return BadRequest(new { message = "Invalid category ID." });
                    }
               var category = await dBContext.Categorys.FindAsync(categoryId);
               if (category == null)
                    {
                         return NotFound(new { message = "Category not found." });
                    }

               if (!string.IsNullOrEmpty(input.name) && input.name.Length <= 100)
                    {
                         category.name = input.name;
                    }
               if (!string.IsNullOrEmpty(input.storeID) && Guid.TryParse(input.storeID, out var storeId))
                    {
                         var store = await dBContext.Stores.FindAsync(storeId);
                         if (store == null)
                         {
                              return BadRequest(new { message = "Store not found." });
                         }

                         category.StoreId = store.Id;
                         category.Store  = store;
                    }
               else if (!string.IsNullOrEmpty(input.storeID))
                    {
                         return BadRequest(new { message = "Invalid store ID." });
                    }

               await dBContext.SaveChangesAsync();
               return Ok(new { message = "Category updated successfully." });
          }

     }

}