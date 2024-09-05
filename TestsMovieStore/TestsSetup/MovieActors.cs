using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class MovieActors
    {
        public static void AddMovieActors(this MovieStoreDbContext context)
        {
            var movieIds = context.Movies.ToDictionary(m => m.MovieName, m => m.MovieID);
            var actorIds = context.Actors.ToDictionary(a => (a.Name + " " + a.Surname), a => a.ActorID);

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
        }
    }
}
