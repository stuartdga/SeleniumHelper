SeleniumHelper for C#

This project is a simple framework for 
creating Selenium tests using the Selenium
WebDriver and the Selenium Grid.  This version is using version 3
of the Selenium WebDriver.

The project has two main classes:  Connector and Utility.

Connector Class:
Use this class to initialize an instance of the WebDriver, 
either local or remote.  Current browsers supported are:

Local:  Firefox, Chrome or PhantomJS<br />
Remote:  Firefox or Chrome

Utility Class:
This class contains useful methods to make tests easier
to create.

GoTo: navigates to the desired URL<br />
InputValue: sets the value of an Input textbox<br />
SetDropDownItem: sets an option list to a specified option<br />
WaitForElementReady: returns an iWebElement once it is available
on the web page<br />
WaitForElementByIdReady: returns an iWebElement based on the document Id once it is available
on the web page <br />
WaitForElementXpathIdReady: returns an iWebElement based on an Xpath selector once it is available
on the web page <br />
GetDriver: returns an instance of the driver that is associated 
with an iWebElement<br />
GetID: returns the document Id of an iWebElement if it exists<br />
GetClasses: returns a list of CSS classes for an iWebElement<br />
HasClass:  returns a bool if a class is present on an iWebElement<br />
SetAttribue: sets the value of an HTML element's attribute<br />
ExtractManifestResourceToDisk: supports extracting embedded resources allowing them
to be utilized for tests<br />
ResetDriver: quit the driver and release any resources

The project includes unit tests for the classes listed above as well
as sample tests using the framework.