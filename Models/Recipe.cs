using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Recipes_Final_Project.Models
{
    //Keeps Recipe details inclusing instructions and price.
    public class Recipe
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Instructions { get; set; }

        [NotMapped]
        public IFormFile RecipeImage { get; set; }

        public string RecipeImageLocation { get; set; }

        public decimal  Price { get; set;  }


    }
}
