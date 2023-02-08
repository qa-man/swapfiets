using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SwapfietsTests.Helpers
{
    public static class WaitHelper
    {
        static WebDriverWait _wait;

        static WaitHelper()
        {
            _wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(30)) { PollingInterval = TimeSpan.FromMicroseconds(100), };
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public static bool WaitFor(Func<bool> func, double timeout = 10, double step = 1)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                Thread.Sleep(TimeSpan.FromSeconds(step));
                if (func.Invoke()) return true;
            }
            while (stopwatch.Elapsed.TotalSeconds < timeout);

            return false;
        }
    }
}