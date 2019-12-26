using Microsoft.EntityFrameworkCore;
using MediaPlannerCore.Model.Models;

namespace MediaPlannerCore.Data.Context
{
    public partial class MediaPlannerCoreContext : DbContext
    {
        public MediaPlannerCoreContext()
        {
        }

        public void Commit()
        {
            this.SaveChanges();
        }

        public MediaPlannerCoreContext(DbContextOptions<MediaPlannerCoreContext> options)
            : base(options)
        {
        }
        public  DbSet<Ad> Ad { get; set; }
        public  DbSet<AdSet> AdSet { get; set; }
       
        public  DbSet<Campaign> Campaign { get; set; }
        public  DbSet<Client> Client { get; set; }
        public  DbSet<Country> Country { get; set; }
        public  DbSet<MediaChannel> MediaChannel { get; set; }
        public  DbSet<Supplier> Supplier { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-469D9E0N\\MSSQLSERVER01;Initial Catalog=MediaPlannerDb;Integrated Security=True");
            }
        }
    }
}
