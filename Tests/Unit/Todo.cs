using Xunit;
using TODOApp.Models;
using TODOApp.ViewModels;

public class TaskViewModelTests
{
    [Fact]
    public void Add_Todo()
    {
        var viewModel = new TaskViewModel();
        viewModel.Todo = "Hello World";

        viewModel.AddTodoCommand.Execute(null);

        Assert.Single(viewModel.Todos);
        Assert.Equal("Hello World", viewModel.Todos.First().Title);
    }

    [Fact]
    public void Should_Not_Add_Duplicate_Todo()
    {
        var viewModel = new TaskViewModel();
        viewModel.Todo = "Hello World";

        viewModel.AddTodoCommand.Execute(null);
        viewModel.AddTodoCommand.Execute(null);

        Assert.Single(viewModel.Todos);
    }

    [Fact]
    public void Should_Not_Add_Empty_Todo()
    {
        var viewModel = new TaskViewModel();
        viewModel.Todo = string.Empty;

        viewModel.AddTodoCommand.Execute(null);

        Assert.Empty(viewModel.Todos);
    }

    [Fact]
    public void Delete_Todo()
    {
        var viewModel = new TaskViewModel();
        var todo = new TODO { Id = Guid.NewGuid(), Title = "Hello World", IsCompleted = false };
        viewModel.Todos.Add(todo);

        viewModel.DeleteTodoCommand.Execute(todo);

        Assert.Empty(viewModel.Todos);
    }

    [Fact]
    public void Mark_Todo_As_Complete()
    {
        var viewModel = new TaskViewModel();
        var todo = new TODO { Id = Guid.NewGuid(), Title = "Hello World", IsCompleted = false };
        viewModel.Todos.Add(todo);

        viewModel.CompleteTodoCommand.Execute(todo);

        Assert.True(todo.IsCompleted);
        Assert.DoesNotContain(todo, viewModel.IncompletedTodos);
        Assert.Contains(todo, viewModel.CompletedTodos);
    }

    [Fact]
    public void Mark_Todo_As_Incomplete()
    {
        var viewModel = new TaskViewModel();
        var todo = new TODO { Id = Guid.NewGuid(), Title = "Hello World", IsCompleted = false };
        viewModel.Todos.Add(todo);

        viewModel.CompleteTodoCommand.Execute(todo);
        viewModel.IncompleteTodoCommand.Execute(todo);

        Assert.False(todo.IsCompleted);
        Assert.DoesNotContain(todo, viewModel.IncompletedTodos);
        Assert.Contains(todo, viewModel.CompletedTodos);
    }
}
