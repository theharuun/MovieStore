using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;

namespace MovieStore.Aplication.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;
        public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<GetMoviesModel> Handle()
        {
            // burada iliskile olan sınıflarımı ekledikten sonra coklu iliskilerimden bir tanesini getircem film actor iliskisinde actoru getirdim 
            var movieList= _dbContext.Movies.Include(x=>x.Genre).Include(x=>x.Director).Include(x=>x.MovieActors).ThenInclude(ma=>ma.Actor).OrderBy(x=>x.MovieName ).ToList();

            List<GetMoviesModel> list= mapper.Map<List<GetMoviesModel>>(movieList);

            return list;
        }
    }
    public class GetMoviesModel
    {
        public string movieName { get; set; }
        public DateTime movieDate { get; set; }
        public string genre { get; set; }
        public string director { get; set; }
   
        public int price { get; set; }
        public List<string> actors { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
