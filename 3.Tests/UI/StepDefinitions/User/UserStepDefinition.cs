using NUnit.Framework.Constraints;
using TechTalk.SpecFlow;
using Todoly.Core.Helpers;
using Todoly.Core.UIElements.Drivers;
using Todoly.Tests.UI.Steps.Commons;
using Todoly.Views.WebAppPages;

namespace SeleniumTest.Tests;

[Binding]
[Scope(Feature = "User")]
public class UserStepDefinitions : CommonSteps
{
    private readonly ScenarioContext _scenarioContext;

    public UserStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"the user navigates to the URL")]
    public void GivenTheUserNavigatesToTheURL()
    {
        GenericWebDriver.Instance.Navigate().GoToUrl(ConfigModel.HostUrl);
    }

    [When(@"the user introduces his credentials")]
    public void IntroduceCredentials()
    {
        UIElementFactory.GetElement("Email", "Login Page").Type(ConfigModel.WEB_USERNAME);
        UIElementFactory.GetElement("Password", "Login Page").Type(ConfigModel.WEB_PASS);
    }
}
