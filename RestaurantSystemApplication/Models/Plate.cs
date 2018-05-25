using System;
using System.Collections.Generic;

namespace RestaurantSystemApplication.Models
{
    public class Plate
    {
        public int PlateID { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Serve> Serves { get; set; }
    }
}
