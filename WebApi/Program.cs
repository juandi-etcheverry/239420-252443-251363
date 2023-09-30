using ServerFactory;
using WebApi.Filters;
using WebApi.Filters.CreateUser;
using WebApi.Filters.Signup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration.GetConnectionString("DA2DB"));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});
builder.Services.AddScoped<CreateUserAuthenticationFilter>();
builder.Services.AddScoped<SignupAuthenticationFilter>();

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
