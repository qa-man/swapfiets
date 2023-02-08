using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SwapfietsTests.Helpers;

public static class DriverHelper
{
    public static WebDriver Driver { get { if (_driver is null) SetupBrowser(TestRunHelper.Browser); return _driver; } }

    private static WebDriver _driver;

    private static List<string> ChromiumArguments => new()
        {
            "-inprivate",
            "--start-maximized",
            "allow-running-insecure-content",
            "test-type",
            "ignore-certificate-errors",
            "disable-extensions",
        "enable-precise-memory-info",
            "js-flags=--expose-gc",
            "--no-sandbox",
            "lang=en-GB"
        };

    public static WebDriver SetupBrowser(Enums.Browser browser)
    {

        var driverManager = new DriverManager();

        switch (browser)
        {
            case Enums.Browser.chrome:
                driverManager.SetUpDriver(new ChromeConfig());

                var driverService = ChromeDriverService.CreateDefaultService();
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments(ChromiumArguments);
                chromeOptions.AddExcludedArgument("enable-automation");
                chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

                _driver = new ChromeDriver(driverService, chromeOptions);

                break;

            case Enums.Browser.edge:
                driverManager.SetUpDriver(new EdgeConfig());

                var edgeDriverService = EdgeDriverService.CreateDefaultService();
                var edgeOptions = new EdgeOptions();
                edgeOptions.AddArguments(ChromiumArguments);
                edgeOptions.AddExcludedArgument("enable-automation");
                edgeOptions.BinaryLocation = $"{ConfigHelper.PathToEdge}";

                _driver = new EdgeDriver(edgeDriverService, edgeOptions);
                break;
            default:
                goto case Enums.Browser.chrome;
        }

        return Driver;
    }

    public static bool WaitForLoading(this WebDriver driver, double timeout = 30, double step = 0.5, bool waitJs = true)
    {
        bool WaitForJavaScript()
        {
            return driver.ExecuteScript("return document.readyState").Equals("complete");
        }

        return WaitHelper.WaitFor(WaitForJavaScript, timeout, step);
    }

    public static void Quit() => _driver.Dispose();
}