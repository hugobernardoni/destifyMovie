using AutoMapper;
using DestifyMovie.API.ViewModels;
using DestifyMovie.Data.Entities;

namespace DestifyMovie.API.Mappings;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<Movie, MovieBasicViewModel>();
        CreateMap<Actor, ActorBasicViewModel>();

        CreateMap<Movie, MovieViewModel>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings));

        CreateMap<Actor, ActorViewModel>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));

        CreateMap<MovieRating, MovieRatingViewModel>();

        CreateMap<MovieInputView, Movie>()
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Ratings, opt => opt.Ignore()); 

        CreateMap<ActorInputView, Actor>();

        CreateMap<MovieRatingInputView, MovieRating>();
    }
}
