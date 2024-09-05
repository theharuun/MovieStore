using MovieStore.DbOperations;

namespace MovieStore.Aplication.DirectorOperations.Command.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public readonly IMovieStoreDbContext _context;
        public int directorId { get; set; }
        public DeleteDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle() 
        {
            var director= _context.Directors.SingleOrDefault(x=>x.DirectorID==directorId);
            if (director == null)
                throw new InvalidOperationException("Director not found - Direktör bulunamadi");

            _context.Directors.Remove(director);
            _context.SaveChanges();

        }

    }

}
