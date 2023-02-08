using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SwapfietsTests.Extensions
{
    public static class DriverExtensions
    {
        public static IWebElement GetElement(this IWebDriver driver, By locator, double timeLimitInSeconds = 30)
        {
            var element = WaitElementToBeDisplayed(driver, locator, timeLimitInSeconds).FindElement(locator);
            return element;
        }

        public static IWebDriver WaitElementToBeDisplayed(this IWebDriver driver, By locator, double timeLimitInSeconds = 30)
        {
            if (IsElementDisplayed(driver, locator, timeLimitInSeconds)) return driver;
            throw new NotFoundException($"Element with locator '{locator}' has NOT been found.");
        }

        public static bool IsElementDisplayed(this IWebDriver driver, By locator, double timeLimitInSeconds = 1)
        {
            try
            {
                var result = IsElementExist(driver, locator, timeLimitInSeconds) && WaitElementCondition(driver, SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator), timeLimitInSeconds);
                return result;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        #region Private Methods

        private static bool IsElementExist(this IWebDriver driver, By locator, double timeLimitInSeconds = 1)
        {
            try
            {
                WaitElementCondition(driver, SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator), timeLimitInSeconds);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

        private static bool WaitElementCondition(IWebDriver driver, Func<IWebDriver, IWebElement> condition, double timeLimitInSeconds = 1)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeLimitInSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            wait.IgnoreExceptionTypes(typeof(WebDriverException));
            return wait.Until(condition) is not null;
        }

        #endregion
    }
}