using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Buy_Recipes_Final_Project.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Buy_Recipes_Final_Project.Controllers
{

    //Manages payment information 
  
    public class PaymentsController : Controller
    {
        private readonly Buy_Recipes_DataContext _context;

        SignInManager<IdentityUser> SignInManager;
        UserManager<IdentityUser> UserManager;

        public PaymentsController(Buy_Recipes_DataContext context,
            SignInManager<IdentityUser> SignInManager,
        UserManager<IdentityUser> UserManager
            
            
            )
        {
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
            _context = context;
        }

        // GET: Payments
        //Gets all payments
        [Authorize(Roles = "super_user")]
        public async Task<IActionResult> Index()
        {
            var buy_Recipes_DataContext = _context.Payment.Include(p => p.Customer).Include(p => p.Recipe);
            return View(await buy_Recipes_DataContext.ToListAsync());
        }

        // GET: Payments/Details/5
        //Gets details of the payment
        [Authorize(Roles = "super_user")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Customer)
                .Include(p => p.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        //Creats a new payment customer only action.
        [Authorize(Roles = "customer")]
        public IActionResult Create(int id )
        {

            if (SignInManager.IsSignedIn(User)) {

                var customer = (from cust in _context.Customer
                                where cust.Email.Equals(User.Identity.Name)
                                select cust).FirstOrDefault();

                //Create a payment 

                Payment payment = new Payment();

                payment.CustomerId = customer.Id;
                payment.RecipeId = id;

                _context.Add(payment);
                _context.SaveChanges();

                var savedPayment = _context.Payment
                    .Include(p => p.Customer)
                    .Include(p => p.Recipe).FirstOrDefault(p => p.Id == payment.Id);
                return View(savedPayment);


            }
           
            return View();
        }


       //Gets the  payment for deletetion

        // GET: Payments/Delete/5
        [Authorize(Roles = "super_user")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Customer)
                .Include(p => p.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        //Deletes the payment.
        // POST: Payments/Delete/5
        [Authorize(Roles = "super_user")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
