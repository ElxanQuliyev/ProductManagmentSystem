using DataAccess;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.ProductAdmin.Controllers
{
    [Area("ProductAdmin")]
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ProductManager productManager, CategoryManager categoryManager, IWebHostEnvironment webHostEnvironment)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View(_productManager.GetAll());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null) return NotFound();

            var selectedProduct = _productManager.GetById(id.Value);
            if(selectedProduct == null) return NotFound();
            return View(selectedProduct);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = _categoryManager.GetAll();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product,IFormFile PhotoUrl)
        {
            ViewBag.CategoryId = _categoryManager.GetAll();
            if (ModelState.IsValid)
            {
                if (PhotoUrl!=null)
                {
                    string fileName=Guid.NewGuid()+PhotoUrl.FileName;
                    var rootFile = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    var photoLink = Path.Combine(rootFile, fileName);
                    using FileStream fileStream = new(photoLink,FileMode.Create);
                    PhotoUrl.CopyTo(fileStream);
                    product.PhotoUrl = "/uploads/" + fileName;
                    product.PublishDate = DateTime.Now;
                    _productManager.Add(product);

                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }


        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id==null) return NotFound();
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id==null) return NotFound();
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
