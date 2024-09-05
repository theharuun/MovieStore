using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class Directors
    {
     
            public static void AddDirektors(this MovieStoreDbContext context)
            {
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
        }
        
    }
}
