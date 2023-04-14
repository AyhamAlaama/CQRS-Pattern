using Post.Application.Interfaces;
using Post.Implementation.ImplementRepositories;

namespace Post.Implementation.Dependencies;
public static class DbContextDependency
{
      
        public static IServiceCollection AddDbServices(this IServiceCollection services,
                                                        IConfiguration configuration)
        {


        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(
                    configuration.GetConnectionString("LightCon"));
                
            });
        services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
        services.AddScoped(typeof(IAuthService),typeof(AuthService));


              return services;
        }
    }
