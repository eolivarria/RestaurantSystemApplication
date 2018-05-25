using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantSystemApplication.Models;
using RestaurantSystemApplication.ViewModels;

namespace RestaurantSystemApplication.Controllers
{
    public class PlateController : Controller
    {
        private readonly RestaurantContext _context;

        public PlateController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: Plate
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plates.ToListAsync());
        }

        // GET: Plate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plate = await _context.Plates
                .SingleOrDefaultAsync(m => m.PlateID == id);
            if (plate == null)
            {
                return NotFound();
            }

            return View(plate);
        }

        // GET: Plate/Create
        public IActionResult Create()
        {
            PopulateRestaurantsDropDownList();
            return View();
        }

        // POST: Plate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlateID,Name,Ingredients,Price,Serves")] Plate plate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plate);
        }

        // GET: Plate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plate = await _context.Plates.SingleOrDefaultAsync(m => m.PlateID == id);
            if (plate == null)
            {
                return NotFound();
            }

            PopulateRestaurantsDropDownList(plate.PlateID);
            return View(plate);
        }

        // POST: Plate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlateID,Name,Ingredients,Price")] Plate plate)
        {
            if (id != plate.PlateID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlateExists(plate.PlateID))
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
            return View(plate);
        }

        // GET: Plate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plate = await _context.Plates
                .SingleOrDefaultAsync(m => m.PlateID == id);
            if (plate == null)
            {
                return NotFound();
            }

            return View(plate);
        }

        // POST: Plate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plate = await _context.Plates.SingleOrDefaultAsync(m => m.PlateID == id);
            _context.Plates.Remove(plate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlateExists(int id)
        {
            return _context.Plates.Any(e => e.PlateID == id);
        }


        private void PopulateRestaurantsDropDownList(object selectedRestaurant = null){
            if(selectedRestaurant == null){
                var restaurantsQuery = from d in _context.Restaurants orderby d.Name select d;
                ViewBag.ID = new SelectList(restaurantsQuery, "ID", "Name", null); 
            }else{
                var restautant = _context.Restaurants.Where(y => y.ID == (_context.Serves.Where(x => x.PlateID == (int)selectedRestaurant).Single()).RestaurantID);
                ViewBag.ID = new SelectList(restautant, "ID", "Name");
            }
        }
    }
}
