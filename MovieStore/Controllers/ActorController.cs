using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Aplication.ActorOperations.Command.CreateActor;
using MovieStore.Aplication.ActorOperations.Command.DeleteActor;
using MovieStore.Aplication.ActorOperations.Command.UpdateActor;
using MovieStore.Aplication.ActorOperations.Queries.GetActorByID;
using MovieStore.Aplication.ActorOperations.Queries.GetActors;
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
    public class ActorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public ActorController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetActors()
        {
           GetActorsQuery query = new GetActorsQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);

        }


        [HttpGet("{id}")]
        public IActionResult GetActorByID(int id)
        {
            GetActorByIDQuery query = new GetActorByIDQuery(_dbContext, _mapper);
            query.ID = id;
            GetActorByIDQueryValidator validator = new GetActorByIDQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddActor([FromBody] CreateActorModel model)
        {
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            command.Model = model;
            CreateActorCommandValidator validationRules = new CreateActorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpDelete("{ActorId}")]
        public IActionResult DeleteBook(int ActorId)
        {
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);

            command.actorId = ActorId;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromBody] UpdateActorModel model, int id)
        {
            UpdateActorCommand command = new UpdateActorCommand(_dbContext, _mapper);
            command.actorId = id;
            command.Model = model;
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
