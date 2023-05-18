using Todoly.Core.Helpers;
using Todoly.Core.UIElements.Commons;
using Todoly.Core.UIElements.Enums;
using Todoly.Core.UIElements.Web;
using Todoly.Core.UIElements.WebActions;
using Todoly.Views.WebAppPages.Attributes;

namespace Todoly.Views.WebAppPages;

[View("Login Page")]
public class LoginPage
{
    public readonly string HostUrl = ConfigModel.HostUrl;
    public readonly string EmailCredentials = ConfigModel.WEB_USERNAME;
    public readonly string PassCredentials = ConfigModel.WEB_PASS;

    [Element("Login Button", ElementType.Button)]
    [Locator(LocatorType.Id, "login-button")]
    public Button? LoginButton { get; }

    [Element("Email", ElementType.TextField)]
    [Locator(LocatorType.Id, "user-name")]
    public TextField? EmailTextField { get; }

    [Element("Password", ElementType.TextField)]
    [Locator(LocatorType.Id, "password")]
    public TextField? PasswordTextField { get; }

    public LoginPage() { }

    public void LoginIntoApplication()
    {
        WebActions.NavigateTo(ConfigModel.HostUrl);
        UIElementFactory.GetElement("Email", "Login Page").Type(EmailCredentials);
        UIElementFactory.GetElement("Password", "Login Page").Type(PassCredentials);
        UIElementFactory.GetElement("Login Button", "Login Page").Click();
    }
}
