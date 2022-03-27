using Microsoft.AspNetCore.Mvc.Rendering;
using SalesTransaction.Interfaces;
using SalesTransaction.Models;

namespace SalesTransaction.Helpers
{
    public class DropDownHelpers : IDropDownHelpers
    {
        private readonly ICustomerServices customerServices;
        private readonly IProductServices productServices;

        public DropDownHelpers(ICustomerServices customerServices, IProductServices productServices)
        {
            this.customerServices = customerServices;
            this.productServices = productServices;
        }

        public List<SelectListItem> getCustomerDD()
        {
            List<Customer> customers = customerServices.SelectAll();
            List<SelectListItem> customerDD = new List<SelectListItem>();
            foreach (Customer customer in customers)
            {
                customerDD.Add(new SelectListItem { Value = customer.Id.ToString(), Text = customer.Name });
            }
            return customerDD;
        }

        public List<SelectListItem> getProductDD()
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
}