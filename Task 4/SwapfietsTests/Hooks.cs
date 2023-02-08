using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ScreenRecorderLib;
using SwapfietsTests.Helpers;
using TechTalk.SpecFlow;
using TestContext = NUnit.Framework.TestContext;

namespace SwapfietsTests
{
    [Binding]
    public class Hooks
    {
        private static WebDriver _driver;
        private static string TimeStamp => $"{DateTime.Now:dd MMMM yyyy ± HH·mm}";
        private static string _reportPath;
        private static string _uniqueName;
        private static string _scenarioRecording;
        public static TestContext TestContext;
        private static Recorder _videoRecorder;


        [BeforeTestRun]
        public static void Initializer()
        {
            SetupReporting();
            TestContext = TestContext.CurrentContext;
            KillOldDrivers();
        }

        [AfterTestRun]
        public static void Finalizer()
        {
            DriverHelper.Quit();
        }

        [BeforeScenario]
        public static void TestInitialize(ScenarioContext scenarioContext)
        {
            _uniqueName = Path.Combine(_reportPath!, $"{scenarioContext.ScenarioInfo.Title} [{TimeStamp}]");
            _scenarioRecording = $"{_uniqueName}.mp4";

            _driver ??= DriverHelper.SetupBrowser(TestRunHelper.Browser);

            _videoRecorder = Recorder.CreateRecorder();
            _videoRecorder.Record(_scenarioRecording);
        }

        [AfterScenario]
        public static void TestCleanup()
        {
            var result = TestContext.CurrentContext.Result;
            switch (result.Outcome.Status)
            {
                case TestStatus.Failed:
                case TestStatus.Inconclusive:
                case TestStatus.Warning:
                    var screenshot = $"{_uniqueName}.png";
                    _driver!.GetScreenshot().SaveAsFile(screenshot, ScreenshotImageFormat.Png);
                    TestContext.AddTestAttachment(screenshot);
                    break;
            }

            _videoRecorder.Stop();
            TestContext.AddTestAttachment(_scenarioRecording!);
        }

        #region Private Methods

        private static void SetupReporting()
        {
            _reportPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Results", $"{TimeStamp}");
            Directory.CreateDirectory(_reportPath);
        }

        private static void KillOldDrivers()
        {
            Process.GetProcessesByName("chromedriver").ToList().ForEach(oldDriver => oldDriver.Kill());
            Process.GetProcessesByName("msedgedriver").ToList().ForEach(oldDriver => oldDriver.Kill());
        }

        #endregion

    }
}