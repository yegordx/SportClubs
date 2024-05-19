using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace SportClubs1.Data
{
    public static class IdentityWebApplicationExtensions
    {
        private record UserInfo(string Username, string Password)
        {
            public UserInfo() : this(string.Empty, string.Empty)
            {
            }
        }
        private static async Task AddUserIfNotExistsAsync(UserManager<ApplicationUser> userManager
        , ILogger logger
        , string userName
        , string password
        , ICollection<string> roles)
        {
            var applicationUser = await userManager.FindByEmailAsync(userName);
            if (applicationUser is null)
            {
                applicationUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName
                };
                await userManager.CreateAsync(applicationUser, password);

            logger.LogInformation("{username} user added",userName);
            }
            else
            {
                logger.LogInformation("User {username} is already in database", userName);
            }
            var existingRoles = await userManager.GetRolesAsync(applicationUser);
            foreach (var role in roles.Where(role =>
            !existingRoles.Contains(role)))
            {
                await userManager.AddToRoleAsync(applicationUser, role);
                logger.LogInformation("{username} has {rolename} assigned", userName, role);
            }
        }
        public static async Task InitializeRolesAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var roleName in RoleNames.All)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
        }
        public static async Task InitializeDefaultUsersAsync(this WebApplication app
        , IConfiguration? AdminConfiguration
        , IConfiguration? ClientConfiguration
        , IConfiguration? StaffConfiguration)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>> ();
            var AdminInfo = AdminConfiguration.Get<UserInfo>();
            var ClientInfo = ClientConfiguration.Get<UserInfo>();
            var StaffInfo = StaffConfiguration.Get<UserInfo>();
            if (AdminInfo is not null)
            {
                await AddUserIfNotExistsAsync(userManager, app.Logger, AdminInfo.Username, AdminInfo.Password, RoleNames.All);
            }
            if (ClientInfo is not null)
            {
                    await AddUserIfNotExistsAsync(userManager, app.Logger, ClientInfo.Username, ClientInfo.Password, Array.Empty<string>());
            }
            if (StaffInfo is not null)
            {
                    await AddUserIfNotExistsAsync(userManager, app.Logger, StaffInfo.Username, StaffInfo.Password, Array.Empty<string>());
            }
        }
    }
}
