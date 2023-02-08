Feature: Task 4

Background: 
	Given Swapfiets main page opened

Scenario Outline: 'Power 1' subscription in London
	When User performs search by <location> using search field
	Then <location> location is displayed on search result page
	And Search results displayed on screen
	And Price is displayed for all search result items
	And 'Subscribe' button is displayed for each search result item
	And 'More details' link is displayed for each search result item
	When User clicks on 'Subscribe' button for <bike> item
	Then <bike> details page is displayed
	And Membership options displayed
	And 'Bike usage' options displayed
	And 'Order bike' button is displayed
	When User clicks 'Order bike' button
	Then Warning about required 'Bike usage' selection appears
	When User selects 'Bike usage' option
	And User clicks 'Order bike' button to enroll
	Then User redirects to enrollment registration page
	And <country> displayed as default country for 'Home address'
	And 'Next' button is disabled to proceed
	When User fills details
	And Check 'Terms and Conditions' checkbox
	Then 'Next' button is enabled to proceed
	When User clicks 'Back' button
	Then User redirects to home page
	And Homepage content displayed

Examples: 
| location | bike    |    country     |
| London   | Power 1 | United Kingdom |