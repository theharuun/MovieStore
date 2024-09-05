using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.OrderOperations.Queries.GetOrderByID
{
    public class GetOrderByIDQuery
    {
        public int ID;
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper mapper;

        public GetOrderByIDQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public GetOrderByIDModel Handle()
        {
            var order = _dbContext.Orders
                .Include(x => x.Movies)
                .Include(x => x.Customer)
                 .Where(x => x.OrderID == ID)
                .SingleOrDefault();
            if (order == null)
                throw new InvalidOperationException("Order not found - Sipariş bulunamadi");

            GetOrderByIDModel model = mapper.Map<GetOrderByIDModel>(order);
            return model;

        }
    
    }

   public class GetOrderByIDModel {
        public DateTime OrderDate { get; set; }
        public string customer { get; set; }
        public List<string> Movies { get; set; } // Film isimlerini içeren bir liste
    }
}
