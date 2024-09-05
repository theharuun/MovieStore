using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class Orders
    {
        public static void AddOrders(this MovieStoreDbContext context)
        {
            var movieIds = context.Movies.ToDictionary(m => m.MovieName, m => m.MovieID);
            context.Orders.AddRange(
                   new Order()
                   {
                       OrderDate = new DateTime(2010, 5, 6),
                       CustomerID = 1,
                       Movies = new List<Movie>
                       {
                            context.Movies.Find(movieIds["Inception"]) ,
                            context.Movies.Find(movieIds["The Grand Budapest Hotel"])

                       }
                   },
                   new Order()
                   {
                       OrderDate = new DateTime(2000, 8, 12),
                       CustomerID = 2,
                       Movies = new List<Movie>
                       {
                            context.Movies.Find( movieIds["Interstellar"]),
                            context.Movies.Find(movieIds["The Royal Tenenbaums"])
                       }
                   }
               );
        }
    }
}
