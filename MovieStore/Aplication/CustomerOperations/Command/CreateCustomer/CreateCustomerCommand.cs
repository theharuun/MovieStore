using AutoMapper;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.CustomerOperations.Command.CreateCustomer
{
    public class CreateCustomerCommand
    {

        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;

        public CreateCustomerModel Model { get; set; }
        public CreateCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Email==Model.email);
            if (customer != null)
                throw new InvalidOperationException("Bu emaile sahip musteri var- there is a customer with same email");

            customer=_mapper.Map<Customer>(Model);

            // Satin aldigi filmleri ekle
            if (Model.purchasedMoviesIDs != null && Model.purchasedMoviesIDs.Any())
            {
                customer.purchasedMovies = Model.purchasedMoviesIDs.Select(purchasedMovieId => new PurchasedMovie
                {
                         MovieID = purchasedMovieId
                }).ToList();
            }
            // favori türleri ekle 
            if (Model.favoriteGenresIDs != null && Model.favoriteGenresIDs.Any())
            {
                customer.favoriteGenres= Model.favoriteGenresIDs.Select(favGenreId => new FavoriteGenre
                {
                     GenreID = favGenreId
                }).ToList();
            }
            // RefreshToken'ı burada ayarlayın
            customer.RefreshToken = "initial_refresh_token";  // Geçici bir değer atayabilirsiniz
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

    }

    public class CreateCustomerModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email  { get; set; }

        // Satın aldığı filmler
        public List<int> purchasedMoviesIDs { get; set; } // Movie ID'lerini tutacak liste

        // Favori türler
        public List<int> favoriteGenresIDs { get; set; } // favori genre ID'lerini tutacak liste

        public string password { get; set; }
        public DateTime publishDate { get; set; }
    }
}
