using NUnit.Framework;

namespace UITests;

public class MainPageTests : BaseTest
{
	[Test]
	public void AddTodo()
	{
		var todoEntry = FindUIElement("TodoEntry");
		var addTodoButton = FindUIElement("AddTodoButton");

		todoEntry.SendKeys("Hello World!");
		addTodoButton.Click();


    }
}