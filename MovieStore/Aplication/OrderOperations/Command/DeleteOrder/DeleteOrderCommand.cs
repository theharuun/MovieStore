using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.OrderOperations.Command.DeleteOrder
{
    public class DeleteOrderCommand
    {
        public readonly IMovieStoreDbContext _context;
        public int orderId { get; set; }

        public DeleteOrderCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var order = _context.Orders
                .Include(o => o.Movies)
                .Include(o => o.Customer)
                .ThenInclude(c => c.purchasedMovies)
                .SingleOrDefault(o => o.OrderID == orderId);

            if (order == null)
                throw new InvalidOperationException("Order not found - Sipariş bulunamadı");

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
        }
    }

}
