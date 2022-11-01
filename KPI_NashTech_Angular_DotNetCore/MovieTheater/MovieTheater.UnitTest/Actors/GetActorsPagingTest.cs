using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieTheater.Infrastructure.Data.ApplicationDbContext;
using MovieTheater.Infrastructure.Data.Models;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using MovieTheater.Services.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.UnitTest.Actors
{
    public class GetActorsPagingTest
    {
        private readonly IActorService _actorService;

        private readonly HttpContext _httpContext;

        private readonly MovieDbContext _dbcontext;

        private readonly IMapper _mapper;

        private readonly Mock<IStorageService> _storageService;

        public GetActorsPagingTest()
        {
            _storageService = new Mock<IStorageService>();

            _httpContext = new DefaultHttpContext();

            var options = new DbContextOptionsBuilder<MovieDbContext>()
               .UseInMemoryDatabase(databaseName: "Movie_API").Options;

            _dbcontext = new MovieDbContext(options);

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<ActorDTO, Actor>().ReverseMap());

            _mapper = new Mapper(configuration);

            _actorService = new ActorService
            (
                _dbcontext,
                _mapper,
                _storageService.Object
            );
        }

        [Test]
        public async Task GetActors_Default_Correct()
        {
            // arrange
            var query = new PaginationDTO();

            var listActor = new List<Actor>
            {
                new Actor { Name = "Test", Dob = DateTime.Now, Picture ="1", Story="1"},
                new Actor { Name = "Test1", Dob = DateTime.Now, Picture ="2", Story="2"},
                new Actor { Name = "Test2", Dob = DateTime.Now, Picture ="3", Story="3"}
            };

            _dbcontext.Actors.AddRange(listActor);

            _dbcontext.SaveChanges();

            // act
            var result = await _actorService.GetAllActor(query, _httpContext);

            // assert
            Assert.AreEqual(3, result.Count);

            Assert.AreEqual("Test", result[0].Name);
        }
    }
}
