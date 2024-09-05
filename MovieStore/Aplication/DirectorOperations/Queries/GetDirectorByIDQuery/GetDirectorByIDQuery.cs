using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.MovieOperations.Queries.GetMovieByID;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.DirectorOperations.Queries.GetDirectorByIDQuery
{
    public class GetDirectorByIDQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;
        public int ID;

        public GetDirectorByIDQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public GetDirectorByIDModel Handle()
        {
            var director = _dbContext.Directors
                .Include(x=>x.Movies)
                .Where(x => x.DirectorID == ID)
                .SingleOrDefault();
            if (director == null)
                throw new InvalidOperationException("Director not found - Direktör bulunamadı");

            GetDirectorByIDModel model = mapper.Map<GetDirectorByIDModel>(director);
            return model;
        }
    }

    public class GetDirectorByIDModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; } // Film isimlerini içeren bir liste
    }
}
