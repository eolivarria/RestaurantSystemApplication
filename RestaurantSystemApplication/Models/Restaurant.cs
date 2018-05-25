using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantSystemApplication.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Serve> Serves { get; set; }
    }
}
