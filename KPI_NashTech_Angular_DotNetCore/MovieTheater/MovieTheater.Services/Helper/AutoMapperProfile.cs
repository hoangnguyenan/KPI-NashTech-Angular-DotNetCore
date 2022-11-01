using AutoMapper;
using MovieTheater.Services.DTOs;
using MovieTheater.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Identity;

namespace MovieTheater.Services.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

        }

        public AutoMapperProfile(GeometryFactory geometryFactory)
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();// config automap from CategoryDTO to Category and opposite
            CreateMap<CreateCategory, Category>();

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<CreateActor, Actor>().ForMember(x => x.Picture, options => options.Ignore());

            CreateMap<Cinema, MovieTheaterDTO>()
                .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<CreateMovieTheater, Cinema>()
                .ForMember(x => x.Location, x => x.MapFrom(prop => 
                geometryFactory.CreatePoint(new Coordinate(prop.Longitude, prop.Latitude))));

            CreateMap<CreateMovie, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MovieCategories, options => options.MapFrom(MapMovieCategories))
                .ForMember(x => x.MovieCinemas, options => options.MapFrom(MapMovieCinemas))
                .ForMember(x => x.MovieActors, options => options.MapFrom(MapMovieActors));

            CreateMap<Movie, MovieDTO>()
              .ForMember(x => x.Categories, options => options.MapFrom(MapMovieCategoriesDTO))
              .ForMember(x => x.MovieTheaters, options => options.MapFrom(MapMovieCinemasDTO))
              .ForMember(x => x.Actors, options => options.MapFrom(MapMovieActorsDTO));

            CreateMap<IdentityUser, UserDTO>();
        }

        private List<MovieCategory> MapMovieCategories(CreateMovie createMovie, Movie movie)
        {
            var result = new List<MovieCategory>();

            if (createMovie.CategoriesId == null) { return result; }

            foreach (var id in createMovie.CategoriesId)
            {
                result.Add(new MovieCategory() { CategoryId = id });
            }

            return result;
        }
        private List<MovieCinema> MapMovieCinemas(CreateMovie createMovie, Movie movie)
        {
            var result = new List<MovieCinema>();

            if (createMovie.CinemasId == null) { return result; }

            foreach (var id in createMovie.CinemasId)
            {
                result.Add(new MovieCinema() { CinemaId = id });
            }

            return result;
        }
        private List<MovieActor> MapMovieActors(CreateMovie createMovie, Movie movie)
        {
            var result = new List<MovieActor>();

            if (createMovie.Actors == null) { return result; }

            foreach (var actor in createMovie.Actors)
            {
                result.Add(new MovieActor() { ActorId = actor.Id, Character = actor.Character });
            }

            return result;
        }

        private List<CategoryDTO> MapMovieCategoriesDTO(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<CategoryDTO>();

            if (movie.MovieCategories != null)
            {
                foreach (var category in movie.MovieCategories)
                {
                    result.Add(new CategoryDTO() { Id = category.CategoryId, Name = category.Category.Name });
                }
            }

            return result;
        }
        private List<MovieTheaterDTO> MapMovieCinemasDTO(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<MovieTheaterDTO>();

            if (movie.MovieCinemas != null)
            {
                foreach (var movieCinema in movie.MovieCinemas)
                {
                    result.Add(new MovieTheaterDTO()
                    {
                        Id = movieCinema.CinemaId,
                        Name = movieCinema.Cinema.Name,
                        Address = movieCinema.Cinema.Address,
                        Latitude = movieCinema.Cinema.Location.Y,
                        Longitude = movieCinema.Cinema.Location.X
                    });
                }
            }

            return result;
        }
        private List<ActorMovieDTO> MapMovieActorsDTO(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<ActorMovieDTO>();

            if (movie.MovieActors != null)
            {
                foreach (var movieActor in movie.MovieActors)
                {
                    result.Add(new ActorMovieDTO()
                    {
                        Id = movieActor.ActorId,
                        Name = movieActor.Actor.Name,
                        Character = movieActor.Character,
                        Picture = movieActor.Actor.Picture,
                        Order = movieActor.Order
                    });
                }
            }

            return result;
        }
    }
}
