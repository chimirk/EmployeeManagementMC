using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Common
{
    public static class ObjectHelper
    {
        public static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;


        public static bool AreEqualWithoutAs(string str1, string str2)
        {
            return str1.Replace("a", "") == str2.Replace("a", "");
        }
    }
}
