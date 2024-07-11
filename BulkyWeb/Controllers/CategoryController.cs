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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {       //adding error message for a real world business scenario(following check has no significance)
            if (obj.Name == obj.DisplayOrder.ToString())//toString() will convert the numeric value to string type

            {   //ModelState is a property of Controller that contains all the data posted by the user(here ModelState is an object of Category class)
                ModelState.AddModelError("name", "Name and displayOrder can't be same");//here we are adding a error message to "Name" property of ModelState   
            }

            if(obj.Name == "test")
            {//Model level is set because of ""
                    ModelState.AddModelError("", "This will be set as a model level error since we did not specified any property name like we did in above example");
            }

            if (ModelState.IsValid)//this will check all the validations applied using data annotations in Model Class
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "New category added successfully";//This is temporary data which can be used to show notifications on the next page.Once used this data will be released from the catch.
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id) //? allows this parameter to be a null value
        {
            if(id ==null || id <= 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.categories.Find(id); //Note that Find() only works with the primary key

          /*  //Searching in a non primary key property
            Category? categoryFromDb1 = _db.categories.FirstOrDefault(cat => cat.Id==id);//here i could even iterate over a non-PK property and this will                                                                           return the first satisfying the condition or default if source is empty
            
            //This is another way if property is Non-PK. Here we can apply a filter with Where clause.
            Category? categoryFromDb2 = _db.categories.Where(cat => cat.Id == id).FirstOrDefault();*/

            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
         public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? categoryToDel = _db.categories.Find(id);
            _db.categories.Remove(categoryToDel);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
