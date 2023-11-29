using NUnit.Framework;

namespace UITests;

public class MainPageTest : BaseTest
{
    [Test]
    public void Add_Todo()
    {
        var todoEntry = FindUIElement("TodoEntry");
        var addTodoButton = FindUIElement("AddTodoButton");

        todoEntry.SendKeys("Hello World");
        addTodoButton.Click();

        var testing = FindUIElements("TodoItem");
        Assert.That(testing[0].Text, Is.EqualTo("Hello World"));
    }
}