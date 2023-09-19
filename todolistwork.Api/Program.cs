using todolistwork.Infrastructure;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader()
                                                  .AllowAnyMethod().AllowCredentials();

        });
});
/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
  {
      options.Cookie.Name = "token";
      options.SlidingExpiration = true;
      options.ExpireTimeSpan = new TimeSpan(1, 0, 0); // Expires in 1 hour
      options.Events.OnRedirectToLogin = (context) =>
      {
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          return Task.CompletedTask;
      };
      options.Cookie.HttpOnly = true;
      // Only use this when the sites are on different domains
      options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
  });*/
// Add services to the container.
builder.Services.RegisterServices();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        // Đọc thông tin Authentication:Google từ appsettings.json
        // IConfigurationSection googleAuthNSection = Microsoft.Extensions.Configuration.GetSection("Authentication:Google");

        // Thiết lập ClientID và ClientSecret để truy cập API google
        googleOptions.ClientId = "798177398700-446lvhovve9kk9vkq2f0hk4e39a0n0iq.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)

    });
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    // Cấu hình các tùy chọn khác cho cookie
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();


app.Run();
