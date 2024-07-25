using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class AuthAttribute:ActionFilterAttribute
{
     private readonly string _ValueToCheck ;
     public AuthAttribute( string ValueToCheck = "UserId"){
          Console.WriteLine("Auth Attribute is Working");
          _ValueToCheck = ValueToCheck;
     }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
     if(!context.HttpContext.Request.Headers.TryGetValue(_ValueToCheck , out var headerVar))
         {
              context.Result = new UnauthorizedResult();
         }else{
              Console.WriteLine("sex sex sex");
         }
    }
}


