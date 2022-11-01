using Microsoft.AspNetCore.Http;
using MovieTheater.Infrastructure.Data.Models;
using MovieTheater.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Interface
{
    public interface IActorService
    {
        Task<List<ActorMovieDTO>> SearchByName(string name);
        Task<List<ActorDTO>>GetAllActor(PaginationDTO paginationDTO, HttpContext httpContext);
        Task<ActorDTO> GetActorById(int Id);
        Task<Actor> CreateActor(CreateActor createActor);
        Task<Actor> UpdateActor(int Id, CreateActor updateActor);
        Task<ActorDTO> DeleteActor(int Id); 

    }
}
