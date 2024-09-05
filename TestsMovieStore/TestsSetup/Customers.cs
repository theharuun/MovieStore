using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public static class Customers
    {
        public static void AddCustomer(this MovieStoreDbContext context)
        {
            // Customers
            context.Customers.AddRange(
                new Customer() { Name = "John", Surname = "Doe", Email = "johndoe@moviestore.com" },
                new Customer() { Name = "Jane", Surname = "Smith", Email = "janeswitch@moviestore.com" }
            );
        }
    }
}
