using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesTransaction.Helpers;
public interface IDropDownHelpers
{
    List<SelectListItem> getProductDD();
    List<SelectListItem> getCustomerDD();
}
