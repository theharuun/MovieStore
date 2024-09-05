using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Aplication.DirectorOperations.Command.CreateDirector
{
    public class CreateDirectorCommand
    {
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;

        public CreateDirectorModel Model { get; set; }
        public CreateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var director= _context.Directors.Include(x=>x.Movies).SingleOrDefault(x=>x.Name==Model.name && x.Surname==Model.surname);
            if (director != null)
                throw new InvalidOperationException("There is already this director-Bu direktör zaten var");
            director = _mapper.Map<Director>(Model);

            // filmler ekle
            if (Model.moviesIDs != null && Model.moviesIDs.Any())
            {
                // MovieID'leri ile mevcut filmleri veri tabanından alıyoruz
                director.Movies = _context.Movies
                                         .Where(movie => Model.moviesIDs.Contains(movie.MovieID ))
                                         .ToList();
            }
            _context.Directors.Add(director);
            _context.SaveChanges();


        }

    }
    public class CreateDirectorModel 
    {
        public string name { get; set; }
        public string surname { get; set; }
        public List<int> moviesIDs { get; set; }
    }
}
