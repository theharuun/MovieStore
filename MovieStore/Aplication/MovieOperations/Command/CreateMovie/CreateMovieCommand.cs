using AutoMapper;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.MovieOperations.Command.CreateMovie
{
    public class CreateMovieCommand
    {

        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public CreateMovieModel Model { get; set; }
        public CreateMovieCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x => x.MovieName == Model.movieName);
            if (movie != null)
                throw new InvalidOperationException("There is already this movie-Bu film zaten var");

            movie = _mapper.Map<Movie>(Model);
            // Aktörleri ekle
            if (Model.ActorIDs != null && Model.ActorIDs.Any())
            {
                movie.MovieActors = Model.ActorIDs.Select(actorId => new MovieActor
                {
                    ActorID = actorId
                }).ToList();
            }
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }
    }

    public class CreateMovieModel
    {
        public string movieName { get; set; }
        public DateTime movieDate { get; set; }
        public int genreID { get; set; }
        public int directorID { get; set; }
        public int price { get; set; }
        public List<int> ActorIDs { get; set; } // Aktörlerin ID'lerini tutacak liste
                                                // Aktif/Pasif durumu
        public bool IsActive { get; set; } = true;


    }
}
