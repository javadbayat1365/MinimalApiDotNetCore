using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalApiDotNetCore;
using MinimalApiDotNetCore.DTOs;
using MinimalApiDotNetCore.Entities;

var build = WebApplication.CreateBuilder(args);

build.Services.AddDbContext<MinimalApiDbContext>(options => {
    options.UseInMemoryDatabase("UserList");
});
var app = build.Build();


var users = app.MapGroup("/users");

users.MapGet("/list", GetListOfUsers);

users.MapGet("/{Id:int}", GetById);

users.MapPost("/create", CreateUser);

users.MapPut("/{Id:int}", UpdateUser);

users.MapDelete("/{Id:int}", DeleteUser);

app.Run();

static async Task<IResult> GetListOfUsers(MinimalApiDbContext context)
{
    var users = await context.Users.Select(s => new UserDto(s)).ToListAsync();
    if (users is not null)
    return TypedResults.Ok(users);
    return TypedResults.NoContent();
}

static async Task<IResult> GetById(int Id,MinimalApiDbContext context)
{
    var user = await context.Users.Where(w => w.Id == Id).Select(s => new UserDto(s)).SingleOrDefaultAsync();
    if (user is not null)
    return TypedResults.Ok(user);
    return TypedResults.NotFound();
}

static async Task<IResult> CreateUser(UserDto userDto,MinimalApiDbContext context)
{
    await context.Users.AddAsync(
        new User()
        {
            Name = userDto.Name,
            IsComplete = userDto.IsComplete,
        });
    await context.SaveChangesAsync();
    return Results.Ok("User Added");
}


static async Task<IResult> UpdateUser(int Id,UserDto userDto,MinimalApiDbContext context)
{
    var UserUpdate = context.Users.Where(w => w.Id.Equals(Id)).SingleOrDefault();
    UserUpdate.Name = userDto.Name;
    UserUpdate.IsComplete = userDto.IsComplete;
    await context.SaveChangesAsync();
    return Results.Ok("User Updated");
}

static async Task<IResult> DeleteUser(int Id,MinimalApiDbContext context)
{
    var ExistUser = await context.Users.Where(w => w.Id.Equals(Id)).SingleOrDefaultAsync();
    if (ExistUser is null)
    {
        return Results.NotFound("Not Found User");
    }
    context.Users.Remove(ExistUser);
    await context.SaveChangesAsync();
    return Results.Ok();
}