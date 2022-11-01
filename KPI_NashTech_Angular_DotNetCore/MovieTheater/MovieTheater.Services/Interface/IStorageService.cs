using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Services.Interface
{
    public interface IStorageService
    {
        Task DeleteFile(string fileRoute, string name);
        Task<string> SaveFile(string name, IFormFile file);
        Task<string> EditFile(string name, string fileRoute, IFormFile file);
    }
}
