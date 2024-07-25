using backend.Data.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using backend.Model.Entities;

namespace backend{
     
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class FullAuthAttribute:ActionFilterAttribute
{
     private readonly string _ValueToCheck;

    // constricter to get Checking Attr
    public FullAuthAttribute( string ValueToCheck = "UserId")
     {
          _ValueToCheck = ValueToCheck;
     }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
          
          var db = context.HttpContext.RequestServices.GetService<AppDBContext>();
          if(db != null)
          if(context.HttpContext.Request.Headers.TryGetValue(_ValueToCheck , out var token))
          {
               if(!Guid.TryParse(token , out var AuthedToken))
               {
                    context.Result = new UnauthorizedResult();
               }
               var user = db.UsersAuths.Find(AuthedToken);
               if(user == null || user.is_superUser == false)
               {
                    context.Result = new UnauthorizedResult();
               }
          }else{
               context.Result = new UnauthorizedResult();
          }
    }
}
}
