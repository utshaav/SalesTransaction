using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesTransaction.Data;
using SalesTransaction.Models;
using SalesTransaction.Services;

namespace SalesTransaction.Controllers;
public class SalesTransactionController : Controller
{
    SalesTransactionServices saleServices;
    CustomerServices customerServices;
    ProductServices productServices;
    public SalesTransactionController(TestDBContext context)
    {
        saleServices = new SalesTransactionServices(context);
        customerServices = new CustomerServices(context);
        productServices = new ProductServices(context);
    }
    public IActionResult Index()
    {
        List<SalesTransactionVM> sales = saleServices.SelectAll();
        ViewBag.ProductDD = getProductDD();
        ViewBag.CustomerDD = getCustomerDD();
        return View(sales);
    }

    public IActionResult Detail(int id)
    {
        Sale sale = saleServices.SelectById(id);
        return View(sale);
    }
    public IActionResult Add()
    {
        ViewBag.ProductDD = getProductDD();
        ViewBag.CustomerDD = getCustomerDD();
        return PartialView(new Sale());
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Sale sale)
    {
        if (ModelState.IsValid)
        {
            await saleServices.CreateAsync(sale);
            return RedirectToActionPermanent("Index");
        }
        ViewBag.ProductDD = getProductDD();
        ViewBag.CustomerDD = getCustomerDD();
        return View();
    }

    public IActionResult Update(int id)
    {
        ViewBag.ProductDD = getProductDD();
        ViewBag.CustomerDD = getCustomerDD();
        Sale sale = saleServices.SelectById(id);
        return PartialView(sale);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Sale sale)
    {
        if (ModelState.IsValid)
        {
            await saleServices.UpdateAsync(sale);
            // Console.WriteLine(result);
            return RedirectPermanent("Index");
        }
        ViewBag.ProductDD = getProductDD();
        ViewBag.CustomerDD = getCustomerDD();
        return View(sale);
    }

    public IActionResult Delete(int id)
    {
        Sale sale = saleServices.SelectById(id);
        return PartialView(sale);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Sale sale)
    {
        await saleServices.DeleteById(sale.Id);
        return RedirectToActionPermanent("Index");
    }

    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await saleServices.DeleteById(id);
        return RedirectToActionPermanent("Index");
    }

    [NonAction]
    private List<SelectListItem> getCustomerDD()
    {
        List<Customer> customers = customerServices.SelectAll();
        List<SelectListItem> customerDD = new List<SelectListItem>();
        foreach (Customer customer in customers)
        {
            customerDD.Add(new SelectListItem { Value = customer.Id.ToString(), Text = customer.Name });
        }
        return customerDD;
    }

    
    [NonAction]
    private List<SelectListItem> getProductDD()
    {
        List<Product> products = productServices.SelectAll();
        List<SelectListItem> productDD = new List<SelectListItem>();
        foreach (Product product in products)
        {
            productDD.Add(new SelectListItem { Value = product.Id.ToString(), Text = product.Name });
        }
        return productDD;
    }

}
