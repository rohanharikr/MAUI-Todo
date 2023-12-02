using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace UITests;

public class TodoTest : BaseTest
{
    private AppiumElement todoEntry => FindUIElement("TodoEntry");
    private AppiumElement addTodoButton => FindUIElement("AddTodoButton");
    private AppiumElement homePageTab => FindUIElement("HomePageTab");
    private AppiumElement completedPageTab => FindUIElement("CompletedPageTab");
    private List<AppiumElement> todos => FindUIElements("TodoItem");
    private List<AppiumElement> completedTodos => FindUIElements("TodoCompleteItem");

    private void AddTodo()
    {
        todoEntry.SendKeys("Hello World");
        addTodoButton.Click();
    }

    [Test]
    public void Add_Todo()
    {
        AddTodo();

        var todo = todos.First().FindElement(MobileBy.Id("TodoItemEntry")).Text;

        Assert.That(todo, Is.EqualTo("Hello World"));
    }

    [Test]
    public void Should_Not_Add_Empty_Todo()
    {
        Assert.IsFalse(addTodoButton.Enabled); //disabled on app load

        todoEntry.SendKeys("Hello World");
        todoEntry.Clear();

        Assert.IsFalse(addTodoButton.Enabled); //disabled on clear input
    }

    [Test]
    public void Should_Not_Add_Duplicate_Todo()
    {
        AddTodo();

        todoEntry.SendKeys("Hello World");

        Assert.IsFalse(addTodoButton.Enabled);
    }

    [Test]
    public void Delete_Incomplete_Todo()
    {
        AddTodo();

        var todoItemDeleteButton = todos.First().FindElement(MobileBy.Id("TodoItemDeleteButton"));

        todoItemDeleteButton.Click();

        Assert.IsEmpty(todos);
    }

    [Test]
    public void Mark_Todo_As_Complete()
    {
        AddTodo();

        var todoItemCompleteButton = todos.First().FindElement(MobileBy.Id("TodoItemCompleteButton"));

        todoItemCompleteButton.Click();

        Assert.IsEmpty(todos);

        completedPageTab.Click();
        var todoComplete = completedTodos.First().FindElement(MobileBy.Id("TodoCompleteItemEntry")).Text;

        Assert.That(todoComplete, Is.EqualTo("Hello World"));
    }

    [Test]
    public void Delete_Complete_Todo()
    {
        AddTodo();
        var todoItemCompleteButton = todos.First().FindElement(MobileBy.Id("TodoItemCompleteButton"));

        todoItemCompleteButton.Click();
        completedPageTab.Click();

        var todoItemDeleteButton = completedTodos.First().FindElement(MobileBy.Id("TodoCompleteItemDeleteButton"));

        todoItemDeleteButton.Click();

        Assert.IsEmpty(completedTodos);
    }

    [Test]
    public void Mark_Todo_As_Incomplete()
    {
        AddTodo();
        var todoItemCompleteButton = todos.First().FindElement(MobileBy.Id("TodoItemCompleteButton"));

        todoItemCompleteButton.Click();

        Assert.IsEmpty(todos);

        completedPageTab.Click();
        var todoCompleteItemDeleteButton = completedTodos.First().FindElement(MobileBy.Id("TodoCompleteItemDeleteButton"));

        todoCompleteItemDeleteButton.Click();

        Assert.IsEmpty(completedTodos);

        homePageTab.Click();

        var todoComplete = completedTodos.First().FindElement(MobileBy.Id("TodoCompleteItemEntry")).Text;

        Assert.That(todoComplete, Is.EqualTo("Hello World"));
    }
}