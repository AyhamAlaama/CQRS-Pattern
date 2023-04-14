using Post.Domain;
using Post.Domain.IdentityModels.ExtendedUser;

namespace Post.Implementation.DataAccess;
    public class ApplicationDbContext
    :
    IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext
        (DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
       

    public DbSet<Posts>? Posts { get; set; }
    public DbSet<RefreshToken>? RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //AspNetRoles - AspNetUsers - AspNetRoleClaims
        //AspNetUserClaims - AspNetUserLogins - AspNetUserRoles - AspNetUserTokens

        base.OnModelCreating(builder);
        builder.HasDefaultSchema("post");
        builder.Entity<ApplicationUser>().ToTable("Users","Security");
        builder.Entity<IdentityRole>().ToTable("Roles", "Security");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim","Security");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Security");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Security");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Security");
        //builder.Seed();
    }
}