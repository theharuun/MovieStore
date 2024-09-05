using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Aplication.MovieOperations.Command.CreateMovie;
using MovieStore.Aplication.MovieOperations.Command.DeleteMovie;
using MovieStore.Aplication.MovieOperations.Command.UpdateMovie;
using MovieStore.Aplication.MovieOperations.Queries.GetMovieByID;
using MovieStore.Aplication.MovieOperations.Queries.GetMovies;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMoviesQuery query= new GetMoviesQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetMovieByID(int id) 
        {
            GetMoviesByIDQuery query=new GetMoviesByIDQuery(_dbContext,_mapper);
            query.ID = id;
            GetMoviesByIDQueryValidator validator=new GetMoviesByIDQueryValidator();
            validator.ValidateAndThrow(query);
            var result=query.Handle();
            return Ok(result);
        }


        [Authorize]
        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateMovieModel model)
        {
            CreateMovieCommand command= new CreateMovieCommand(_dbContext, _mapper);
            command.Model = model;
            CreateMovieCommandValidator validationRules = new CreateMovieCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{MovieId}")]
        public IActionResult DeleteBook(int MovieId)
        {
            DeleteCommandMovie command = new DeleteCommandMovie(_dbContext);

            command.movieId = MovieId;
           DeleteCommandMovieValidator validator= new DeleteCommandMovieValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieModel model,int id)
        {
            UpdateCommandMovie command= new UpdateCommandMovie(_dbContext, _mapper);
            command.ID = id;
            command.Model= model;
            UpdateCommandMovieValidator validator= new UpdateCommandMovieValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
