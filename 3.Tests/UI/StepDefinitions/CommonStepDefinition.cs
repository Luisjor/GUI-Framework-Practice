﻿using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;
using Todoly.Core.Helpers;
using Todoly.Core.UIElements.Drivers;
using Todoly.Core.UIElements.WebActions;
using Todoly.Views.Helpers;
using Todoly.Views.WebAppPages;

namespace Todoly.Tests.UI.Steps.Commons;

[Binding]
public class CommonSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly LoginPage _loginPage = new LoginPage();

    private string? _currentView;
    public string CurrentView
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_currentView))
            {
                throw new Exception("No view name was specified");
            }

            return _currentView!;
        }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _currentView = value;
            }
        }
    }

    public CommonSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"the user is logged in")]
    public void Login()
    {
        _loginPage.LoginIntoApplication();
    }

    [When(@"(?:the user )?clicks on '([a-zA-Z ]+)'(?: at '([a-zA-Z ]+)')?$")]
    public void Click(string elementName, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        UIElementFactory.GetElement(elementName, CurrentView).Click();
    }

    [When(@"the user clicks on '(.*)' '(.*)' at '(.*)'")]
    public void Click(string elementName, string locatorArgument, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        UIElementFactory.GetElement(elementName, CurrentView, locatorArgument).Click();
    }

    [When(@"(?:the user )?types ""(.*)"" on '([a-zA-Z ]+)'(?: at '([a-zA-Z ]+)')?$")]
    public void Type(string input, string elementName, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        if (input == "Password Credential")
        {
            UIElementFactory.GetElement(elementName, CurrentView).Type(ConfigModel.WEB_PASS);
        }
        else
        {
            if (elementName == "New Password")
            {
                _scenarioContext.Add("Password", input);
            }
            else if (elementName == "Email")
            {
                _scenarioContext.Add("Email", input);
            }

            UIElementFactory.GetElement(elementName, CurrentView).Type(input);
        }
    }

    [When(@"enters the item ""(.*)"" on ""(.*)"" input")]
    public void Whentheuserclicks(string itemName, string elementName)
    {
        UIElementFactory.GetElement(elementName, "Items Component").Type(itemName);
        UIElementFactory.GetElement(elementName, "Items Component").Type(Keys.Enter);
    }


    [When(@"the user drags and drop '([\w ]+)' '([\w ]+)' (above|on top of) '([\w ]+)'(?: at '([a-zA-Z ]+)')?$")]
    public void DragAndDrop(string locatorArgument1, string elementName, string placeToDrop, string locatorArgument2, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        int x = 0, y = 0;
        if (placeToDrop == "above")
        {
            y = -1;
        }
        else if (placeToDrop == "on top of")
        {
            x = 15;
            y = 1;
        }

        var source = UIElementFactory.GetElement(elementName, CurrentView, locatorArgument1).WebElement;
        var target = UIElementFactory.GetElement(elementName, CurrentView, locatorArgument2).WebElement;

        WebActions.DragAndDrop(source, target, x, y);
    }


    [Then(@"the '(.*)' should (not )?be displayed(?: at '([a-zA-Z ]+)')?$")]
    public void ValidateDisplay(string elementName, string display, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        if (display == "not ")
        {
            Assert.True(UIElementFactory.GetElement(elementName, CurrentView).IsInvisible());
        }
        else
        {
            Assert.True(UIElementFactory.GetElement(elementName, CurrentView).WebElement.Displayed);
        }
    }

    [Then(@"the '(.*)' ""(.*)"" should (not )?be displayed(?: at '([a-zA-Z ]+)')?$")]
    public void ValidateDisplay(
        string elementName,
        string locatorArgument,
        string display,
        string viewName
    )
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        if (display == "not ")
        {
            Assert.True(UIElementFactory.GetElement(elementName, CurrentView).IsInvisible());
        }
        else
        {
            Assert.True(
                UIElementFactory
                    .GetElement(elementName, CurrentView, locatorArgument)
                    .WebElement.Displayed
            );
        }
    }

    [When(@"(?:the user )?hovers on '([a-zA-Z ]+)'(?: at '([a-zA-Z ]+)')?$")]
    public void Hover(string elementName, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        WebActions.HoverElement(UIElementFactory.GetElement(elementName, CurrentView).WebElement);
    }

    [When(@"(?:the user )?hovers on ""([\w ]+)""(?: <([\w ]+)>)?(?: at '([\w ]+)')?$")]
    public void HoverItemName(string elementName, string itemName, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        WebActions.HoverElement(
            UIElementFactory.GetElement(elementName, CurrentView, itemName).WebElement
        );
    }

    [Then(@"the main title text is ""(.*)""")]
    public void Thenthemaintitletextis(string expectedTitle)
    {
        GenericWebDriver.Wait.Until(
            ExpectedConditions.TextToBePresentInElement(
                UIElementFactory.GetElement("Current Project Title", CurrentView).WebElement,
                expectedTitle
            )
        );
    }

    [Then(@"the snack bar message is '(.*)' at '([a-zA-Z ]+)'")]
    public void Giventhesnackbarmessageis(string expectedMessage, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        Assert.That(
            UIElementFactory.GetElement("Information Message", CurrentView).WebElement.Text,
            Is.EqualTo(expectedMessage)
        );
    }

    [Then(@"an alert should appear with the message ""(.*)""")]
    public void VerifyAlertMessage(string message)
    {
        var alert = GenericWebDriver.Wait.Until(ExpectedConditions.AlertIsPresent());
        Assert.That(message, Is.EqualTo(alert.Text));
    }

    [When(@"(?:the user )?accepts the alert")]
    public void AcceptAlert()
    {
        GenericWebDriver.AcceptAlert();
    }


    [Then(@"the '(.*)' value is updated with '(.*)' at '(.*)'")]
    public void VerifyElementValueUpdate(string elementName, string newValue, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        Assert.That(
            UIElementFactory.GetElement(elementName, CurrentView).WebElement.GetAttribute("value"),
            Is.EqualTo(newValue)
        );
    }

    [When(@"the '(.*)' is selected at '(.*)'")]
    public void VerifyElementIsSelected(string elementName, string viewName)
    {
        if (viewName != null)
        {
            CurrentView = viewName;
        }

        Assert.That(
            UIElementFactory
                .GetElement(elementName, CurrentView)
                .WebElement.GetAttribute("selected"),
            Is.EqualTo("true")
        );
    }
}
