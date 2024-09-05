using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.ActorOperations.Queries.GetActorByID
{
    public class GetActorByIDQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;
        public int ID;

        public GetActorByIDQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public GetActorByIdModel Handle()
        {
            var actor = _dbContext.Actors.Include(x => x.MovieActors).ThenInclude(ma => ma.Movie).Where(x => x.ActorID == ID).SingleOrDefault();

            if (actor == null)
                throw new InvalidOperationException("Actor not found - Oyuncu bulunamadı");

            GetActorByIdModel model = mapper.Map<GetActorByIdModel>(actor);
            return model;

        }
    }

    public class GetActorByIdModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public List<string> movies { get; set; }
    }

}
