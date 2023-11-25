using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODOApp.Models;

namespace TODOApp.ViewModels;

public partial class TaskViewModel : ObservableObject
{
    [ObservableProperty]
	string todo;

    [ObservableProperty]
    bool addEnabled = false;

    [ObservableProperty]
    public ObservableCollection<TODO> todos = new();

    [ObservableProperty]
    public ObservableCollection<TODO> incompletedTodos = new();

    [ObservableProperty]
    public ObservableCollection<TODO> completedTodos = new();

    partial void OnTodoChanged(string value)
    {
        AddEnabled = true;
        if (Todos.Any(todo => todo.Title == Todo)) //is duplicate
            AddEnabled = false;
        if (string.IsNullOrWhiteSpace(Todo)) //is empty
            AddEnabled = false;
    }

    [RelayCommand]
	void AddTodo()
	{
        //Console.WriteLine(IncompletedTodos.Count);
        TODO todoItem = new()
        {
            Id = Guid.NewGuid(),
            Title = Todo,
            IsCompleted = false
        };
        Todos.Add(todoItem);
        Todo = "";

        IncompletedTodos.Clear();
        foreach (var todo in Todos.Where(todo => !todo.IsCompleted))
            IncompletedTodos.Add(todo);

        CompletedTodos.Clear();
        foreach (var todo in Todos.Where(todo => todo.IsCompleted))
            CompletedTodos.Add(todo);
    }

    [RelayCommand]
    void DeleteTodo(Guid id)
    {
        var updatedTodos = Todos.Where(todo => todo.Id != id).ToList();
        Todos = new ObservableCollection<TODO>(updatedTodos);

        IncompletedTodos.Clear();
        foreach (var todo in Todos.Where(todo => !todo.IsCompleted))
            IncompletedTodos.Add(todo);

        CompletedTodos.Clear();
        foreach (var todo in Todos.Where(todo => todo.IsCompleted))
            CompletedTodos.Add(todo);
    }

    [RelayCommand]
    void CompleteTodo(Guid id)
    {
        var updatedTodos = Todos.ToList();
        var todoToUpdate = updatedTodos.FirstOrDefault(todo => todo.Id == id);
        todoToUpdate.IsCompleted = true;
        Todos = new ObservableCollection<TODO>(updatedTodos);

        IncompletedTodos.Clear();
        foreach (var todo in Todos.Where(todo => !todo.IsCompleted))
            IncompletedTodos.Add(todo);

        CompletedTodos.Clear();
        foreach (var todo in Todos.Where(todo => todo.IsCompleted))
            CompletedTodos.Add(todo);
    }
}

