using Microsoft.EntityFrameworkCore; // invock the Entity framework to handel the data base create and build 
using backend.Model.Entities;        // invock Entit


namespace backend.Data.DBContext
{
     public class AppDBContext : DbContext
     {
          public AppDBContext(DbContextOptions options) : base(options)
          {
               
          }
        public DbSet<UserAuth> UsersAuths { get; set; }
          public DbSet<Store> Stores {get; set;}
          public DbSet<Category> Categorys {get; set ;}
          
     }
}