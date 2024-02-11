using Microsoft.EntityFrameworkCore;
using WebApplicationTutorial;
using WebApplicationTutorial.Data;
using WebApplicationTutorial.Interfaces;
using WebApplicationTutorial.Repository;

var builder = WebApplication.CreateBuilder(args);

// Tutorial Repo 
/* https://github.com/teddysmithdev/pokemon-review-api */

// Add services to the container.
builder.Services.AddControllers();
// Seed data into Db
builder.Services.AddTransient<Seed>();
// AutoMapper Install "AutoMapper and AutoMapper dependency Injection" 
// in NuGetPM. It allows only specified object be sent back
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Connects to the different Controller
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();  
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DataContext
builder.Services.AddDbContext<DataContext>(options =>
{
// Get Connection string in the appsettings.json
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    // If using Microsoft SQL server
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Run this when using postgres
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//your other scoped code
// await app.RunAsync();

// Makes sure to seed on app start
// In the terminal "dotnet run seedData"
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Console.WriteLine("Seeding data...");
    SeedData(app);
}

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// Open Package Manager Console >> Input "Add-Migration InitialCreate" >> After build succeeded
// >> Input "Update-Database" >>

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
