using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlazorDemo.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}
