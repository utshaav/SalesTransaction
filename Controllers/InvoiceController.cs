using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Data;
using SalesTransaction.Interfaces;
using SalesTransaction.Models;

namespace SalesTransaction.Controllers;
public class InvoiceController : Controller
{
    private readonly IDropDownHelpers dropDown;
    private readonly ISalesTransactionServices salesServices;
    private readonly ICustomerServices customerServices;

    public InvoiceController(IDropDownHelpers dropDown, ISalesTransactionServices salesServices, ICustomerServices customerServices)
    {
        this.customerServices = customerServices;
        this.salesServices = salesServices;
        this.dropDown = dropDown;
    }

    public IActionResult Index()
    {
        ViewBag.CustomerDD = dropDown.getCustomerDD();
        return View();
    }

    public IActionResult UnbilledSales(int customerId)
    {
        ViewBag.CustomerId = customerId;
        List<SalesTransactionVM> unbilledSales = salesServices.SelectAllByCustomerId(customerId);
        return PartialView(unbilledSales);
    }

    [HttpPost]
    public IActionResult Bill(InvoiceVM invoice)
    {
        if (invoice.SalesIds == null)
            return RedirectToActionPermanent("index");
        string idCsv = String.Empty;
        foreach (int id in invoice.SalesIds)
        {
            idCsv += id.ToString() + ",";
        }
        List<SalesTransactionVM> sales = salesServices.GenerateInvoice(idCsv, invoice.CustomerId).ToList();
        Customer customer = customerServices.SelectById(invoice.CustomerId);
        ViewBag.CustomerName = customer.Name;
        ViewBag.CustomerAddress = customer.Address;
        return View(sales);
    }





}