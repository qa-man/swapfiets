using OpenQA.Selenium;
using SwapfietsTests.Helpers;

namespace SwapfietsTests.Pages;

public abstract class BasePage
{
    protected WebDriver Driver;

    protected BasePage(WebDriver driver)
    {
        Driver = driver;
        Driver.WaitForLoading();
    }
    
    public abstract bool Displayed();
    
}