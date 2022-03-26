using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Data;
using SalesTransaction.Models;
using SalesTransaction.Services;

namespace SalesTransaction.Controllers;
public class CustomerController : Controller
{
    private CustomerServices customerServices; 
    public CustomerController(TestDBContext context)
    {
        customerServices= new CustomerServices(context);
    }
    public IActionResult Index()
    {
        List<Customer> customers = customerServices.SelectAll(); 
        return View(customers);
    }

    public IActionResult Detail(int id)
    {
        Customer customer = customerServices.SelectById(id);
        return View(customer);
    }
    public IActionResult Add()
    {
        return PartialView(new Customer());
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Customer customer)
    {
        if (ModelState.IsValid)
        {
            await customerServices.CreateAsync(customer);
            return RedirectToActionPermanent("Index");
        }
        return View();
    }

    public IActionResult Update(int id)
    {
        Customer customer = customerServices.SelectById(id);
        return PartialView(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Customer customer)
    {
        if (ModelState.IsValid)
        {
            await customerServices.UpdateAsync(customer);
            // Console.WriteLine(result);
            return RedirectPermanent("Index");
        }
        return View(customer);
    }

    public IActionResult Delete(int id)
    {
        Customer customer = customerServices.SelectById(id);
        return PartialView(customer);
    }

    [HttpPost]
    public IActionResult Delete(Customer customer)
    {
        customerServices.DeleteById(customer.Id);
        return RedirectToActionPermanent("Index");
    }

    public IActionResult ConfirmDelete(int id)
    {
        customerServices.DeleteById(id);
        return RedirectToActionPermanent("Index");
    }

    
}