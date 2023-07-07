using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Common
{
    public static class AllExtensions
    {
        public static bool IsNotNull(this object obj)
        {
            if (obj == null) return false;
            return true;
        }
    }
}
