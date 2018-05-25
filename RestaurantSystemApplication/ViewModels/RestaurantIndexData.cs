using System.Collections.Generic;
using RestaurantSystemApplication.Models;

namespace RestaurantSystemApplication.ViewModels
{
    public class RestaurantIndexData
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public IEnumerable<Plate> Plates { get; set; }
        public IEnumerable<Serve> Serves { get; set; }
    }
}
