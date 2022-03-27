using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesTransaction.Interfaces;
public interface IDropDownHelpers
{
    List<SelectListItem> getProductDD();
    List<SelectListItem> getCustomerDD();
}
