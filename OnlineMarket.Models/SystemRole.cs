using Microsoft.AspNetCore.Identity;

namespace OnlineMarket.Models
{
    public class SystemRole : IdentityRole
    {
         private RoleManager<IdentityRole> roleManager;

        public SystemRole(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager; 
        }

        public void SeedRoles()
        {
            
            
            if (!roleExists("Buyer"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Buyer";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleExists("Seller"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Seller";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
 
 
            if (!roleExists("Moderator"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Moderator";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
 
            if (!roleExists("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
        private bool roleExists(string role){
            return roleManager.RoleExistsAsync(role).Result;
        }
    }
}