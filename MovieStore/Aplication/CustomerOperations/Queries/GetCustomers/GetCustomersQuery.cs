using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.MovieOperations.Queries.GetMovies;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;

        public GetCustomersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<GetCustomersModel> Handle() 
        {
            var customers = _dbContext.Customers
          .Include(c => c.purchasedMovies)
          .ThenInclude(pm => pm.Movie)
          .Include(c => c.favoriteGenres)
          .ThenInclude(fg => fg.Genre)
          .OrderBy(x => x.CustomerID)
          .ToList();

            List<GetCustomersModel> list = mapper.Map<List<GetCustomersModel>>(customers);

            return list;

        }

    }

    public class GetCustomersModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }


        // Satın aldığı filmler
        public List<string> purchasedMovies { get; set; } = new List<string>();

        // Favori türler
        public List<string> favoriteGenres { get; set; } = new List<string>();

        /*  bu şekilde yaptığımız zaman movie ve favoritürümüzün bütün icerikleri gelir mappingde islemler arasında sadece ismine göre maple dedim ve yukarıda ki gibi string bir liste ifadesi tuttum 
        // Satın aldığı filmler
        public ICollection<Movie> purchasedMovies { get; set; } = new List<Movie>();

        // Favori türler
        public ICollection<Genre> favoriteGenres { get; set; } = new List<Genre>();
        */
    }
}
