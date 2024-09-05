using AutoMapper;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.MovieOperations.Command.UpdateMovie
{
    public class UpdateCommandMovie
    {
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public UpdateMovieModel Model { get; set; }
        public int ID { get; set; }
        public UpdateCommandMovie(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.MovieID == ID);
            if (movie == null)
                throw new InvalidOperationException("This ID has not movie-Bu ID film yok");

            movie = _mapper.Map<Movie>(Model);
            // Aktörleri ekle
            if (Model.ActorIDs != null && Model.ActorIDs.Any())
            {
                movie.MovieActors = Model.ActorIDs.Select(actorId => new MovieActor
                {
                    ActorID = actorId
                }).ToList();
            }
            
            _context.SaveChanges();
        }
    }

    public class UpdateMovieModel
    {
        public string movieName { get; set; }
        public DateTime movieDate { get; set; }
        public int genreID { get; set; }
        public int directorID { get; set; }
        public int price { get; set; }
        public List<int> ActorIDs { get; set; } // Aktörlerin ID'lerini tutacak liste

    }
}
