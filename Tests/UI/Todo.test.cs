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

        var todos = FindUIElements("TodoItem");
        var todoItem = todos.First();
        var todo = todoItem.FindElement(MobileBy.Id("TodoItemEntry")).Text;

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

        var todos = FindUIElements("TodoItem");
        var todoItem = todos.First();
        var todoItemDeleteButton = todoItem.FindElement(MobileBy.Id("TodoItemDeleteButton"));

        todoItemDeleteButton.Click();

        todos = FindUIElements("TodoItem");

        Assert.IsEmpty(todos);
    }

    [Test]
    public void Mark_Todo_As_Complete()
    {
        AddTodo();

        var todos = FindUIElements("TodoItem");
        var todoItem = todos.First();
        var todoItemCompleteButton = todoItem.FindElement(MobileBy.Id("TodoItemCompleteButton"));

        todoItemCompleteButton.Click();

        todos = FindUIElements("TodoItem");

        Assert.IsEmpty(todos);

        completedPageTab.Click();
        var completedTodos = FindUIElements("TodoCompleteItem");
        var todoCompleteItem = completedTodos.First();
        var todoComplete = todoCompleteItem.FindElement(MobileBy.Id("TodoCompleteItemEntry")).Text;

        Assert.That(todoComplete, Is.EqualTo("Hello World"));
    }

    [Test]
    public void Delete_Complete_Todo()
    {
        AddTodo();
        var todos = FindUIElements("TodoItem");
        var todoItem = todos.First();
        var todoItemCompleteButton = todoItem.FindElement(MobileBy.Id("TodoItemCompleteButton"));

        todoItemCompleteButton.Click();
        completedPageTab.Click();

        var completedTodos = FindUIElements("TodoCompleteItem");
        var todoCompleteItem = completedTodos.First();
        var todoItemDeleteButton = todoCompleteItem.FindElement(MobileBy.Id("TodoCompleteItemDeleteButton"));

        todoItemDeleteButton.Click();

        completedTodos = FindUIElements("TodoCompleteItem");

        Assert.IsEmpty(completedTodos);
    }

    [Test]
    public void Mark_Todo_As_Incomplete()
    {
        AddTodo();
        var todos = FindUIElements("TodoItem");
        var todoItem = todos.First();
        var todoItemCompleteButton = todoItem.FindElement(MobileBy.Id("TodoItemCompleteButton"));

        todoItemCompleteButton.Click();

        todos = FindUIElements("TodoItem");

        Assert.IsEmpty(todos);

        completedPageTab.Click();
        var completedTodos = FindUIElements("TodoCompleteItem");
        var todoCompleteItem = completedTodos.First();
        var todoCompleteItemDeleteButton = todoCompleteItem.FindElement(MobileBy.Id("TodoCompleteItemDeleteButton"));

        todoCompleteItemDeleteButton.Click();

        completedTodos = FindUIElements("TodoCompleteItem");

        Assert.IsEmpty(completedTodos);

        homePageTab.Click();

        todos = FindUIElements("TodoItem");
        todoItem = todos.First();
        var todoComplete = todoItem.FindElement(MobileBy.Id("TodoCompleteItemEntry")).Text;

        Assert.That(todoComplete, Is.EqualTo("Hello World"));
    }
}