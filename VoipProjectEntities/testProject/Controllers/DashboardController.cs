using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testProject.Models;
using testProject.Repositories;

namespace testProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepo repo;
        const string CustomerId = "";
        public DashboardController(IDashboardRepo _repo)
        {
            repo = _repo;
        }

        #region "Index"
        public IActionResult Index()
        {
            string CustId =  HttpContext.Session.GetString(CustomerId);
            List<MenuAccessModel> MenuList = repo.GetMenu(CustId, true);

            if (MenuList.Count > 0)
            {
                var menus = MenuList;
                ViewBag.menuList = menus.ToList();
            }

            return View();
        }
        #endregion
    }
}
