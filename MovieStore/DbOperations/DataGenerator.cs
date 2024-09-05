using Microsoft.EntityFrameworkCore;
using MovieStore.Entities;

namespace MovieStore.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                {

                    return;
                }

                context.Genres.AddRange(
                    new Genre()
                    {
                         Name= "Bilim Kurgu, Gerilim / Science Fiction, Thriller "
                    },
                    new Genre()
                    {
                        Name= "Bilim Kurgu, Drama / Science Fiction, Drama "
                    },
                    new Genre()
                    {
                        Name = "Komedi , Drama / Comedy , Drama "
                    },
                    new Genre()
                    {
                        Name = "Suç , Drama / Crime , Drama "
                    }
                    );

                context.Directors.AddRange(
                    new Director()
                    {
                        Name = "Christopher",
                        Surname = "Nolan"

                    },
                    new Director()
                    {
                        Name = "Wes",
                        Surname = "Anderson"
                    });
                context.SaveChanges();//genre ve directors of records were made 


                context.Movies.AddRange(
                    new Movie()
                    {
                        MovieName = "Inception",
                        GenreID = 1,
                        DirectorID = 1,
                        MovieDate = new DateTime(2010, 1, 15),
                        Price = 120
                    },
                      new Movie()
                      {
                          MovieName = "Interstellar",
                          GenreID = 2,
                          DirectorID = 1,
                          MovieDate = new DateTime(2014, 12, 25),
                          Price = 120
                      },
                        new Movie()
                        {
                            MovieName = "The Grand Budapest Hotel",
                            GenreID = 3,
                            DirectorID = 2,
                            MovieDate = new DateTime(2015, 10, 5),
                            Price = 240
                        },
                          new Movie()
                          {
                              MovieName = "The Royal Tenenbaums",
                              GenreID = 5,
                              DirectorID = 2,
                              MovieDate = new DateTime(2001, 11, 20),
                              Price = 300
                          }
                    );
                context.SaveChanges();//movies records were made

                context.Actors.AddRange(
                   new Actor()
                   {
                       Name="Leonardo", Surname="DiCaprio" 
                   },
                     new Actor()
                     {
                         Name = "Joseph",
                         Surname = "Gordon"
                     },
                       new Actor()
                       {
                           Name = "Elliot",
                           Surname = "Page"

                       },
                       new Actor()
                       {
                           Name = "Matthew",
                           Surname = "McConaughey"
                       },
                       new Actor()
                       {
                           Name = "Anne",
                           Surname = "Hathaway"
                       },
                       new Actor()
                       {
                           Name = "Jessica",
                           Surname = "Chastain"

                       },
                       new Actor()
                       {
                           Name = "Ralph",
                           Surname = "Fiennes"

                       },
                       new Actor()
                       {
                           Name = "Murray",
                           Surname = "Abraham"

                       },
                       new Actor()
                       {
                           Name = "Mathieu",
                           Surname = "Amalric"
                       },
                       new Actor()
                       {
                           Name = "Gene",
                           Surname = "Hackman"

                       },
                       new Actor()
                       {
                           Name = "Gwyneth",
                           Surname = "Paltrow"
                       

                       },
                       new Actor()
                       {
                           Name = "Anjelica",
                           Surname = "Huston"

                       }

                   );
                context.SaveChanges();//actors records were made
                                    
                // Movie ve Actor ID'lerini almak için sorgulama yapılır - Query is made to get Movie and Actor IDs
                var movieIds = context.Movies.ToDictionary(m => m.MovieName, m => m.MovieID);
                var actorIds = context.Actors.ToDictionary(a => (a.Name + " " + a.Surname), a => a.ActorID);
                var genreIds = context.Genres.ToDictionary(g => g.Name, g => g.Id);
                
                // Customers
                context.Customers.AddRange(
                    new Customer() { Name = "John", Surname = "Doe" , Email="johndoe@moviestore.com" , Password="john1234" },
                    new Customer() { Name = "Jane", Surname = "Smith" , Email = "janeswitch@moviestore.com" , Password = "jane1234" ,  }
                );
                context.SaveChanges();

                // Orders
                context.Orders.AddRange(
                    new Order() 
                    {
                        OrderDate = new DateTime(2010,5,6),
                        CustomerID = 1 ,
                        Movies = new List<Movie> 
                        {
                            context.Movies.Find(movieIds["Inception"]) ,
                            context.Movies.Find(movieIds["The Grand Budapest Hotel"])

                        }
                    },
                    new Order()
                    {
                        OrderDate = new DateTime(2000,8,12),
                        CustomerID = 2,
                        Movies = new List<Movie>
                        {
                            context.Movies.Find( movieIds["Interstellar"]),
                            context.Movies.Find(movieIds["The Royal Tenenbaums"])
                        }
                    }
                );
                context.SaveChanges();
                // Add PurchasedMovies and FavoriteGenres
                context.PurchasedMovies.AddRange(
                    new PurchasedMovie { CustomerID = 1, MovieID = movieIds["Inception"] },
                    new PurchasedMovie { CustomerID = 1, MovieID = movieIds["The Grand Budapest Hotel"] },

                    new PurchasedMovie { CustomerID = 2, MovieID = movieIds["Interstellar"] },
                    new PurchasedMovie { CustomerID = 2, MovieID = movieIds["The Royal Tenenbaums"] }
                );

                context.FavoriteGenres.AddRange(
                    new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Bilim Kurgu, Gerilim / Science Fiction, Thriller "] },
                    new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Bilim Kurgu, Drama / Science Fiction, Drama "] },


                    new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Suç , Drama / Crime , Drama "] },
                    new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Komedi , Drama / Comedy , Drama "] }


                );

                context.SaveChanges();



                // MovieActor ilişkilerini ekleme -Adding MovieActor relationships
                context.MoviesActors.AddRange(
                    new List<MovieActor>
                    {
                        new MovieActor { MovieID = movieIds["Inception"], ActorID = actorIds["Leonardo DiCaprio"] },
                        new MovieActor { MovieID = movieIds["Inception"], ActorID = actorIds["Joseph Gordon"] },
                        new MovieActor { MovieID = movieIds["Inception"], ActorID = actorIds["Elliot Page"] },

                        new MovieActor { MovieID = movieIds["Interstellar"], ActorID = actorIds["Matthew McConaughey"] },
                        new MovieActor { MovieID = movieIds["Interstellar"], ActorID = actorIds["Anne Hathaway"] },
                        new MovieActor { MovieID = movieIds["Interstellar"], ActorID = actorIds["Jessica Chastain"] },

                        new MovieActor { MovieID = movieIds["The Grand Budapest Hotel"], ActorID = actorIds["Ralph Fiennes"] },
                        new MovieActor { MovieID = movieIds["The Grand Budapest Hotel"], ActorID = actorIds["Murray Abraham"] },
                        new MovieActor { MovieID = movieIds["The Grand Budapest Hotel"], ActorID = actorIds["Mathieu Amalric"] },

                        new MovieActor { MovieID = movieIds["The Royal Tenenbaums"], ActorID = actorIds["Gene Hackman"] },
                        new MovieActor { MovieID = movieIds["The Royal Tenenbaums"], ActorID = actorIds["Gwyneth Paltrow"] },
                        new MovieActor { MovieID = movieIds["The Royal Tenenbaums"], ActorID = actorIds["Anjelica Huston"] }
                    }
                );

                context.SaveChanges(); // MovieActor ilişkileri yapıldı - MovieActor relations were made
            }
        }
    }
}
