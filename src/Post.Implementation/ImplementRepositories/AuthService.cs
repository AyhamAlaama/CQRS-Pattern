using Microsoft.Extensions.Options;
using Post.Application.Features.v1.Roles.Command.AddUserToRole;
using Post.Application.Features.v1.Roles.Command.RemoveUserFromRole;
using Post.Application.Features.v1.Roles.Query;
using Post.Application.Features.v1.Users.Command.RefreshTokens;
using Post.Application.Features.v1.Users.Command;
using Post.Application.Features.v1.Users.Query.FetchClaims;
using Post.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Post.Implementation.ImplementRepositories
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _role;
        private readonly JWT _jwt;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ApplicationDbContext _context;

        public AuthService(UserManager<ApplicationUser> userManager,
                           IOptions<JWT> jwt, RoleManager<IdentityRole> role,
                           TokenValidationParameters tokenValidationParameters,
                           ApplicationDbContext context)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _role = role;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
        }
        public async Task<AuthModel> LoginAsync(LoginUserCommand model)
        {
            var authModel = new AuthModel();
            var user = new ApplicationUser();
            if (model.EmailOrUserName.Contains("@"))
                user = await _userManager.FindByEmailAsync(model.EmailOrUserName);
            else
                user = await _userManager.FindByNameAsync(model.EmailOrUserName);
            if (user is null || !await _userManager.
                CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Invalid Credentials";
                return authModel;
            }
            var jwtModel = await CreateJwtTokenAsync(user);
            

          
            authModel.UserId = user.Id;
            authModel.IsAuthenticated = true;
            authModel.Token = jwtModel.Token;
            authModel.UserName = user.UserName;
            authModel.Email = user.Email;
            authModel.RefreshToken = jwtModel.RefreshToken;

            return authModel;

        }
        public async Task<AuthModel> RegisterAsync(CreateUserCommand model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "UserName is already registered!" };
            var user = new ApplicationUser
            {
                FirstName=model.FirstName,
                LastName =model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var err in result.Errors)
                    errors += $"{err.Description},";
                return  new AuthModel {Message=errors} ;


            }
            await _userManager.AddToRoleAsync(user, "User");

            var jwtModel = await CreateJwtTokenAsync(user);

            return new AuthModel
            {
                UserId = user.Id,
                IsAuthenticated = true,
                Token = jwtModel.Token ,
                UserName = user.UserName,
                Email=user.Email,
                RefreshToken = jwtModel.RefreshToken

            };
        
        }
        public async Task<string> AddUserToRoleAsync(AddUserToRoleCommand model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await _role.RoleExistsAsync(model.RoleName))
                return "Invaild user Id or Role";
            if (await _userManager.IsInRoleAsync(user, model.RoleName))
                return "User already assigned to this role";
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
                return "something wrong";
            return $"User Added to role{model.RoleName}";
           

        }
        public async Task<string> RemoveUserFromRoleAsync(
            RemoveUserRoleCommand model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null)
                return "Invaild user Id or Role";
            if (!await _role.RoleExistsAsync(model.RoleName))
                return "Invaild role name";

            if (!await _userManager.IsInRoleAsync(user, model.RoleName))
                return "User is not assigned to this role";
            var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    return err.Description; 
            }
                
            return $"User Removed From Role : {model.RoleName}";


        }
        public async Task<List<UserRolesDto>> GetUserRoles(string id)
        {
            // Resolve the user via their email
            var list = new List<UserRolesDto>();
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return list;
           

           var roles = await _userManager.GetRolesAsync(user);
            list.Add(new UserRolesDto 
            { 
                UserId = user.Id,
                UserName =  user.UserName,
                Roles = roles.ToList()
            });
 
  
            return list;
        }
        public async Task<string> CreateRole(string name)
        {
         
           var result = await _role.CreateAsync(new IdentityRole(name));
            if (!result.Succeeded)
                return "Invalid name";

            return "Created sccussfully";
        }
        private async Task<AuthModel> CreateJwtTokenAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new ClaimsIdentity(claims:new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)

            });

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials
                (symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {   
                Issuer = _jwt.Issuer,
                Audience =_jwt.Audience,
                Subject = claims,
                Expires = DateTime.UtcNow.Add(_jwt.Expires),
                SigningCredentials = signingCredentials,};
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            var refreshToken = new RefreshToken()
            {
                Token = RandomString(21)+ Guid.NewGuid(),
                JwtId = securityToken.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(7),
             };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthModel 
            { Token=token,IsAuthenticated=true,RefreshToken = refreshToken.Token};
        }
        public async Task<AuthModel> RefreshTokenAsync(RefreshTokenCommand model)
        {
            var validateToken = GetPrincipalsFromToken(model.Token);
            if (validateToken is null) 
                return new AuthModel { Message = "Invalid Token" };
            var expiryDateUnix = long.Parse(
                validateToken.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime
                (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);
            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthModel { Message = "This token has't expired yet" };
            var jti = 
                validateToken.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefrshToken = await _context.RefreshTokens
                .SingleOrDefaultAsync(x => x.Token == model.RefreshToken);

            if (storedRefrshToken is null)
                return new AuthModel { Message = "This refresh token doesn't exist" };
            if (DateTime.UtcNow > storedRefrshToken.ExpiryDate)
                return new AuthModel { Message = "This refresh token has expired" };
            if (storedRefrshToken.IsRevoked)
                return new AuthModel { Message = "This refresh token has been Invalidated" };
            if (storedRefrshToken.IsUsed)
                return new AuthModel { Message = "This refresh token has been Uesd" };
            if (storedRefrshToken.JwtId != jti)
                return new AuthModel { Message = "This refresh token doesn't match JWT" };
            storedRefrshToken.IsUsed = true;
            _context.RefreshTokens.Update(storedRefrshToken);
            await _context.SaveChangesAsync();
            var user = await _userManager.FindByIdAsync
                (validateToken.Claims
                .Single(x => x.Type == "uid").Value);
            return await CreateJwtTokenAsync(user);
        }
        public async Task<bool> InvokeRefreshToken(InvokeRefreshTokenCommand model)
        {
            var refreshToken = await _context.RefreshTokens
                .SingleOrDefaultAsync(x => x.Token == model.RefreshTokenId);
            if (refreshToken is null) return false;
            refreshToken.IsRevoked=true;
            await _context.SaveChangesAsync();
            return true;
        }
        private ClaimsPrincipal GetPrincipalsFromToken(string token)
        {
            var refreshTokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidIssuer = _jwt.Issuer,
                ValidAudience = _jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                                                                        (_jwt.Key)),
                

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token,
                    refreshTokenValidationParams, out var securityToken);
                var tokenValidate = securityToken as JwtSecurityToken;
                if (tokenValidate is null && !tokenValidate.Header.Alg.Equals
                           (SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }
                
                return principal;
            }
            catch{ return null; }
        }
        private bool CheckAlgorithm(SecurityToken validatedToken)
        => (validatedToken is JwtSecurityToken jwtSecurityToken)
                           && jwtSecurityToken.Header.Alg.Equals
                           (SecurityAlgorithms.HmacSha256, 
                            StringComparison.InvariantCultureIgnoreCase);
        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<List<Claim>> GetUserClaims(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var claims = await _userManager.GetClaimsAsync(user);
            return claims.ToList();
        }

 
    }

}
