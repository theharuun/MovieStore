using AutoMapper;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.ActorOperations.Command.UpdateActor
{
    public class UpdateActorCommand
    {
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public UpdateActorModel Model { get; set; }
        public int actorId { get; set; }
        public UpdateActorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x => x.ActorID == actorId);
            if (actor == null)
                throw new InvalidOperationException("This ID  has not Actor - Bu ID oyuncu yok");

            // Aktörleri ekle
            if (Model.moviesIDs != null && Model.moviesIDs.Any())
            {
                actor.MovieActors = Model.moviesIDs.Select(movieId => new MovieActor
                {
                    MovieID = movieId
                }).ToList();
            }
            UpdateMoviesIds(Model.moviesIDs,actor.MovieActors);
            _context.SaveChanges();

        }
        public void UpdateMoviesIds(List<int> moviesIDs, ICollection<MovieActor> MovieActors)
        {
            // Mevcut ID'lerin bir kümesini oluştur
            var existingIds = MovieActors.Select(pm => pm.MovieID).ToHashSet();

            // Yeni ID'lerin bir kümesini oluştur
            var newIds = new HashSet<int>(moviesIDs);

            // Mevcut listeyi güncelle
            foreach (var id in existingIds)
            {
                if (!newIds.Contains(id))
                {
                    // Mevcut ID listeyi güncellemek istiyorsak, bu ID'yi sil
                    var itemToRemove = MovieActors.First(pm => pm.MovieID == id);
                    MovieActors.Remove(itemToRemove);
                }
            }

            // Yeni ID'leri listeye ekle
            foreach (var id in newIds)
            {
                if (!existingIds.Contains(id))
                {
                    MovieActors.Add(new MovieActor{  MovieID= id });
                }
            }
        }
    }



    public class UpdateActorModel
    {
        public List<int> moviesIDs { get; set; }
    }
}
