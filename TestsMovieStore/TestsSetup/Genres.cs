using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class Genres
    {
        public static void AddGenre(this MovieStoreDbContext context)
        {
            context.Genres.AddRange(
                  new Genre()
                  {
                      Name = "Bilim Kurgu, Gerilim / Science Fiction, Thriller "
                  },
                  new Genre()
                  {
                      Name = "Bilim Kurgu, Drama / Science Fiction, Drama "
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
        }
    }
}
