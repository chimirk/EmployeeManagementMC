using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Dapper
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();

    }
}
