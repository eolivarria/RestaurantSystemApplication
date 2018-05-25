using System;
namespace RestaurantSystemApplication.Models
{
    public class Serve
    {
        public int ServeID { get; set; }
        public int RestaurantID { get; set; }
        public int PlateID { get; set; }
        public string FoodType { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Plate Plate { get; set; }
    }
}
