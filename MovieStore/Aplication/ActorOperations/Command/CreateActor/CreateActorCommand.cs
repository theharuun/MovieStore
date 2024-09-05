using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.ActorOperations.Command.CreateActor
{
    public class CreateActorCommand
    {
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;

        public CreateActorModel Model { get; set; }
        public CreateActorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor =  _context.Actors.Include(x => x.MovieActors).ThenInclude(ma => ma.Movie).SingleOrDefault(x=>x.Name==Model.name && x.Surname==Model.surname);

            if (actor != null)
                throw new InvalidOperationException("There is already this actor-Bu oyuncu zaten var");
            actor = _mapper.Map<Actor>(Model);
            // Aktörleri ekle
            if (Model.moviesIDs != null && Model.moviesIDs.Any())
            {
                actor.MovieActors = Model.moviesIDs.Select(movieId => new MovieActor
                {
                     MovieID = movieId
                }).ToList();
            }
            _context.Actors.Add(actor);
            _context.SaveChanges();

        }
    }

    public class CreateActorModel
    {

        public string name { get; set; }
        public string surname { get; set; }
        public List<int> moviesIDs { get; set; }
    }
}
