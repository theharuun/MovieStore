using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class PurchasedMovies
    {
        public static void AddPurchasedMovies(this MovieStoreDbContext context)
        {
            var movieIds = context.Movies.ToDictionary(m => m.MovieName, m => m.MovieID);
            context.PurchasedMovies.AddRange(
                   new PurchasedMovie { CustomerID = 1, MovieID = movieIds["Inception"] },
                   new PurchasedMovie { CustomerID = 1, MovieID = movieIds["The Grand Budapest Hotel"] },

                   new PurchasedMovie { CustomerID = 2, MovieID = movieIds["Interstellar"] },
                   new PurchasedMovie { CustomerID = 2, MovieID = movieIds["The Royal Tenenbaums"] }
               );
        }
    }
}
