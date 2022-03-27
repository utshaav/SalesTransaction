using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Data;
using SalesTransaction.Interfaces;
using SalesTransaction.Models;
using SalesTransaction.Services;

namespace SalesTransaction.Controllers;
public class ProductController : Controller
{
    private readonly IProductServices productServices;
    public ProductController(IProductServices productServices)
    {
        this.productServices = productServices;
    }
    public IActionResult Index()
    {
        List<Product> products = productServices.SelectAll();
        return View(products);
    }

    public IActionResult Detail(int id)
    {
        Product product = productServices.SelectById(id);
        return View(product);
    }
    public IActionResult Add()
    {
        return PartialView(new Product());
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Product product)
    {
        if (ModelState.IsValid)
        {
            await productServices.CreateAsync(product);
            return RedirectToActionPermanent("Index");
        }
        return View();
    }

    public IActionResult Update(int id)
    {
        Product product = productServices.SelectById(id);
        return PartialView(product);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Product product)
    {
        if (ModelState.IsValid)
        {
            await productServices.UpdateAsync(product);
            // Console.WriteLine(result);
            return RedirectPermanent("Index");
        }
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        Product product = productServices.SelectById(id);
        return PartialView(product);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Product product)
    {
        await productServices.DeleteById(product.Id);
        return RedirectToActionPermanent("Index");
    }

    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await productServices.DeleteById(id);
        return RedirectToActionPermanent("Index");
    }

}
