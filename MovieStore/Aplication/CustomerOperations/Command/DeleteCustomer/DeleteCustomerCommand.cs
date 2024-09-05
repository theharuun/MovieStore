using MovieStore.DbOperations;

namespace MovieStore.Aplication.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        public readonly IMovieStoreDbContext _context;
        public string email { get; set; }
        public DeleteCustomerCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var movie = _context.Customers.SingleOrDefault(x => x.Email == email);
            if (movie == null)
                throw new InvalidOperationException("Customer not found-Musteri bulunamadi");

            _context.Customers.Remove(movie);
            _context.SaveChanges();

        }
    }
}
