using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelClasses;

namespace AspnetIdentityRoleBasedTutorial.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kamar> Kamar { get; set; }
        public DbSet<KategoriKamar> KategoriKamar{ get; set; }

        public DbSet<PImages> PImages { get; set; }


    }

}