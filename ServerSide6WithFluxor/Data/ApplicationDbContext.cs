using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ServerSide6WithFluxor.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}