using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.MovieOperations.Queries.GetMovieByID
{
    public class GetMoviesByIDQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;
        public int ID;
        public GetMoviesByIDQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

    
        public GetMoviesByIDModel Handle()
        {
            var movie = _dbContext.Movies
                .Include(x => x.Genre)
                .Include(x => x.Director)
                .Include(x => x.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .Where(x => x.MovieID == ID && x.IsActive)
                .SingleOrDefault();

            if (movie == null)
                throw new InvalidOperationException("Movie not found - Film bulunamadı");

            GetMoviesByIDModel model = mapper.Map<GetMoviesByIDModel>(movie);
            return model;
        }

    }

    public class GetMoviesByIDModel
    {
        public string movieName { get; set; }
        public DateTime movieDate { get; set; }
        public string genre { get; set; }
        public string director { get; set; }
        public List<string> actors { get; set; }
        public int price { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
