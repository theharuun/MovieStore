using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System.IO;

namespace MovieStore.Aplication.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommand
    {
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public CreateOrderModel Model { get; set; }
        public CreateOrderCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            // Sipariş veritabanında zaten var mı kontrol edin
            var order = _context.Orders
                .Include(x => x.Movies)
                .Include(x => x.Customer)
                .SingleOrDefault(x => x.CustomerID == Model.customerID && x.OrderDate == Model.orderDate);

            if (order != null)
                throw new InvalidOperationException($"Bu Siparişi bu müsteri zaten verdi");

            // Yeni siparişi haritadan oluşturun
            order = _mapper.Map<Order>(Model);

            if (Model.moviesIDs != null && Model.moviesIDs.Any())
            {
                // Film ID'lerine göre veritabanından filmleri alın
                var movies = _context.Movies
                                     .Where(movie => Model.moviesIDs.Contains(movie.MovieID))
                                     .ToList();

                // Filmleri siparişe ekleyin
                order.Movies = movies;

                // Müşteriyi veritabanından alın
                var customer = _context.Customers
                                       .Include(c => c.purchasedMovies)
                                       .SingleOrDefault(c => c.CustomerID == Model.customerID);

                if (customer == null)
                    throw new InvalidOperationException("Müşteri bulunamadı");

                // Satın alınan filmleri müşterinin listesine ekleyin
                foreach (var movie in movies)
                {
                    // Aynı müşteri ve film için mevcut bir kayıt olup olmadığını kontrol edin
                    if (!customer.purchasedMovies.Any(pm => pm.MovieID == movie.MovieID))
                    {
                        customer.purchasedMovies.Add(new PurchasedMovie
                        {
                            MovieID = movie.MovieID
                        });
                    }
                }
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }


    }

    public class CreateOrderModel
    {
      
        public DateTime orderDate { get; set; }
        public int customerID { get; set; }
        public List<int> moviesIDs { get; set; }
    }

}
