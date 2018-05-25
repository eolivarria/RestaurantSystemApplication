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
    public class RestaurantController : Controller
    {
        private readonly RestaurantContext _context;

        public RestaurantController(RestaurantContext context)
        {
            _context = context;
        }
        /*
        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restaurants.ToListAsync());
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewBag.NameSortParm = searchString == "Name" ? "name_desc" : "Name";
            ViewBag.AddressSortParm = searchString == "Address" ? "address_desc" : "Address";
            ViewBag.CitySortParm = searchString == "City" ? "city_desc" : "City";
            ViewBag.StateSortParm = searchString == "State" ? "state_desc" : "State";
            ViewBag.PhoneSortParm = searchString == "Phone" ? "phone_desc" : "Phone";
            ViewBag.OwnerSortParm = searchString == "OwnerName" ? "owner_desc" : "OwnerName";
            ViewBag.DateSortParm = searchString == "RegistrationDate" ? "date_desc" : "RegistrationDate";

            var restaurants = from m in _context.Restaurants
                         select m;
            
            switch (searchString)
            {
                case "Name":
                    restaurants = restaurants.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    restaurants = restaurants.OrderByDescending(s => s.Name);
                    break;
                case "Address":
                    restaurants = restaurants.OrderBy(s => s.Address);
                    break;
                case "City":
                    restaurants = restaurants.OrderBy(s => s.City);
                    break;
                case "State":
                    restaurants = restaurants.OrderBy(s => s.State);
                    break;
                case "Phone":
                    restaurants = restaurants.OrderBy(s => s.Phone);
                    break;
                case "OwnerName":
                    restaurants = restaurants.OrderBy(s => s.OwnerName);
                    break;
                case "RegistrationDate":
                    restaurants = restaurants.OrderBy(s => s.RegistrationDate);
                    break;
                default:
                    restaurants = restaurants.OrderBy(s => s.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(s => s.Name.Contains(searchString));
            }

            return View(await restaurants.ToListAsync());
        }*/
        public IActionResult Index(int? id, int? plateID, string searchString)
        {
            ViewBag.CurrentFilter = searchString;

            var viewModel = new RestaurantIndexData();
            viewModel.Restaurants = _context.Restaurants
                .OrderBy(i => i.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Restaurants = _context.Restaurants
                    .Where(s => s.Name.Contains(searchString));
            }
            else if (id != null)
            {
                ViewBag.RestaurantID = id.Value;
                // Lazy loading
                //
                //viewModel.Serves = viewModel.Serves.Where(
                //    i => i.RestaurantID == id.Value);
                var selectedRestaurant = viewModel.Restaurants.Where(x => x.ID == id).Single();
                _context.Entry(selectedRestaurant).Collection(x => x.Serves).Load();
                foreach (Serve serve in selectedRestaurant.Serves)
                {
                    _context.Entry(serve).Reference(x => x.Plate).Load();
                }

                viewModel.Serves = selectedRestaurant.Serves;
            }
            /*
            if (plateID != null)
            {
                ViewBag.PlateID = plateID.Value;
                viewModel.Serves = viewModel.Serves.Where(
                    x => x.PlateID == plateID);
            }*/
            return View(viewModel);
        }

        // GET: Restaurant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .SingleOrDefaultAsync(m => m.ID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Address,City,State,Phone,OwnerName,RegistrationDate")] Restaurant restaurant)
        {
                if (ModelState.IsValid)
                {
                    _context.Add(restaurant);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            return View(restaurant);
        }

        // GET: Restaurant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(m => m.ID == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,City,State,Phone,OwnerName,RegistrationDate")] Restaurant restaurant)
        {
            if (id != restaurant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.ID))
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
            return View(restaurant);
        }

        // GET: Restaurant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .SingleOrDefaultAsync(m => m.ID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(m => m.ID == id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.ID == id);
        }
    }
}
