using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace UITests;

public abstract class BaseTest
{
    protected AppiumDriver App => AppiumSetup.App;

    protected AppiumElement FindUIElement(string id)
    {
        if (App is WindowsDriver)
        {
            return App.FindElement(MobileBy.AccessibilityId(id));
        }

        return App.FindElement(MobileBy.Id(id));
    }

    protected List<AppiumElement> FindUIElements(string id)
    {
        if (App is WindowsDriver)
        {
            return App.FindElements(MobileBy.AccessibilityId(id)).ToList();
        }

        return App.FindElements(MobileBy.Id(id)).ToList();
    }

    // Looks like ActivateApp method has not yet been implemented
    //[SetUp]
    //public void StartApp()
    //{
    //    App.ActivateApp(AppiumSetup.AppId);
    //}
    //[TearDown]
    //public void TerminateApp()
    //{
    //    App.TerminateApp(AppiumSetup.AppId);
    //}

    [TearDown]
    public void TerminateApp()
    {
        App.ResetApp();
    }
}