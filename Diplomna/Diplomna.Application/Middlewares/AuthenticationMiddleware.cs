using Diplomna.Common.Auth;

namespace Diplomna.Application.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthConstants _authConstants;

        public AuthenticationMiddleware(RequestDelegate next, AuthConstants authConstants)
        {
            _next = next;
            _authConstants = authConstants;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().Contains("/auth"))
            {
                await _next(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var authResult = Auth.DecodeToken(authHeader.Substring(7), _authConstants.PublicKey);
                if (authResult is null || DateTime.Parse(authResult.Exp) < DateTime.UtcNow)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                context.Items["tutorId"] = authResult.Id;
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }
}
