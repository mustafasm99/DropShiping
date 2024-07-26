using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Model.Entities

{
     [Index(nameof(username)       , IsUnique = true)] // put rouls on field 
     [Index(nameof(phone_number)   , IsUnique = true)] 
     [Index(nameof(email)          , IsUnique = true)] 
     
     public class UserAuth
     {
          public Guid Id {get ; set ;}
          public required string username {get;set;}
          public string? first_name {get;set;}
          public string? last_name {get;set;}
          public required string email {get;set;}
          public DateTime last_login {get; set;}
          public required string phone_number {get; set;}
          public bool is_stuff {get;set;} = false;
          public bool is_superUser {get ; set;} = false;
          public string password { get; set; } = Guid.NewGuid().ToString();
     }

     public class Category
     {
          public Guid Id {set; get;}
          public required string name {set; get;}
          //foreign key to store
          public Guid? StoreId {get; set;}
          public Store? Store {get; set;}
     }



     public class Store 
     {
          [ForeignKey("StoreOwner")]
          public  required UserAuth User { get; set; }
          public Guid Id { get; set; }
          public required string title {get; set;}
          public required string image_url {get ; set;}
          public ICollection<Category> StoreTags {get ; set;} = new List<Category>();
     }

     public class Prodect{
          
          // Create the prodect tabel 
          // Create the connect each prodect with store 
          // store -> to user 
          
          [ForeignKey("ProdectToStore")]
          public required Store store {get; set;}
          public required string name {get ; set;}
          public Guid Id { get; set; }
          public  ICollection<ProdectImages> Images {get ; set;} = new List<ProdectImages>();
          public required float Price { get; set; }
          public DateTime Create { get; set; }
          public DateTime Update { get; set; }
          public bool Is_Active { get; set; } = true ;
          public int Number_of_sell {get; set;} = 0;
          
          
     }

     public class ProdectImages {
          [ForeignKey("ProdectToImage")]
          public required Prodect ProdectId { get; set; }
          public required string image_url {get; set;} 
     }
}