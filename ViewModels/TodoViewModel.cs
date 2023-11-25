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
    [NotifyPropertyChangedFor(nameof(IncompletedTodos)), NotifyPropertyChangedFor(nameof(CompletedTodos))]
    public ObservableCollection<TODO> todos = new();

    public ObservableCollection<TODO> IncompletedTodos => new ObservableCollection<TODO>(Todos.Where(todo => !todo.IsCompleted));
    public ObservableCollection<TODO> CompletedTodos => new ObservableCollection<TODO>(Todos.Where(todo => todo.IsCompleted));

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
    }

    [RelayCommand]
    void DeleteTodo(Guid id)
    {
        var updatedTodos = Todos.Where(todo => todo.Id != id).ToList();
        Todos = new ObservableCollection<TODO>(updatedTodos);
    }

    [RelayCommand]
    void CompleteTodo(Guid id)
    {
        var updatedTodos = Todos.ToList();
        var todoToUpdate = updatedTodos.FirstOrDefault(todo => todo.Id == id);
        todoToUpdate.IsCompleted = true;
        Todos = new ObservableCollection<TODO>(updatedTodos);
    }
}

