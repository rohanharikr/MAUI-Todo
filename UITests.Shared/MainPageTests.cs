using NUnit.Framework;

namespace UITests;

public class MainPageTests : BaseTest
{
	[Test]
	public void AddTodo()
	{
		var todoEntry = FindUIElement("TodoEntry");
		var addTodoButton = FindUIElement("AddTodoButton");
		var todoList = FindUIElement("TodoList");
		var todos = FindUIElements("Todo").ToList();

        todoEntry.SendKeys("Hello World!");
		addTodoButton.Click();

		Assert.That(todos[0].Text, Is.EqualTo("Hello World!"));
    }
}