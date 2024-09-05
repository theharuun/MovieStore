using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.DirectorOperations.Queries.GetDirectors;
using MovieStore.DbOperations;

namespace MovieStore.Aplication.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;

        public GetOrdersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<GetOrdersModel> Handle()
        {
            var orders = _dbContext.Orders.Include(x => x.Movies).Include(x=>x.Customer).OrderBy(x => x.OrderID).ToList();

            List<GetOrdersModel> list = mapper.Map<List<GetOrdersModel>>(orders);

            return list;
        }
    }

    public class GetOrdersModel
    {
        public DateTime OrderDate { get; set; }
        public string customer{ get; set; }
        public List<string> Movies { get; set; } // Film isimlerini içeren bir liste
    }
}
