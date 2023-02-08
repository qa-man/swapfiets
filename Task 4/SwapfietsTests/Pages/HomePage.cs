using OpenQA.Selenium;
using SwapfietsTests.Extensions;
using SwapfietsTests.Helpers;

namespace SwapfietsTests.Pages;

public class HomePage : BasePage
{
    private By CoockieAllowanceButtonLocator => By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
    private By CitySearchInputLocator => By.Id("city-select-18");
    private By SeeBikesButtonLocator => By.XPath("//button[@data-test-id='city-selector-submit']");
    private IWebElement AppStoreAppLinkLocator => Driver.GetElement(By.XPath("//a[contains(@href, 'https://apps.apple.com/us/app/swapfiets/id1330923084')]"));
    private IWebElement GooglePlayLinkLocator => Driver.GetElement(By.XPath("//a[contains(@href, 'https://play.google.com/store/apps/details?id=com.swapfiets')]"));
    private IWebElement MenuButton => Driver.GetElement(By.XPath("//button[@data-test-id='sidemenu-hamburger']"));
    private IWebElement CoockieAllowanceButton => Driver.GetElement(CoockieAllowanceButtonLocator);
    private IWebElement SeeBikesButton => Driver.GetElement(SeeBikesButtonLocator);
    private IWebElement CitySearchInput => Driver.GetElement(CitySearchInputLocator);

    public HomePage(WebDriver driver) : base(driver) { AllowCookiesIfDisplayed(); }

    public override bool Displayed()
    {
        return Driver.Url.Equals(ConfigHelper.GbUrl) && Driver.IsElementDisplayed(CitySearchInputLocator);
    }

    public CityBikesPage SearchByCity(string city)
    {
        CitySearchInput.EnterText(city);
        return SeeBikesButton.ClickElement<CityBikesPage>(Driver);
    }

    private void AllowCookiesIfDisplayed()
    {
        if(Driver.IsElementDisplayed(CoockieAllowanceButtonLocator)) CoockieAllowanceButton.ClickElement();
    }

    public bool ContentDisplayed()
    {
        AppStoreAppLinkLocator.ScrollToElement();
        var menuDisplayed = MenuButton.Displayed;
        var appStoreLinkDisplayed = AppStoreAppLinkLocator.Displayed;
        var googlePlayLinkDisplayed = GooglePlayLinkLocator.Displayed;

        return menuDisplayed && appStoreLinkDisplayed && googlePlayLinkDisplayed;
    }
}