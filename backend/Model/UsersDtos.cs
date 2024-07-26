namespace backend.Model.UsersDtos
{
     public class AddUsersDtos
     {
          public required string username {get;set;}
          public string? first_name {get;set;}
          public string? last_name {get;set;}
          public required string email {get;set;}
          public DateTime last_login {get; set;}
          public required string phone_number {get; set;} 
          public required string password {get; set;}
          
     }

     public class UpdateUser
     {
          public string? first_name {get;set;}
          public string? last_name {get;set;}
     }

     public class UserDto{
          public Guid Id {get; set;}
          public required string username {get;set;}
          public string? first_name {get;set;}
          public string? last_name {get;set;}
          public required string email {get;set;}
          public DateTime last_login {get; set;}
          public required string phone_number {get; set;} 
     }
}