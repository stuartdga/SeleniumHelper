# SeleniumHelper for C# #

This project is a simple framework for 
creating Selenium tests using the Selenium
WebDriver and the Selenium Grid.  This version is using version 3
of the Selenium WebDriver.

### The project has two main classes:  Connector and Utility.

###### Connector Class:
Use this class to initialize an instance of the WebDriver, 
either local or remote.  Current browsers supported are:

There are several overloadeds for the __Initialize__ method:<br />
###### Local execution:  Firefox, Chrome or PhantomJS<br />
1. string browser <br />
2. string browser, string driverPath <br />
3. string browser, bool remote, string driverPath, Uri seleniumHubURL, string operatingSystem, , bool maximize

###### Remote execution:  Firefox or Chrome
1. string browser, Uri seleniumHubURL <br />
2. string browser, Uri seleniumHubURL, string operatingSystem, bool maximize <br />

_operatingSystem_ defaults to Any<br />
_maximize_ (the browser window) defaults to true for local execution and false for remote execution

__InitializeRemote:__ Browser browser, Uri seleniumHubURL, string operatingSystem<br />
You can use this method to initialize the remote webdriver directly.

You can set the remote webdriver URL in the app.config:

`
  <appSettings>
    <add key="SeleniumHubURL" value="http://127.0.0.1:4444/wd/hub" />
  </appSettings>
`
###### Utility Class:
This class contains useful methods to make tests easier
to create.

__GoTo:__ navigates to the desired URL<br />
__InputValue:__ sets the value of an Input textbox<br />
__SetDropDownItem:__ sets an option list to a specified option<br />
__WaitForElementReady:__ returns an iWebElement once it is available
on the web page<br />
__WaitForElementByIdReady:__ returns an iWebElement based on the document Id once it is available
on the web page <br />
__WaitForElementXpathIdReady:__ returns an iWebElement based on an Xpath selector once it is available
on the web page <br />
__GetDriver:__ returns an instance of the driver that is associated 
with an iWebElement<br />
__GetID:__ returns the document Id of an iWebElement if it exists<br />
__GetClasses:__ returns a list of CSS classes for an iWebElement<br />
__HasClass:__  returns a bool if a class is present on an iWebElement<br />
__SetAttribue:__ sets the value of an HTML element's attribute<br />
__ExtractManifestResourceToDisk:__ supports extracting embedded resources allowing them
to be utilized for tests<br />
__ResetDriver:__ quit the driver and release any resources<br />
__CaptureScreenshot:__ captures the screen of a browser running on a remote node and saves it 
to a text file in Base64 format<br />

The project includes unit tests for the classes listed above as well
as sample tests using the framework.