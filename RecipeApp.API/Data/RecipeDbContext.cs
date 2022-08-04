using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.API.Data
{
    public class RecipeDbContext : IdentityDbContext<ApiUser>
    {
        public RecipeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
