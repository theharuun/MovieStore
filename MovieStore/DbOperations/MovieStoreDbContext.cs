using Microsoft.EntityFrameworkCore;
using MovieStore.Entities;

namespace MovieStore.DbOperations
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
        public DbSet<FavoriteGenre> FavoriteGenres { get; set; } // Yeni eklenen DbSet
        public DbSet<PurchasedMovie> PurchasedMovies { get; set; } // Yeni eklenen DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Movie entity
            modelBuilder.Entity<Movie>()
                .HasKey(m => m.MovieID);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

            // Actor entity
            modelBuilder.Entity<Actor>()
                .HasKey(a => a.ActorID);

            // Customer entity
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerID);

            // Director entity
            modelBuilder.Entity<Director>()
                .HasKey(d => d.DirectorID);

            // Genre entity
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.Id);

            // Order entity
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderID);

            // MovieActor entity
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieID, ma.ActorID });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieID);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorID);

            // FavoriteGenre entity - Yeni yapılandırma
            modelBuilder.Entity<FavoriteGenre>()
                .HasKey(fg => new { fg.CustomerID, fg.GenreID }); // Çoktan çoğa ilişki için anahtar

            modelBuilder.Entity<FavoriteGenre>()
                .HasOne(fg => fg.Customer)
                .WithMany(c => c.favoriteGenres)
                .HasForeignKey(fg => fg.CustomerID); // Customer ile ilişki

            modelBuilder.Entity<FavoriteGenre>()
                .HasOne(fg => fg.Genre)
                .WithMany()
                .HasForeignKey(fg => fg.GenreID); // Genre ile ilişki

            // PurchasedMovie entity - Yeni yapılandırma
            modelBuilder.Entity<PurchasedMovie>()
                .HasKey(pm => new { pm.CustomerID, pm.MovieID }); // Çoktan çoğa ilişki için anahtar

            modelBuilder.Entity<PurchasedMovie>()
                .HasOne(pm => pm.Customer)
                .WithMany(c => c.purchasedMovies)
                .HasForeignKey(pm => pm.CustomerID); // Customer ile ilişki

            modelBuilder.Entity<PurchasedMovie>()
                .HasOne(pm => pm.Movie)
                .WithMany()
                .HasForeignKey(pm => pm.MovieID); // Movie ile ilişki
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
