using AutoMapper;
using MovieStore.Aplication.MovieOperations.Command.UpdateMovie;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.DirectorOperations.Command.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public UpdateDirectorModel Model { get; set; }
        public int ID { get; set; }
        public UpdateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
           var director = _context.Directors.SingleOrDefault(x=>x.DirectorID== ID);
            if (director == null)
                throw new InvalidOperationException("This ID  has not Director - Bu ID sahip directör yok");
            // filmler ekle
            if (Model.moviesIDs != null && Model.moviesIDs.Any())
            {
                // MovieID'leri ile mevcut filmleri veri tabanından alıyoruz
                director.Movies = _context.Movies
                                         .Where(movie => Model.moviesIDs.Contains(movie.MovieID))
                                         .ToList();
            }
            UpdateMoviesIds(Model.moviesIDs, director.Movies);

            _context.SaveChanges();

            
        }

        public void UpdateMoviesIds(List<int> moviesIDs, ICollection<Movie> Movies)
        {
            // Mevcut ID'lerin bir kümesini oluştur
            var existingIds = Movies.Select(pm => pm.MovieID).ToHashSet();

            // Yeni ID'lerin bir kümesini oluştur
            var newIds = new HashSet<int>(moviesIDs);

            // Mevcut listeyi güncelle
            foreach (var id in existingIds)
            {
                if (!newIds.Contains(id))
                {
                    // Mevcut ID listeyi güncellemek istiyorsak, bu ID'yi sil
                    var itemToRemove = Movies.First(pm => pm.MovieID == id);
                    Movies.Remove(itemToRemove);
                }
            }

            // Yeni ID'leri listeye ekle
            var moviesToAdd = _context.Movies.Where(m => newIds.Contains(m.MovieID) && !existingIds.Contains(m.MovieID)).ToList();
            foreach (var movie in moviesToAdd)
            {
                Movies.Add(movie);
            }
        }
    }

    public class UpdateDirectorModel
    {
        public List<int> moviesIDs { get; set; }
    }
}
