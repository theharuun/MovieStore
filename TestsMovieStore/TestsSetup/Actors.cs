using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext context)
        {
            context.Actors.AddRange(
                  new Actor()
                  {
                      Name = "Leonardo",
                      Surname = "DiCaprio"
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
        }
    }
}
