﻿using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Todoly.Core.Helpers;
using Todoly.Core.UIElements.Commons;
using Todoly.Core.UIElements.Drivers;
using Todoly.Core.UIElements.Interfaces;

namespace Todoly.Core.UIElements.Web
{
    public class Button : BaseWebElement, IButton
    {
        public Button(string name, Locator locator) : base(name, locator) { }

        public void Click()
        {
            try
            {
                GenericWebDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(WebElement));
                WebElement.Click();
            }
            catch (ElementNotVisibleException error)
            {
                ConfigLogger.Error($"Unable to visualize {Name} button with ${Locator.Value}");
                throw error;
            }
            catch (ElementNotInteractableException error)
            {
                ConfigLogger.Error($"Unable to interact with {Name} button with ${Locator.Value}");
                throw error;
            }
        }
    }
}
