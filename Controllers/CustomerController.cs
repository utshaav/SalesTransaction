using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Data;
using SalesTransaction.Interfaces;
using SalesTransaction.Models;
using SalesTransaction.Services;

namespace SalesTransaction.Controllers;
public class CustomerController : Controller
{
    private readonly ICustomerServices customerServices;
    public CustomerController(ICustomerServices customerServices)
    {
        this.customerServices = customerServices;

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
    public async Task<IActionResult> Delete(Customer customer)
    {
        await customerServices.DeleteById(customer.Id);
        return RedirectToActionPermanent("Index");
    }

    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await customerServices.DeleteById(id);
        return RedirectToActionPermanent("Index");
    }


}