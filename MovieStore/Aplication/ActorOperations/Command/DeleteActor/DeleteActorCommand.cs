using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.ActorOperations.Command.DeleteActor
{
    public class DeleteActorCommand
    {
        public readonly IMovieStoreDbContext _context;
        public int actorId { get; set; }
        public DeleteActorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var actor= _context.Actors.Include(x=>x.MovieActors).ThenInclude(x=>x.Movie).SingleOrDefault(x => x.ActorID == actorId );
            if (actor== null)
                throw new InvalidOperationException("Actor not found-Oyuncu bulunamadi");

            if (actor.MovieActors.Any())
                throw new InvalidOperationException("There are movies this actor has played in. First, you must delete the movie. - Bu oyuncunun oynadigi filmler var. Öncelikle filmleri silmelisiniz.");

            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }


    }
}
