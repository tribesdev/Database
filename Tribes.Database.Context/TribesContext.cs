using System.Data.Entity;
using Tribes.Database.Context.Entities;

namespace Tribes.Database.Context
{
    public class TribesContext : DbContext, IDbContext
    {
        public virtual DbSet<Clan> Clans { get; set; }
        public virtual DbSet<Tribe> Tribes { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        public TribesContext()
            : base("Tribes")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;

            modelBuilder.Entity<Clan>().HasKey(x => x.Id);
            modelBuilder.Entity<Tribe>().HasKey(x => x.Id);
        }

        public virtual new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
