using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Infrastructure.Data.ApplicationDbContext;
using MovieTheater.Infrastructure.Data.Models;
using MovieTheater.Services.DTOs;
using MovieTheater.Services.Interface;
using MovieTheater.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.Service
{
    public class ActorService: IActorService
    {
        private readonly MovieDbContext _context;
        private readonly IStorageService storageService;
        private readonly string containerName = "actorImages";
        private readonly IMapper mapper;
        public ActorService(MovieDbContext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            this.mapper = mapper;
            this.storageService = storageService;
        }

        public async Task<List<ActorMovieDTO>> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return new List<ActorMovieDTO>(); }
            return await _context.Actors
                .Where(x => x.Name.Contains(name))
                .OrderBy(x => x.Name)
                .Select(x => new ActorMovieDTO { Id = x.Id, Name = x.Name, Picture = x.Picture })
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<ActorDTO>> GetAllActor(PaginationDTO paginationDTO, HttpContext httpContext)
        {
            var queryable = _context.Actors.AsQueryable();
            await httpContext.PaginationQueryable(queryable);
            var actors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<ActorDTO>>(actors);
        }

        public async Task<ActorDTO> GetActorById(int Id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Id);
            return mapper.Map<ActorDTO>(actor);
        }

        public async Task<Actor> CreateActor(CreateActor createActor)
        {
            var actor = mapper.Map<Actor>(createActor);
            if (createActor.Picture != null)
            {
                actor.Picture = await storageService.SaveFile(containerName, createActor.Picture);
            }
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            return mapper.Map<Actor>(actor);
        }
        public async Task<Actor> UpdateActor(int Id, CreateActor updateActor)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Id);
            actor = mapper.Map(updateActor, actor);
            if (updateActor.Picture != null)
            {
                actor.Picture = await storageService.EditFile(containerName, actor.Picture, updateActor.Picture);
            }
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return mapper.Map<Actor>(actor);
        }
        public async Task<ActorDTO> DeleteActor(int Id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            await storageService.DeleteFile(actor.Picture, containerName);
            return mapper.Map<ActorDTO>(actor);
        }
    }
}
