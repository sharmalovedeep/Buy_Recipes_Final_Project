using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Recipes_Final_Project.Models
{
    //Keeps payment info including customer and recipe information
    public class Payment
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public int CustomerId { get; set; }

        public Recipe Recipe { get; set; }

        public Customer Customer { get; set; }

    }
}
