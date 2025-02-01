using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assnet8.Data.Configuration;

namespace assnet8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new FieldConfiguration());
            modelBuilder.ApplyConfiguration(new GalleryConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new EntryConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipConfiguration());
            modelBuilder.ApplyConfiguration(new MunicipalityConfiguration());
            modelBuilder.ApplyConfiguration(new ListingConfiguration());


            // Configure the TagType enum to be stored as a string
            modelBuilder.Entity<Tag>()
                .Property(t => t.Type)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string when saving to the database
                    v => (TagType)Enum.Parse(typeof(TagType), v) // Convert string back to enum when reading from the database
                );
            // Configure the ListingType enum to be stored as a string
            modelBuilder.Entity<Listing>()
                .Property(l => l.Type)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string when saving to the database
                    v => (ListingType)Enum.Parse(typeof(ListingType), v) // Convert string back to enum when reading from the database
                );

            // Configure the ListingCondition enum to be stored as a string
            modelBuilder.Entity<Listing>()
                .Property(l => l.Condition)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string when saving to the database
                    v => (ListingCondition)Enum.Parse(typeof(ListingCondition), v) // Convert string back to enum when reading from the database
                );

            // Configure the ListingStatus enum to be stored as a string
            modelBuilder.Entity<Listing>()
                .Property(l => l.Status)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string when saving to the database
                    v => (ListingStatus)Enum.Parse(typeof(ListingStatus), v) // Convert string back to enum when reading from the database
                );
            // Configure the TeamRole enum to be stored as a string
            modelBuilder.Entity<Membership>()
                .Property(m => m.Role) // Assuming you have a Role property in Membership
                .HasConversion(
                    v => v.ToString(), // Convert enum to string when saving to the database
                    v => (TeamRole)Enum.Parse(typeof(TeamRole), v) // Convert string back to enum when reading from the database
                );


        }
    }
}