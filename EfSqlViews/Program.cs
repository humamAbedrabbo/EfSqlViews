using EfSqlViews.ApplicationCore.Domain.Abstractions;
using EfSqlViews.ApplicationCore.Domain.Features.Sites;
using EfSqlViews.ApplicationCore.Features.Sites;
using EfSqlViews.Infrastructure;
using EfSqlViews.Infrastructure.Repositories;
using EfSqlViews.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure services to the container.
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.AddScoped<ICurrentUser, CurrentUserService>();
builder.Services.AddScoped<ISiteFactory, SiteFactory>();
builder.Services.AddScoped(typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddHostedService<DataSeedService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", async ([FromServices] IRepository<SiteListView> repo) =>
{
    return await repo.ListAsync();
})
.WithName("GetSites");

app.Run();

