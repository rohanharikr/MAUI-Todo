using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace TODOApp.Models;

public record TODO (
    string Title,
    bool IsCompleted
)
{
    private RelayCommand deleteTodoCommand;
    public ICommand DeleteTodoCommand => deleteTodoCommand ??= new RelayCommand(DeleteTodo);

    private void DeleteTodo()
    {
    }
}