using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Buy_Recipes_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Buy_Recipes_Final_Project.Controllers
{
    //Managed all the recipes
    public class RecipesController : Controller
    {
        private readonly Buy_Recipes_DataContext _context;

        public RecipesController(Buy_Recipes_DataContext context)
        {
            _context = context;
        }

        //Gets all recipes
        // GET: Recipes
        [Authorize(Roles = "super_user,customer")]
        public async Task<IActionResult> Index()
        {
            return View(await (from recipes in _context.Recipe select recipes).ToListAsync());
        }

        // GET: Recipes/Details/5
        //Gets the details of the recipe

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        //Gets the create recipe for
        [Authorize(Roles = "super_user")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Creates the recipe

        [Authorize(Roles = "super_user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Instructions,Price,RecipeImage")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                UploadRecipe(recipe);
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        //Gets the recipe ofr update
        [Authorize(Roles = "super_user")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates the recipe
        [Authorize(Roles = "super_user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Instructions,RecipeImageLocation,Price, RecipeImage")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadRecipe(recipe);
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
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
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        //Gets the recipe for delete
        [Authorize(Roles = "super_user")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        //Deletes the recipe
        [Authorize(Roles = "super_user")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Checks existance by sending  a lamda query
        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }


        //Uploads the recipe to folder recipes under wwwroot

        private void UploadRecipe(Recipe Recipe)
        {

            if (Recipe.RecipeImage != null)
            {
                Recipe.RecipeImageLocation = Recipe.RecipeImage.FileName;

                var filePath = "./wwwroot/recipes/" + Recipe.RecipeImage.FileName; ;


                if (Recipe.RecipeImage.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Recipe.RecipeImage.CopyTo(stream);
                    }
                }
            }


        }
    }
}
