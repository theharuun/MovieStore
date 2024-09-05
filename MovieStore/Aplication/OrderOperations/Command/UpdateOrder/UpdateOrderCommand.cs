using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.OrderOperations.Command.CreateOrder;
using MovieStore.Aplication.OrderOperations.Command.DeleteOrder;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.OrderOperations.Command.UpdateOrder
{
    public class UpdateOrderCommand
    {
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public UpdateOrderModel Model;
        public int OrderId { get; set; }    
        public UpdateOrderCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public void Handle()
        {
            // Mevcut siparişi ve ilişkili verileri al
            var order = _context.Orders
                .Include(x => x.Movies)
                .Include(x => x.Customer)
                .ThenInclude(c => c.purchasedMovies)
                .SingleOrDefault(x => x.OrderID == OrderId);

            if (order == null)
                throw new InvalidOperationException("Bu sipariş bulunamadı");

            var customer = order.Customer;
            if (customer != null)
            {
                foreach (var movie in order.Movies)
                {
                    var purchasedMovie = customer.purchasedMovies
                        .SingleOrDefault(pm => pm.MovieID == movie.MovieID);

                    if (purchasedMovie != null)
                    {
                        customer.purchasedMovies.Remove(purchasedMovie);
                    }
                }
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            // Yeni siparişi oluştur
            var newOrder = _mapper.Map<Order>(Model);
            if (Model.moviesIDs != null && Model.moviesIDs.Any())
            {
                var movies = _context.Movies.Where(m => Model.moviesIDs.Contains(m.MovieID)).ToList();
                newOrder.Movies = movies;

                var newCustomer = _context.Customers.SingleOrDefault(c => c.CustomerID == Model.customerID);
                if (newCustomer != null)
                {
                    // Yeni siparişteki filmleri müşterinin satın aldığı filmler listesine ekle
                    foreach (var movie in movies)
                    {
                        if (!newCustomer.purchasedMovies.Any(pm => pm.MovieID == movie.MovieID))
                        {
                            newCustomer.purchasedMovies.Add(new PurchasedMovie
                            {
                                MovieID = movie.MovieID
                            });
                        }
                    }
                }
            }

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }



    }

    public class UpdateOrderModel
    {
        public DateTime orderDate { get; set; }
        public int customerID { get; set; }
        public List<int> moviesIDs { get; set; }
    }
}
