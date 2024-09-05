using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Aplication.ActorOperations.Command.CreateActor;
using MovieStore.Aplication.ActorOperations.Command.DeleteActor;
using MovieStore.Aplication.ActorOperations.Command.UpdateActor;
using MovieStore.Aplication.ActorOperations.Queries.GetActorByID;
using MovieStore.Aplication.ActorOperations.Queries.GetActors;
using MovieStore.Aplication.DirectorOperations.Command.CreateDirector;
using MovieStore.Aplication.DirectorOperations.Command.DeleteDirector;
using MovieStore.Aplication.DirectorOperations.Command.UpdateDirector;
using MovieStore.Aplication.DirectorOperations.Queries.GetDirectorByIDQuery;
using MovieStore.Aplication.DirectorOperations.Queries.GetDirectors;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DirectorController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDirectors()
        {
            GetDirectorsQuery query = new GetDirectorsQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetActorByID(int id)
        {
            GetDirectorByIDQuery query = new GetDirectorByIDQuery(_dbContext, _mapper);
            query.ID = id;
            GetDirectorByIDQueryValidator validator = new GetDirectorByIDQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddDirectr([FromBody] CreateDirectorModel model)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_dbContext, _mapper);
            command.Model = model;
            CreateDirectorCommandValidator validationRules = new CreateDirectorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{DirectorId}")]
        public IActionResult DeleteBook(int DirectorId)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);

            command.directorId = DirectorId;
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromBody] UpdateDirectorModel model, int id)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext, _mapper);
            command.ID = id;
            command.Model = model;
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
