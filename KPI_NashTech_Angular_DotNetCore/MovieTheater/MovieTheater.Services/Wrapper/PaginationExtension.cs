using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Services.Wrapper
{
    public static class PaginationExtension
    {
        //InsertParametersPaginationInHeader
        public async static Task PaginationQueryable<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            double count = await queryable.CountAsync();
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            httpContext.Response.Headers.Add("totalAmountOfRecords", count.ToString());
        }
        public async static Task PaginationQueryableForMovie<T>(this HttpContext httpContext, IQueryable<T> queryableUpComing, IQueryable<T> queryableInCinema)
        {
            double countUpComing = await queryableUpComing.CountAsync();
            double countInCinema = await queryableInCinema.CountAsync();
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            httpContext.Response.Headers.Add("totalAmountOfRecordsUpComing", countUpComing.ToString());
            httpContext.Response.Headers.Add("totalAmountOfRecordsInCinema", countInCinema.ToString());
        }
    }
}
