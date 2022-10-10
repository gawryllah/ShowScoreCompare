using Microsoft.EntityFrameworkCore;
using ShowScoreCompare.Data;
using ShowScoreCompare.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Secrets.tmdb_api_key = builder.Configuration["tmdb_api_key"];
        Secrets.imdb_api_key = builder.Configuration["imdb_api_key"];

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var provider = builder.Services.BuildServiceProvider();
        var config = provider.GetRequiredService<IConfiguration>();

        builder.Services.AddDbContext<ShowDbContext>(
            item => item.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        builder.Services.AddHttpClient<ITMDB_Service, TmDbService>(s =>
        {
            s.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        });

          builder.Services.AddHttpClient<IIMDB_Service, ImdbService>(s =>
          {
              s.BaseAddress = new Uri("https://imdb-api.com/en/API/");
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();


        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Show}/{action=Index}/{id?}");

        app.Run();
    }
}