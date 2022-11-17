using Microsoft.EntityFrameworkCore;
using PockemonReviewApp.Models;

namespace PockemonReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Reviewer>  Reviewers { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //links this to id together
            // this is for many-to-many relation 
            builder.Entity<PokemonCategory>()
               .HasKey(x => new { x.PokemonId, x.CategoryId });
            builder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(fc => fc.PokemonId);

            builder.Entity<PokemonCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(fc => fc.CategoryId);


            builder.Entity<PokemonOwner>()
               .HasKey(x => new { x.PokemonId, x.OwnerId });
            builder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(fc => fc.PokemonId);

            builder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(fc => fc.OwnerId);


        }
    }
}
