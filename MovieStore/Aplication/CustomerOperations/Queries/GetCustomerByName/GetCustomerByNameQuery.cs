using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.CustomerOperations.Queries.GetCustomers;
using MovieStore.DbOperations;

namespace MovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName
{
    public class GetCustomerByNameQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;
        public string email;

        public GetCustomerByNameQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }
        public GetCustomerByNameModel Handle()
        {
            var customer = _dbContext.Customers
          .Include(c => c.purchasedMovies)
          .ThenInclude(pm => pm.Movie)
          .Include(c => c.favoriteGenres)
          .ThenInclude(fg => fg.Genre)
          .Where(x => x.Email == email)
          .SingleOrDefault();

            if ( customer == null)
                throw new InvalidOperationException("Musteri BULUNAMADI!!!--Customer NOT FOUND!!!");


            return mapper.Map<GetCustomerByNameModel>(customer);

        }
    }
    public class GetCustomerByNameModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }


        // Satın aldığı filmler
        public List<string> purchasedMovies { get; set; } = new List<string>();

        // Favori türler
        public List<string> favoriteGenres { get; set; } = new List<string>();

    }
}
