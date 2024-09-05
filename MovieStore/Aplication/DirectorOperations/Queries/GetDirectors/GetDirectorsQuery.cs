using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;

        public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<GetDirectorsModel> Handle() 
        {
            var directors=_dbContext.Directors
                .Include(x=>x.Movies)
                .OrderBy(x=>x.DirectorID).ToList();

            List<GetDirectorsModel>  list=mapper.Map<List<GetDirectorsModel>>(directors);

            return list;
        }
    }
    public class GetDirectorsModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; } // Film isimlerini içeren bir liste
    }
}
