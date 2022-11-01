using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class FilterMoviesDTO
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public PaginationDTO PaginationDTO
        {
            get { return new PaginationDTO() { Page = Page, RecordsPerpage = RecordsPerPage }; }
        }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public bool InCinemas { get; set; }
        public bool UpComingReleases { get; set; }
    }
}
