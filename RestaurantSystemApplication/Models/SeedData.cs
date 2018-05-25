using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using RestaurantSystemApplication.Models;

namespace RestaurantSystemApplication.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RestaurantContext(
                    serviceProvider.GetRequiredService<DbContextOptions<RestaurantContext>>()))
            {
                if (context.Restaurants.Any())
                {
                    return;
                }
                var restaurant = new List<Restaurant>{
                    new Restaurant{Name="Vintage Restaurant",Address="Carrer de València",
                        City="Barcelona", State="Spain", Phone="+34 932 29 27 73", OwnerName="Pilar Jimenez Valenzuela",
                        RegistrationDate=DateTime.Parse("2005-09-01")},
                    new Restaurant{Name="Eleven Madison Park",Address="11 Madison Avenue",
                        City="New York", State="United States", Phone="+1 212 889 0905", OwnerName="Daniel Humm",
                        RegistrationDate=DateTime.Parse("2006-05-07")},
                    new Restaurant{Name="Osteria Francescana",Address="Via Stella 22, 41121",
                        City="Modena", State="Italy", Phone="+39 059 223912", OwnerName="Massimo Bottura",
                        RegistrationDate=DateTime.Parse("1958-10-12")},
                    new Restaurant{Name="Mirazur",Address="30 Avenue Aristide Briand, 06500",
                        City="Menton", State="France", Phone="+33 4 92 41 86 86", OwnerName="Mauro Colagreco",
                        RegistrationDate=DateTime.Parse("2003-07-12")},
                    new Restaurant{Name="Central",Address="Calle Santa Isabel 376, Miraflores",
                        City="Lima", State="Peru", Phone="+51 1 242 85153", OwnerName="Virgilio Martínez",
                        RegistrationDate=DateTime.Parse("2001-04-01")}
                };

                restaurant.ForEach(s => context.Restaurants.Add(s));
                context.SaveChanges();

                var plate = new List<Plate>{
                    new Plate{Name="Sweet Potato and Burritos",Ingredients="tortillas black-beans onion tomato sweet potatoes avocado lime", Price=25.00M},
                    new Plate{Name="Egg & Ham",Ingredients="eggs salt pepper ham cheddar-cheese", Price=20.00M},
                    new Plate{Name="Roast Beef",Ingredients="beef onions carrots celery fresh-herbs", Price=30.00M},
                    new Plate{Name="Spicy Pork Barbecue",Ingredients="1-pound-pork-belly pear onion-purée garlic ginger onion soy-sauce pepper-paste", Price=25.00M},
                    new Plate{Name="Cheese & Bacon",Ingredients="eggs salt pepper cheddar-cheese", Price=15.00M},
                    new Plate{Name="Brocoli Pesto Pasta",Ingredients="conchiglie-pasta broccoli chilli-flakes nuts parmesan-cheese", Price=20.00M},
                    new Plate{Name="Meatball Spiders",Ingredients="beef panko pepper sauce mozzarella-cheese olive-oli", Price=25.00M},
                    new Plate{Name="Berry Juice",Ingredients="strawberries blueberries raspberries apple", Price=5.00M}
                };

                plate.ForEach(s => context.Plates.Add(s));
                context.SaveChanges();

                var serves = new List<Serve>
                {
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Vintage Restaurant").ID,
                        PlateID = plate.Single(c => c.Name == "Egg & Ham" ).PlateID,
                        FoodType = "Breakfast"
                    },
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Vintage Restaurant").ID,
                        PlateID = plate.Single(c => c.Name == "Sweet Potato and Burritos" ).PlateID,
                        FoodType = "Lunch"
                    },
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Vintage Restaurant").ID,
                        PlateID = plate.Single(c => c.Name == "Roast Beef" ).PlateID,
                        FoodType = "Dinner"
                    },
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Vintage Restaurant").ID,
                        PlateID = plate.Single(c => c.Name == "Berry Juice" ).PlateID,
                        FoodType = "Drinks"
                    },
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Eleven Madison Park").ID,
                        PlateID = plate.Single(c => c.Name == "Spicy Pork Barbecue" ).PlateID,
                        FoodType = "Lunch"
                    },
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Osteria Francescana").ID,
                        PlateID = plate.Single(c => c.Name == "Cheese & Bacon" ).PlateID,
                        FoodType = "Breakfast"
                    },
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Mirazur").ID,
                        PlateID = plate.Single(c => c.Name == "Brocoli Pesto Pasta" ).PlateID,
                        FoodType = "Lunch"
                    },
                    new Serve {
                        RestaurantID = restaurant.Single(s => s.Name == "Central").ID,
                        PlateID = plate.Single(c => c.Name == "Meatball Spiders" ).PlateID,
                        FoodType = "Lunch"
                    }

                };
                serves.ForEach(s => context.Serves.Add(s));
                /*
                foreach (Serve e in serves)
                {
                    var serveInDataBase = context.Serves.Where(
                        s =>
                        s.Restaurants.ID == e.RestaurantID &&
                        s.Plates.PlateID == e.PlateID).SingleOrDefault();
                    if (serveInDataBase == null)
                    {
                        context.Serves.Add(e);
                    }
                }*/
                context.SaveChanges();
            }
        }
    }
}
