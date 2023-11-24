using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODOApp.Models;

namespace TODOApp.ViewModels;

internal partial class TaskViewModel : ObservableObject
{
    [ObservableProperty]
	string todo;

    [ObservableProperty]
    bool addEnabled = false;

    [ObservableProperty]
    ObservableCollection<TODO> todos = new();

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
        Todos.Add(new TODO(
            Title: Todo,
            IsCompleted: false
        ));
        Todo = "";
    }

    [RelayCommand]
    void DeleteTodo(TODO todo)
    {
        var updatedTodos = Todos.Where(_todo => _todo.Title != todo.Title).ToList();
        Todos = new ObservableCollection<TODO>(updatedTodos);
    }
}

