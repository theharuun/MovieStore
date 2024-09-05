using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.CustomerOperations.Command.CreateCustomer;
using MovieStore.Aplication.CustomerOperations.Command.CreateToken;
using MovieStore.Aplication.CustomerOperations.Command.DeleteCustomer;
using MovieStore.Aplication.CustomerOperations.Command.RefreshToken;
using MovieStore.Aplication.CustomerOperations.Command.UpdateCustomer;
using MovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName;
using MovieStore.Aplication.CustomerOperations.Queries.GetCustomers;
using MovieStore.Aplication.MovieOperations.Command.UpdateMovie;
using MovieStore.DbOperations;
using MovieStore.TokenOperations.Models;
using static MovieStore.Aplication.CustomerOperations.Command.CreateToken.CreateTokenCommand;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController: ControllerBase
    {

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public CustomerController(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            GetCustomersQuery query = new GetCustomersQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return Ok(token);

        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> refreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _configuration);
            command.RefreshToken = token;
            var refreshToken = command.Handle();
            return Ok(refreshToken);

        }


        [HttpGet("{Email}")]
        public IActionResult GetCustomerByName(string Email) 
        {
            var query = new GetCustomerByNameQuery(_dbContext, _mapper);
            query.email = Email; 
            GetCustomerByNameQueryValidator validationRules = new GetCustomerByNameQueryValidator();
            validationRules.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] CreateCustomerModel model) 
        {
            CreateCustomerCommand command= new CreateCustomerCommand(_dbContext,_mapper);
            command.Model = model;
            CreateCustomerCommandValidator validator= new CreateCustomerCommandValidator(_dbContext);
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("Email")]
        public IActionResult DeleteCustomer(string Email) 
        {
            var query= new DeleteCustomerCommand(_dbContext);
            query.email = Email;
            DeleteCustomerCommandValidator validationRules = new DeleteCustomerCommandValidator();
            validationRules.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }


        [HttpPut("{Email}")]
        public IActionResult UpdateMovie([FromBody] UpdateCustomerModel model, string Email)
        {
            UpdateCustomerCommand command = new UpdateCustomerCommand(_dbContext, _mapper);
            command.email= Email;
            command.Model = model;
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}
