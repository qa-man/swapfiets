using System;
using OpenQA.Selenium;
using SwapfietsTests.Helpers;

namespace SwapfietsTests.Extensions
{
    public static class ElementExtensions
    {
        public static void ClickElement(this IWebElement element)
        {
            element.Click();
        }

        public static T ClickElement<T>(this IWebElement element, object parameter)
        {
            element.ClickElement();
            return (T)Activator.CreateInstance(typeof(T), parameter);
        }

        public static IWebElement GetElement(this IWebElement parent, By locator, double timeLimitInSeconds = 30)
        {
            DriverHelper.Driver.WaitElementToBeDisplayed(locator, timeLimitInSeconds);
            return parent.FindElement(locator);
        }

        public static void EnterText(this IWebElement element, string text)
        {
            element.ClickElement();
            if(!string.IsNullOrWhiteSpace(element.Text)) element.Clear();
            element.SendKeys(text);
        }

        public static void ScrollToElement(this IWebElement element)
        {
            DriverHelper.Driver.ExecuteScript("arguments[0].focus();", element);
            DriverHelper.Driver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

    }
}