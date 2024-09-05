using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class FavoriteGenres
    {
        public static void AddFavoriteGenres(this MovieStoreDbContext context)
        {
            var genreIds = context.Genres.ToDictionary(g => g.Name, g => g.Id);
            context.FavoriteGenres.AddRange(
                new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Bilim Kurgu, Gerilim / Science Fiction, Thriller "] },
                new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Bilim Kurgu, Drama / Science Fiction, Drama "] },


                new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Suç , Drama / Crime , Drama "] },
                new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Komedi , Drama / Comedy , Drama "] }


            );
        }
    }
}
