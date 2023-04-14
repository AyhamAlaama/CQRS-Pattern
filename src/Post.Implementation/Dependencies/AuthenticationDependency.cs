namespace Post.Implementation.Dependencies;
public static class AuthenticationDependency
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.Configure<JWT>(configuration.GetSection("JWT"));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
            (configuration["JWT:Key"])),
            ClockSkew=TimeSpan.Zero,
           
        };
     
        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(
                   opt =>
                   {
                       opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                   }).AddCookie(O =>
                   {
                       O.Cookie.Name = "token";
                   }).AddJwtBearer(
                   o =>
                   {
                       
                       o.SaveToken = true;
                       o.TokenValidationParameters = tokenValidationParameters;
                       //o.Events = new JwtBearerEvents
                       //{
                       //    OnMessageReceived = context =>
                       //    {
                               
                       //        context.Token = context.Request.Cookies["token"];
                       //        return Task.CompletedTask;
                       //    }
                       //};

                   });
        return services;
        }
    }
