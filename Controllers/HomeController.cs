using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCategories.Models;

namespace ProductsCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context {get; set;}
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.Product = _context.Products.ToList();
            return View();
        }

        [HttpPost("/addproduct")]
        public IActionResult AddProduct(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();

                return Redirect("/");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("/products/{productID}")]
        public IActionResult ShowProduct(int productID)
        {
            Product product = _context.Products
            .Include(p => p.Associations).ThenInclude( a => a.Category)
            .FirstOrDefault(p => p.ProductId == productID);
            ViewBag.CatNotProd = _context.Categories.Include(c => c.Associations).Where(c => c.Associations.All(a => a.ProductId != productID)).ToList();
            ViewBag.Product = product;
            ViewBag.Category = _context.Categories.ToList();
            return View("ShowProduct", product);
        }

        [HttpPost("/joincat")]
        public IActionResult JoinCat(int ProductId, int CategoryId)
        {
            Association join = new Association();
            join.ProductId = ProductId;
            join.CategoryId = CategoryId;
            _context.Associations.Add(join);
            _context.SaveChanges();
            return Redirect($"/products/{ProductId}");
        }

        [HttpGet("/categories")]
        public IActionResult Categories()
        {
            ViewBag.Category = _context.Categories.ToList();
            return View("Categories");
        }

        [HttpPost("/addcategory")]
        public IActionResult AddCategory(Category newCategory)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Add(newCategory);
                _context.SaveChanges();

                return Redirect("/categories");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("/categories/{categoryID}")]
        public IActionResult ShowCategory(int categoryID)
        {
            Category category = _context.Categories
            .Include(c => c.Associations).ThenInclude( a => a.Product)
            .FirstOrDefault(c => c.CategoryId == categoryID);
            ViewBag.ProdNotCat = _context.Products.Include(p => p.Associations).Where(p => p.Associations.All(a => a.CategoryId != categoryID)).ToList();
            ViewBag.Category = category;
            ViewBag.Product = _context.Products.ToList();
            return View("ShowCategory", category);
        }

        [HttpPost("/joinprod")]
        public IActionResult JoinProd(int ProductId, int CategoryId)
        {
            Association join = new Association();
            join.ProductId = ProductId;
            join.CategoryId = CategoryId;
            _context.Associations.Add(join);
            _context.SaveChanges();
            return Redirect($"/categories/{CategoryId}");
        }

    }
}
