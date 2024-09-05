using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.TestsSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDB").Options;
            Context = new MovieStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddGenre();
            Context.AddDirektors();
            Context.SaveChanges();
            Context.AddMovies();
            Context.SaveChanges();
            Context.AddActors();
            Context.SaveChanges();
            Context.AddCustomer();
            Context.SaveChanges();
            Context.AddOrders();
            Context.SaveChanges();
            Context.AddPurchasedMovies();
            Context.AddFavoriteGenres();
            Context.SaveChanges();
            Context.AddMovieActors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}