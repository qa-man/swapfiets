using System;
using NUnit.Framework;
using OpenQA.Selenium;
using SwapfietsTests.Extensions;
using SwapfietsTests.Helpers;
using SwapfietsTests.Pages;
using TechTalk.SpecFlow;

namespace SwapfietsTests.StepDefinitions
{
    [Binding]
    public sealed class StepDefinitions
    {
        private WebDriver Driver => DriverHelper.Driver;
        private HomePage _homePage;
        private CityBikesPage _cityBikesPage;
        private SubscribePage _subscribePage;
        private RegistrationPage _registrationPage;

        #region Given

        [Given(@"Swapfiets main page opened")]
        public void GivenSwapfietsMainPageOpened()
        {
            Driver.Navigate().GoToUrl(ConfigHelper.Url);
            _homePage = new HomePage(Driver);
        }

        #endregion

        #region When

        [When(@"User performs search by ([^']*) using search field")]
        public void WhenUserPerformsSearchByQueryUsingSearchField(string city)
        {
            _cityBikesPage = _homePage.SearchByCity(city);
        }

        [When(@"User clicks on 'Subscribe' button for ([^']*) item")]
        public void WhenUserClicksOnButtonForPowerItem(string bicycle)
        {
            _subscribePage = _cityBikesPage.SubscribeBicycle(bicycle);
        }

        [When(@"User clicks 'Order bike' button")]
        public void WhenUserClicksButton()
        {
            _subscribePage.ClickOrderBike();
        }

        [When(@"User clicks 'Order bike' button to enroll")]
        public void WhenUserClicksOrderBikeButton()
        {
            _registrationPage = _subscribePage.ClickOrderBike();
        }

        [When(@"User selects 'Bike usage' option")]
        public void WhenUserSelectsOption()
        {
            _subscribePage.SelectBikeUsageOption();
        }

        [When(@"User fills details")]
        public void WhenUserFillsDetails()
        {
            _registrationPage.FillFirstName($"{Guid.NewGuid()}");
            _registrationPage.FillLastName($"{Guid.NewGuid()}");
        }

        [When(@"Check 'Terms and Conditions' checkbox")]
        public void WhenCheckCheckbox()
        {
            _registrationPage.AcceptTerms();
        }

        [When(@"User clicks 'Back' button")]
        public void WhenUserClicksBackButton()
        {
            _homePage = _registrationPage.ClickBackButton();
        }


        #endregion

        #region Then

        [Then(@"Search results displayed on screen")]
        public void ThenSearchResultsDisplayedOnScreen()
        {
            CollectionAssert.IsNotEmpty(_cityBikesPage.Bicycles, "No one bicycle displayed after search");
        }

        [Then(@"Price is displayed for all search result items")]
        public void ThenPriceIsDisplayedForAllSearchResultItems()
        {
            Assert.IsTrue(_cityBikesPage.PriceDisplayedForAllBicycle());
        }

        [Then(@"([^']*) location is displayed on search result page")]
        public void ThenQueryLocationIsDisplayedOnSearchResultPage(string city)
        {
            Assert.IsTrue(_cityBikesPage.PageDisplayedForCity(city), "Incorrect location displayed on page after search");
        }

        [Then(@"'Subscribe' button is displayed for each search result item")]
        public void ThenButtonIsDisplayedForEachSearchResultItem()
        {
            Assert.IsTrue(_cityBikesPage.SubscribeButtonDisplayedForAllBicycle());
        }

        [Then(@"'More details' link is displayed for each search result item")]
        public void ThenLinkIsDisplayedForEachSearchResultItem()
        {
            Assert.IsTrue(_cityBikesPage.MoreDetailsDisplayedForAllBicycle());
        }

        [Then(@"([^']*) details page is displayed")]
        public void ThenPowerDetailsPageIsDisplayed(string bicycle)
        {
            Assert.IsTrue(_subscribePage.IsSpecificBicycleSubscriptionPageDisplayed(bicycle));
        }

        [Then(@"Membership options displayed")]
        public void ThenMembershipOptionsDisplayed()
        {
            Assert.IsTrue(_subscribePage.IsMembershipOptionsDisplayed());
        }

        [Then(@"'Bike usage' options displayed")]
        public void ThenOptionsDisplayed()
        {
            Assert.IsTrue(_subscribePage.IsBikeUsageOptionsDisplayed());
        }

        [Then(@"'Order bike' button is displayed")]
        public void ThenButtonIsDisplayed()
        {
            Assert.IsTrue(_subscribePage.OrderBikeButton.Displayed && _subscribePage.OrderBikeButton.Enabled);
        }

        [Then(@"Warning about required '([^']*)' selection appears")]
        public void ThenWarningAboutRequiredSelectionAppears(string p0)
        {
            Assert.IsTrue(_subscribePage.IsAlertDisplayed());
        }

        [Then(@"User redirects to enrollment registration page")]
        public void ThenUserRedirectsToEnrollmentRegistrationPage()
        {
            Assert.IsTrue(_registrationPage.Displayed());
        }

        [Then(@"([^']*) displayed as default country for 'Home address'")]
        public void ThenUnitedKingdomDisplayedAsDefaultCountryFor(string country)
        {
            Assert.IsTrue(_registrationPage.CountryIsDisplayed(country));
        }

        [Then(@"'Next' button is disabled to proceed")]
        public void ThenButtonIsDisabledToProceed()
        {
            Assert.IsFalse(_registrationPage.NextButton.Enabled);
        }

        [Then(@"'Next' button is enabled to proceed")]
        public void ThenButtonIsEnabledToProceed()
        {
            _registrationPage.NextButton.ScrollToElement();
            Assert.IsTrue(_registrationPage.NextButton.Enabled);
        }

        [Then(@"User redirects to home page")]
        public void ThenUserRedirectsToHomePage()
        {
            Assert.IsTrue(_homePage.Displayed());
        }

        [Then(@"Homepage content displayed")]
        public void ThenHomepageContentDisplayed()
        {
            Assert.IsTrue(_homePage.ContentDisplayed());
        }

        #endregion

    }
}