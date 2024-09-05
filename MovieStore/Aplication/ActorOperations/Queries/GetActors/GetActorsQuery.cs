using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;

namespace MovieStore.Aplication.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;
        public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }


        public List<GetActorsModel> Handle()
        {
            var actors =_dbContext.Actors.Include(x => x.MovieActors).ThenInclude(ma => ma.Movie).OrderBy(x => x.Name).ToList();
            List<GetActorsModel> returnlist= mapper.Map<List<GetActorsModel>>(actors);
            return returnlist;
        }

    }
    public class GetActorsModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public List<string>  movies { get; set; }
    }
}
