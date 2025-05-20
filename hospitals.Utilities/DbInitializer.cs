using Hospital.Repositories; 
using Hospital.Models;     
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationUser = Hospital.Models.ApplicationUser;

namespace hospitals.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                // check which migrations are still "pending"
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate(); // apply all pending migrations
                }
            }
            catch (Exception)
            {
                throw;
            }
            if(!_roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult())
            {
                // create roles and seed them to the database
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Patient)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Doctor)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Harkesh",
                    Email = "harkesh@xyz.com"
                }, "Harkesh@123").GetAwaiter().GetResult();


                var Appuser = _context.ApplicationUser.FirstOrDefault(x => x.Email == "harkesh@xyz.com");

                if (Appuser != null) { }
            }
          
        }

    }
}
