using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_Recipes_Final_Project.Models
{
    //Keeps the customer information such as name and email
    public class Customer
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Email{ get; set;  }
    }
}
