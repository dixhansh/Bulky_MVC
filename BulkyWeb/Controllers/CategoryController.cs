using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        //Dependent is getting injected with its dependecy through constructor.
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
                                        //_db has DbSet<> of Category called categories
            List<Category> categories = _db.categories.ToList(); //ToList() will internally hit the database with select * from categories and will return the List<Category> 

            return View(categories); 
        }
    }
}
