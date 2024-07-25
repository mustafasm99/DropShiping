using backend.Migrations;
using backend.Model.Entities;
using backend.Model.UsersDtos;
namespace backend.Model
{
     public class CreaetStoer
     {
          public required Guid user {set;get;}
          public required string title {set; get;}
          public required IFormFile Image { get; set;}
          public List<Guid>? CatIDs {get; set;}
     }


     public class UpdateStore
     {
          public required string? title {set; get;}
          public required IFormFile? Image { get; set;}
          public List<Guid>? CatIDs {get; set;}
     }

      public class StoreDto
     {
          public Guid Id {get; set;}
          public string? name {get ; set;}
          public List<CategoryDtoForStore>? Categories { get; set; }
          public UserDto? User {get; set;}
          
     
     }
     public class StoreDtoForCategories{
          public Guid Id {get; set;}
          public string? name {get ; set;}
     }

}   