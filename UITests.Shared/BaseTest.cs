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

    protected ICollection<AppiumElement> FindUIElements(string id)
    {
        if (App is WindowsDriver)
        {
            return App.FindElements(MobileBy.AccessibilityId(id));
        }

        return App.FindElements(MobileBy.Id(id));
    }
}