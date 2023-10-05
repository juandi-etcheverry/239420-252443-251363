using ServerFactory;
using WebApi.Filters;
using WebApi.Filters.Login;
using WebApi.Filters.Logout;
using WebApi.Filters.Products;
using WebApi.Filters.Purchase;
using WebApi.Filters.Signup;
using WebApi.Filters.User;
using WebApi.Filters.User.Admin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration.GetConnectionString("DA2DB"));
builder.Services.AddControllers(options => { options.Filters.Add<CustomExceptionFilter>(); });
builder.Services.AddScoped<UpdateUserAuthenticationFilter>();
builder.Services.AddScoped<AdminUserAuthenticationFilter>();
builder.Services.AddScoped<SignupAuthenticationFilter>();
builder.Services.AddScoped<LoginAuthenticationFilter>();
builder.Services.AddScoped<IsLoggedInAuthenticationFilter>();
builder.Services.AddScoped<ProductAuthenticationFilter>();
builder.Services.AddScoped<GetPurchaseHistoryFilter>();
builder.Services.AddScoped<AddPurchaseFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();