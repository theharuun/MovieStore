using AutoMapper;
using MovieStore.DbOperations;

namespace MovieStore.Aplication.MovieOperations.Command.DeleteMovie
{
    public class DeleteCommandMovie
    {
        public readonly IMovieStoreDbContext _context;
        public int movieId { get; set; }
        public DeleteCommandMovie(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var movie= _context.Movies.SingleOrDefault(x=>x.MovieID==movieId);
            if (movie == null)
                throw new InvalidOperationException("Movie not found-Film bulunamadi");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
