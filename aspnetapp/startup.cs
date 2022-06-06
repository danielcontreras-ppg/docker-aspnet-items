using Microsoft.EntityFrameworkCore;
using ItemApi.Models;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration {get;}

    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        //services.AddDbContext<ItemApiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ItemApiConnection")));
        services.AddControllers();
        services.AddCors();
        services.AddMvc();
        services.AddEntityFrameworkNpgsql().AddDbContext<ItemApiContext>(options => 
            options.UseNpgsql(Configuration.GetConnectionString("ItemApiConnection"))
        );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(builder =>
        {
            builder
            .WithOrigins("http://localhost:8080/")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true);
        });

        app.UseMvc(routes => routes.MapRoute(
            "default", "api/{controler}/{action}/{id?}"
        ));
    }
}