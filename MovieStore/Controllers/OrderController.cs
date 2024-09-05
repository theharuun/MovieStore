using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Aplication.DirectorOperations.Command.CreateDirector;
using MovieStore.Aplication.DirectorOperations.Command.DeleteDirector;
using MovieStore.Aplication.DirectorOperations.Command.UpdateDirector;
using MovieStore.Aplication.DirectorOperations.Queries.GetDirectorByIDQuery;
using MovieStore.Aplication.DirectorOperations.Queries.GetDirectors;
using MovieStore.Aplication.OrderOperations.Command.CreateOrder;
using MovieStore.Aplication.OrderOperations.Command.DeleteOrder;
using MovieStore.Aplication.OrderOperations.Command.UpdateOrder;
using MovieStore.Aplication.OrderOperations.Queries.GetOrderByID;
using MovieStore.Aplication.OrderOperations.Queries.GetOrders;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            GetOrdersQuery query = new GetOrdersQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetActorByID(int id)
        {
            GetOrderByIDQuery query = new GetOrderByIDQuery(_dbContext, _mapper);
            query.ID = id;
            GetOrderByIDQueryValidator validator = new GetOrderByIDQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }


        [Authorize]
        [HttpPost]
        public IActionResult AddDirectr([FromBody] CreateOrderModel model)
        {
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            command.Model = model;
            CreateOrderCommandValidator validationRules = new CreateOrderCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{OrderId}")]
        public IActionResult DeleteBook(int OrderId)
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);

            command.orderId = OrderId;
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromBody] UpdateOrderModel model, int id)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.OrderId = id;
            command.Model = model;
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
