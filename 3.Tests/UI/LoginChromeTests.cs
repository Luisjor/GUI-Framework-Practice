﻿using SeleniumTest.Core.Drivers;
using SeleniumTest.Core.Interfaces;
using SeleniumTest.Tests.Steps.Commons;
using TechTalk.SpecFlow;
using Views.WebAppPages;

namespace SeleniumTest.Tests;

[Binding]
[Scope(Feature = "User Login")]
[TestFixture]
public class LoginChromeTests : CommonSteps
{
    private readonly LoginPage _loginPage;
    private readonly IGenericWebDriver _driver;
    private readonly ScenarioContext _scenarioContext;

    public LoginChromeTests(ScenarioContext scenarioContext, ChromeWebDriver driver)
        : base(scenarioContext, driver)
    {
        _scenarioContext = scenarioContext;
        _driver = driver;
        _loginPage = new LoginPage(_driver);
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Dispose();
    }

    [Given(@"the user navigates to the URL")]
    public void Giventheusernavigatesto()
    {
        _scenarioContext.Pending();
    }

    [When(@"the user clicks the login button")]
    public void Whentheuserclickstheloginbutton()
    {
        _scenarioContext.Pending();
    }

    [Given(@"introduces his credentials")]
    public void Givenintroduceshiscredentials()
    {
        _scenarioContext.Pending();
    }

    [Then(@"the user should be able to see the main page")]
    public void Thentheusershouldbeabletoseethemainpage()
    {
        _scenarioContext.Pending();
    }
}
