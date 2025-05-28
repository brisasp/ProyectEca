using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DesignAPI.Models.Entity;
using RestAPI.Models.Entity;

namespace DesignAPI.Data
{
    public class TimeSpanConverter : IdentityDbContext<AppUser>
    {
        public TimeSpanConverter(DbContextOptions<TimeSpanConverter> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //Add models here
        public DbSet<ReservaEntity> Reservas { get; set; }
        public DbSet<DiaNoLectivoEntity> DiasNoLectivos { get; set; }
        public DbSet<FranjaHorarioEntity> FranjasHorarios { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }


    }
}
