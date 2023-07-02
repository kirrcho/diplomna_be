using Diplomna.Application.Configuration;
using Diplomna.Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddContext(config);

builder.Services.AddCommonClasses(config);

builder.Services.AddServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddAntiforgery(options =>
{
    options.SuppressXFrameOptionsHeader = true;
});

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
    await next();
});

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseHsts();

app.UseAuthorization();

app.MapControllers();

app.Run();
