using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Buy_Recipes_Final_Project.Models;

namespace Buy_Recipes_Final_Project.Models
{
    public class Buy_Recipes_DataContext : DbContext
    {
        public Buy_Recipes_DataContext (DbContextOptions<Buy_Recipes_DataContext> options)
            : base(options)
        {
        }

        public DbSet<Buy_Recipes_Final_Project.Models.Customer> Customer { get; set; }

        public DbSet<Buy_Recipes_Final_Project.Models.Payment> Payment { get; set; }

        public DbSet<Buy_Recipes_Final_Project.Models.Recipe> Recipe { get; set; }
    }
}
