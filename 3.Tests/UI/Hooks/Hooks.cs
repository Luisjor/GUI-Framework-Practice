using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Allure.Commons;
using OpenQA.Selenium;
using RestSharp;
using TechTalk.SpecFlow;
using Todoly.Core.Helpers;
using Todoly.Core.UIElements.Drivers;
using Todoly.Views.WebAppPages;

namespace SeleniumTest.Tests.Hooks;

[Binding]
public class Hooks
{
    private readonly ScenarioContext _scenarioContext;
    private readonly string _projectName;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _projectName = IdHelper.GetNewId();
    }

    [AfterStep]
    public static void AfterStep(ScenarioContext context)
    {
        if (context.TestError != null)
        {
            byte[] content = GetScreenshot();
            AllureLifecycle.Instance.AddAttachment(
                "Failed test screenshot",
                "image/png",
                content
            );
        }
    }

    private static byte[] GetScreenshot()
    {
        return ((ITakesScreenshot)GenericWebDriver.Instance).GetScreenshot().AsByteArray;
    }

    [AfterScenario]
    public void CaptureScreenshot()
    {
        if (_scenarioContext.TestError == null)
        {
            return;
        }

        var image = ((ITakesScreenshot)GenericWebDriver.Instance).GetScreenshot();
        var scenarioTitle = _scenarioContext.ScenarioInfo.Title;
        var path = $"../../../Assets/{scenarioTitle}";
        path = string.Concat(path.Split().Select(w => $"{char.ToUpper(w[0])}{w.Substring(1)}"));

        Directory.CreateDirectory(path);

        var fileName = DateTime.Now.ToString("yyyyMMddTHHmmss");
        var filePath = Path.Combine(path, $"{fileName}.png");

        image.SaveAsFile(filePath, ScreenshotImageFormat.Png);

        GenericWebDriver.Dispose();
    }

    [AfterScenario]
    public void SessionDisposal(ScenarioContext context)
    {
        ConfigLogger.Information($"Ending {context.ScenarioInfo.Title} scenario", context);
        ConfigLogger.Information($"Disposing driver", context);
        ConfigLogger.Instance = null!;

        GenericWebDriver.Dispose();
    }
}
