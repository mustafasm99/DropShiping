namespace backend.Model
{
     public class CreateNewCategory
     {
          public required string name {get; set;}
     }

     public class UpdateCategory{
          public string? name{get; set;}
          public string? storeID {get; set;}
     };
     public class CategoryDto
     {
          public Guid Id {get ; set;}
          public string? name {get; set;}
          public StoreDtoForCategories? Store {get; set;}
     }

     public class CategoryDtoForStore{
           public Guid Id {get ; set;}
          public string? name {get; set;}
     }
}