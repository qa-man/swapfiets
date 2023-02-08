using System.Linq;
using OpenQA.Selenium;
using SwapfietsTests.Extensions;

namespace SwapfietsTests.Pages;

public class SubscribePage : BasePage
{
    private By OrderBikeButtonLocator => By.XPath("//button[@type='submit']");
    private By BikeNameLocator => By.XPath("//h1[contains(@class, 'ConfigurePage_header')]");
    private By MembershipOptionsLocator => By.XPath("//label[contains(@class, 'ChooseMembership_radioButton')]");
    private By BikeUsageOptionsLocator => By.XPath("//label[contains(@class, 'HeavyUser_button')]");
    private By AlertLocator => By.XPath("//span[contains(@class, 'HeavyUser_error')]");
    
    public IWebElement OrderBikeButton => Driver.GetElement(OrderBikeButtonLocator);

    public SubscribePage(WebDriver driver) : base(driver) { }

    public override bool Displayed()
    {
        return Driver.IsElementDisplayed(OrderBikeButtonLocator) && Driver.IsElementDisplayed(BikeNameLocator);
    }

    public bool IsSpecificBicycleSubscriptionPageDisplayed(string bicycle)
    {
        return Driver.GetElement(BikeNameLocator).Text.Contains(bicycle);
    }

    public bool IsMembershipOptionsDisplayed()
    {
        return Driver.FindElements(MembershipOptionsLocator).Count > 1;
    }

    public bool IsBikeUsageOptionsDisplayed()
    {
        return Driver.FindElements(BikeUsageOptionsLocator).Count > 1;
    }

    public bool IsAlertDisplayed()
    {
        return Driver.IsElementDisplayed(AlertLocator);
    }

    public RegistrationPage ClickOrderBike()
    {
        OrderBikeButton.ScrollToElement();
        return OrderBikeButton.ClickElement<RegistrationPage>(Driver);
    }

    public void SelectBikeUsageOption()
    {
        Driver.FindElements(BikeUsageOptionsLocator).Last().ClickElement();
    }
}