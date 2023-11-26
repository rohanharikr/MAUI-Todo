using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODOApp.Models;
using System.Linq;

namespace TODOApp.ViewModels;

public partial class TaskViewModel : ObservableObject
{
    [ObservableProperty]
	string todo;

    [ObservableProperty]
    bool addEnabled = false;

    [ObservableProperty]
    public ObservableCollection<TODO> todos = new();

    public IEnumerable<TODO> IncompletedTodos => Todos.Where(todo => !todo.IsCompleted);
    public IEnumerable<TODO> CompletedTodos => Todos.Where(todo => todo.IsCompleted);

    public TaskViewModel()
    {
        Todos.CollectionChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(IncompletedTodos));
            OnPropertyChanged(nameof(CompletedTodos));
        };
    }

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
        if (!AddEnabled)
            return;

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
    void DeleteTodo(TODO todo) => Todos.Remove(todo);

    [RelayCommand]
    void CompleteTodo(TODO todo)
    {
        todo.IsCompleted = true;

        //TBD 
        OnPropertyChanged(nameof(IncompletedTodos));
        OnPropertyChanged(nameof(CompletedTodos));
    }
}

