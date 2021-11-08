using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testProject.Models;

namespace testProject.Repositories
{
    public interface IDashboardRepo
    {
        List<MenuAccessModel> GetMenu(string CustomerId, bool IsAccess);
    }
}
