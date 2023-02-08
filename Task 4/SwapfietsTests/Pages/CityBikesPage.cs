using System.Collections.Generic;
using System.Linq;
using SwapfietsTests.Extensions;
using OpenQA.Selenium;

namespace SwapfietsTests.Pages;

public class CityBikesPage : BasePage
{
    private string CityHeader => Driver.GetElement(CityHeaderLocator).Text;
    private By CityHeaderLocator => By.XPath("//h1[contains(@class, 'Typography')]");
    private By NameLocator => By.XPath(".//a/h2");
    private By SubscribeButtonLocator => By.XPath(".//a[@data-test-id='product-cta-link']");
    private By PriceLocator => By.XPath(".//p[contains(@class, 'ProductCard_price')]");
    private By MoreDetailsLocator => By.XPath(".//a[@data-test-id='product-more-details-link']");
    public List<IWebElement> Bicycles => Driver.FindElements(By.XPath("//li[@data-test-id='city-product-list-item']")).ToList();

    public CityBikesPage(WebDriver driver) : base(driver) { }

    public override bool Displayed()
    {
        return Driver.IsElementDisplayed(CityHeaderLocator) && Bicycles.Any();
    }

    public bool PageDisplayedForCity(string city)
    {
        return CityHeader.Contains(city);
    }

    public bool SubscribeButtonDisplayedForAllBicycle()
    {
        return Bicycles.TrueForAll(e => e.GetElement(SubscribeButtonLocator).Displayed);
    }

    public bool PriceDisplayedForAllBicycle()
    {
        return Bicycles.TrueForAll(e => !string.IsNullOrWhiteSpace(e.GetElement(PriceLocator).Text));
    }

    public bool MoreDetailsDisplayedForAllBicycle()
    {
        return Bicycles.TrueForAll(e => e.GetElement(MoreDetailsLocator).Displayed);
    }

    public SubscribePage SubscribeBicycle(string bicycleType)
    {
        var bicycle = Bicycles.Single(b => b.GetElement(NameLocator).Text.Equals(bicycleType));
        return bicycle.GetElement(SubscribeButtonLocator).ClickElement<SubscribePage>(Driver);
    }
}