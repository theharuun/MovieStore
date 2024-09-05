using MovieStore.Entities;
using AutoMapper;
using MovieStore.Aplication.MovieOperations.Command.CreateMovie;
using MovieStore.Aplication.MovieOperations.Queries.GetMovies;
using MovieStore.Aplication.MovieOperations.Command.UpdateMovie;
using MovieStore.Aplication.MovieOperations.Queries.GetMovieByID;
using MovieStore.Aplication.CustomerOperations.Command.CreateCustomer;
using MovieStore.Aplication.CustomerOperations.Queries.GetCustomers;
using MovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName;
using MovieStore.Aplication.CustomerOperations.Command.UpdateCustomer;
using MovieStore.Aplication.ActorOperations.Queries.GetActors;
using MovieStore.Aplication.ActorOperations.Queries.GetActorByID;
using MovieStore.Aplication.ActorOperations.Command.CreateActor;
using MovieStore.Aplication.DirectorOperations.Queries.GetDirectors;
using MovieStore.Aplication.DirectorOperations.Queries.GetDirectorByIDQuery;
using MovieStore.Aplication.DirectorOperations.Command.CreateDirector;
using MovieStore.Aplication.OrderOperations.Queries.GetOrders;
using MovieStore.Aplication.OrderOperations.Queries.GetOrderByID;
using MovieStore.Aplication.OrderOperations.Command.CreateOrder;
using MovieStore.Aplication.OrderOperations.Command.UpdateOrder;

namespace MovieStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //movie

            CreateMap<CreateMovieModel, Movie>().ForMember(dest => dest.MovieActors, opt => opt.Ignore()); // create
            CreateMap<UpdateMovieModel, Movie>().ForMember(dest => dest.MovieActors, opt => opt.Ignore()); // update
            CreateMap<Movie, GetMoviesModel>() // get
                    .ForMember(dest => dest.genre, opt => opt.MapFrom(src => src.Genre.Name))
                    .ForMember(dest=>dest.director,opt=>opt.MapFrom(src=>src.Director.Name+" "+ src.Director.Surname))
                     .ForMember(dest => dest.actors, opt => opt.MapFrom(src =>src.MovieActors.Select(ma => $"{ma.Actor.Name} {ma.Actor.Surname}").ToList()));
            CreateMap<Movie, GetMoviesByIDModel>() // getbyıd
                    .ForMember(dest => dest.genre, opt => opt.MapFrom(src => src.Genre.Name))
                    .ForMember(dest => dest.director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname))
                    .ForMember(dest => dest.actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma => $"{ma.Actor.Name} {ma.Actor.Surname}").ToList()));

            //genre
            //customer
            CreateMap<CreateCustomerModel, Customer>().ReverseMap(); // create
            CreateMap<UpdateCustomerModel, Customer>().ReverseMap(); // update
            CreateMap<Customer, GetCustomersModel>() //gets
                .ForMember(dest => dest.purchasedMovies, opt => opt.MapFrom(src => src.purchasedMovies.Select(pm => pm.Movie.MovieName))) // burada .moviename diyerek string liste tuttuğumuz yerdeki yere sırala maplicek eğer bütün movinin özelliklerini gelsin istiyorsaksadece Movie yeterli
                .ForMember(dest => dest.favoriteGenres, opt => opt.MapFrom(src => src.favoriteGenres.Select(fg => fg.Genre.Name)));

            CreateMap<Customer, GetCustomerByNameModel>() //get by name
               .ForMember(dest => dest.purchasedMovies, opt => opt.MapFrom(src => src.purchasedMovies.Select(pm => pm.Movie.MovieName))) // burada .moviename diyerek string liste tuttuğumuz yerdeki yere sırala maplicek eğer bütün movinin özelliklerini gelsin istiyorsaksadece Movie yeterli
               .ForMember(dest => dest.favoriteGenres, opt => opt.MapFrom(src => src.favoriteGenres.Select(fg => fg.Genre.Name)));

            // PurchasedMovie -> Movie for  gets and by name
            CreateMap<PurchasedMovie, Movie>();

            // FavoriteGenre -> Genre for  gets and by name
            CreateMap<FavoriteGenre, Genre>();

            //actor

            CreateMap<CreateActorModel, Actor>().ForMember(dest => dest.MovieActors, opt => opt.Ignore()); // create
            CreateMap<Actor,GetActorsModel>() // gets
                .ForMember(dest => dest.movies, opt => opt.MapFrom(src => src.MovieActors.Select(ma => $"{ma.Movie.MovieName}").ToList()));
            CreateMap<Actor, GetActorByIdModel>() // getByID
               .ForMember(dest => dest.movies, opt => opt.MapFrom(src => src.MovieActors.Select(ma => $"{ma.Movie.MovieName}").ToList()));

            //director
            CreateMap<CreateDirectorModel, Director>().ForMember(dest => dest.Movies, opt => opt.Ignore()); // create
            CreateMap<Director,GetDirectorsModel>() //gets
                .ForMember(dest=> dest.Movies , opt=> opt.MapFrom(src=>src.Movies.Select(ma=>$"{ma.MovieName}").ToList()));
            CreateMap<Director, GetDirectorByIDModel>() //getByID
               .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(ma => $"{ma.MovieName}").ToList()));
            //order
            CreateMap<CreateOrderModel, Order>().ForMember(dest => dest.Movies, opt => opt.Ignore()); // create
            CreateMap<UpdateOrderModel, Order>().ForMember(dest => dest.Movies, opt => opt.Ignore()); // update
            CreateMap<Order, GetOrdersModel>() //gets
                .ForMember(dest=>dest.customer , opt=>opt.MapFrom(src=>src.Customer.Name+" "+src.Customer.Surname+ " -- email :" + src.Customer.Email))
               .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(ma => $"{ma.MovieName} - Price/Fiyat: {ma.Price}").ToList()));
            CreateMap<Order, GetOrderByIDModel>() //getByID
               .ForMember(dest => dest.customer, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname + " -- email :" + src.Customer.Email))
              .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(ma => $"{ma.MovieName} - Price/Fiyat: {ma.Price}").ToList()));
        }
    }
}
