using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Models.BaseModels
{
    public class BaseLookupRequestModel
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 25;
        public string? OrderByColumn { get; set; }
        public bool OrderByAsc { get; set; } = true;
    }
}
