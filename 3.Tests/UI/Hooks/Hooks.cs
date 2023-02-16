﻿using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using OpenQA.Selenium;
using RestSharp;
using TechTalk.SpecFlow;
using Todoly.Core.Helpers;
using Todoly.Core.UIElements.Drivers;
using Todoly.Views.Models;

namespace SeleniumTest.Tests.Hooks;

[Binding]
public class Hooks
{
    private readonly ScenarioContext _scenarioContext;
    private readonly RestHelper _client;
    private readonly string _urlProject;
    private readonly string _projectName;
    private readonly string _urlItem;
    private readonly int? _projectId;
    private readonly string _itemName;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _client = new RestHelper(ConfigModel.ApiHostUrl);
        _urlProject = ConfigModel.ProjectUri;
        _projectName = IdHelper.GetNewId();
        _urlItem = ConfigModel.ItemUri;
        _itemName = IdHelper.GetNewId();

        if (_scenarioContext.TryGetValue("projectContent", out var projectContentData))
        {
            ProjectModel projectContent = (ProjectModel)projectContentData;
            _projectId = projectContent.Id;
        }
    }

    [AfterTestRun]
    public static void CleanUp()
    {
        APIScripts.RemoveAllProjects();
    }

    [BeforeScenario("create.project", Order = 1)]
    public void CreateProject()
    {
        string payload = $"{{ \"Content\": \"{_projectName}\" }}";

        _client.AddAuthenticator(ConfigModel.TODO_LY_EMAIL, ConfigModel.TODO_LY_PASS);

        RestResponse response = _client.DoRequest(Method.Post, _urlProject, payload);
        ProjectModel? projectModel = JsonSerializer.Deserialize<ProjectModel>(response.Content!);

        _scenarioContext[ConfigModel.CurrentProject] = _projectName;
        _scenarioContext[ConfigModel.CurrentProjectPayload] = projectModel;
    }

    [AfterScenario]
    public void CaptureScreenshot()
    {
        if (_scenarioContext.TestError != null)
        {
            Screenshot image = ((ITakesScreenshot)GenericWebDriver.Instance).GetScreenshot();
            string path = $"../Assets/{_scenarioContext.ScenarioInfo.Title}";
            path = string.Join(" ", path.Split().Select(word => char.ToUpper(word[0]) + word.Substring(1))).Replace(" ", "");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = new string(DateTime.Now.ToString()
                              .Where(c => !char.IsWhiteSpace(c) && c != '/' && c != ':')
                              .ToArray());

            image.SaveAsFile($"{path}/{fileName}.png", ScreenshotImageFormat.Png);
        }

        GenericWebDriver.Dispose();
    }

    [BeforeScenario("create.item", Order = 2)]
    public void CreateAnItem()
    {
        string payload = $"{{ \"Content\": \"{_itemName}\", \"ProjectId\": {_projectId} }}";
        _client.AddAuthenticator(ConfigModel.TODO_LY_EMAIL, ConfigModel.TODO_LY_PASS);

        RestResponse response = _client.DoRequest(Method.Post, _urlItem, payload);
        if (!response.IsSuccessful)
        {
            throw new Exception("Error: Bad Request");
        }

        ItemModel? itemContent = JsonSerializer.Deserialize<ItemModel>(response.Content!);

        if (itemContent!.Content != _itemName)
        {
            throw new Exception("Error: Response field 'Content' does not match input payload");
        }

        _scenarioContext.Add(ConfigModel.CurrentItem, _itemName);
        _scenarioContext.Add("itemContent", itemContent);
    }

    [AfterScenario]
    public void SessionDisposal()
    {
        GenericWebDriver.Dispose();
    }
}
