using Sample.Domain.Database;
using Sample.Business.Services;
using Sample.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Sample.Database;

/* 
 * Configure services (InMemory Database, repository, services, etc...)
 */
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SampleDbContext>(opt => opt.UseInMemoryDatabase("SampleInMemoryDb"));
var dbContext = builder.Services.BuildServiceProvider().GetService<SampleDbContext>();
if (dbContext is null)
{
    throw new InvalidOperationException("Unable to configure app with Database");
}
DbInitializer.Initialize(dbContext);
builder.Services.AddScoped<ISampleDbService, SampleDbService>(s => new SampleDbService(dbContext));
builder.Services.AddScoped<ISampleRepository, SampleRepository>(s => new SampleRepository(dbContext));
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<IProductService, ProductService>();

/*
 * Configure web host app
 */
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html");;

app.Run();
