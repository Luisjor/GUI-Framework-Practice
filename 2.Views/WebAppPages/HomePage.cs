using Todoly.Core.Helpers;
using Todoly.Core.UIElements.Commons;
using Todoly.Core.UIElements.Enums;
using Todoly.Core.UIElements.Interfaces;
using Todoly.Core.UIElements.Web;
using Todoly.Views.WebAppPages.Attributes;

namespace Todoly.Views.WebAppPages;

[View("Home Page")]
public class HomePage
{
    public readonly string HostUrl = ConfigModel.HostUrl;

    [Element("Burger Menu", ElementType.Button)]
    [Locator(LocatorType.Id, "react-burger-menu-btn")]
    public Button? BurgerMenu  { get; }

    [Element("Logout Button", ElementType.Button)]
    [Locator(LocatorType.Id, "logout_sidebar_link")]
    public Button? LogoutButton  { get; }

    [Element("Item Card", ElementType.Button)]
    [Locator(LocatorType.XPath, "//div[@class='inventory_item_name' and text()='{0}']")]
    public Button? ItemCard  { get; }

    [Element("Large Item Card", ElementType.Button)]
    [Locator(LocatorType.XPath, "//div[contains(@class, 'large_size')  and text()='{0}']")]
    public Button? LargeItemCard  { get; }


}
