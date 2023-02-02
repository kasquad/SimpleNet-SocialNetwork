

using SimpleNet.AppConstants;

namespace SimpleNet.Middlewares;

public class RedirectNonAuthMiddleware : IMiddleware
{
    // Todo: Fix bug when not auth user try open route /user/mypage
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var pathValue = context
            .Request
            .Path
            .Value;
        
        if (!context.User.Identity!.IsAuthenticated)
        {
            if (!pathValue!.Equals(RoutesConstants.LoginRoutePath))
            {
                if (!pathValue.Equals(RoutesConstants.RegisterRoutePath))
                {
                    context.Response.Redirect(RoutesConstants.LoginRoutePath);
                }
            }
            
        }else if (pathValue!.Equals(RoutesConstants.EmptyRoutePath))
        {
            context.Response.Redirect(RoutesConstants.MyPageRoutePath);
        }
        
        await next(context);
    }
}