#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using Services;

namespace Web.Areas.ProductAdmin.Controllers
{
    [Area("ProductAdmin")]
    public class CategoriesController : Controller
    {
        private readonly CategoryManager _categoryManager;

        public CategoriesController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }


        // GET: ProductAdmin/Categories
        public IActionResult Index()
        {
            return View(_categoryManager.GetAll());
        }

        // GET: ProductAdmin/Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var category = _categoryManager.GetById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // GET: ProductAdmin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductAdmin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IconUrl,Id,IsDeleted")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryManager.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: ProductAdmin/Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var category = _categoryManager.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: ProductAdmin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,IconUrl,Id,IsDeleted")] Category category)
        {
            if (id != category.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryManager.Update(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryManager.CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: ProductAdmin/Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var category = _categoryManager.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: ProductAdmin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = _categoryManager.GetById(id);
            _categoryManager.Delete(category);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
