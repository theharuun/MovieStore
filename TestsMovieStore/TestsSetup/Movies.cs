using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context)
        {
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
        }
    }
}
