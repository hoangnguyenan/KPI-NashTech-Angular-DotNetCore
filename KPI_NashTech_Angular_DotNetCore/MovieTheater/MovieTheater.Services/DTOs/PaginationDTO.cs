using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Services.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordPerpage = 10;
        private readonly int maxRecords = 50;
        public int RecordsPerpage
        {
            get
            {
                return recordPerpage;
            }
            set
            {
                recordPerpage = (value > maxRecords) ? maxRecords : value;// ex: if records = 100 => records = 50, if records = 30 => records = 30
            }
        }
    }
}
