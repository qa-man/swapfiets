using OpenQA.Selenium;
using SwapfietsTests.Extensions;
using SwapfietsTests.Helpers;

namespace SwapfietsTests.Pages;

public class RegistrationPage : BasePage
{
    private IWebElement FirstNameInput => Driver.GetElement(By.XPath("//input[@name='firstname']"));
    private IWebElement LastNameInput => Driver.GetElement(By.XPath("//input[@name='lastname']"));
    private By TermsCheckBoxLocator => By.XPath("//label[@for='terms-checkbox']");
    private By NextButtonLocator => By.XPath("//button[@type='submit']");
    private IWebElement CountryCode => Driver.GetElement(By.XPath("//select[@name='telephonePrefix']"));
    private IWebElement BackButton => Driver.GetElement(By.XPath("//div[@class='back-button']"));
    private IWebElement TermsCheckBox => Driver.GetElement(TermsCheckBoxLocator);

    public IWebElement NextButton => Driver.GetElement(NextButtonLocator);

    public RegistrationPage(WebDriver driver) : base(driver) { }

    public override bool Displayed()
    {
        Driver.WaitForLoading();
        return Driver.Url.Contains("registration/enroll") && FirstNameInput.Displayed;
    }

    public void FillFirstName(string firstName)
    {
        FirstNameInput.ScrollToElement();
        FirstNameInput.EnterText(firstName);
    }

    public void FillLastName(string lastName)
    {
        LastNameInput.EnterText(lastName);
    }

    public void AcceptTerms()
    {
        TermsCheckBox.ScrollToElement();
        TermsCheckBox.ClickElement();
    }

    public bool CountryIsDisplayed(string country)
    {
        CountryCode.ScrollToElement();

        switch (country)
        {
            case "United Kingdom":
                return CountryCode.GetAttribute("value").Equals("+44");
            default:
                return false;

        }
    }

    public HomePage ClickBackButton()
    {
        BackButton.ScrollToElement();
        return BackButton.ClickElement<HomePage>(Driver);
    }
}