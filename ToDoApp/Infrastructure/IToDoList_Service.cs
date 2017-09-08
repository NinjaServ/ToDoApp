using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Infrastructure
{
    public interface IToDoList_Service
    {
        int GetItemCounterAndIncrement();

        ToDoListCollection GetToDoList();
        bool SyncToDoList();
        bool AddItem(ToDoItem addItem);
        bool ToggleItemCompleted(ToDoItem flagItem);
        bool ReplaceToDoItem(ToDoItem toDoItem, ToDoItem replacementItem); 
        bool DeleteToDoItem(ToDoItem toDoItem);
        bool SaveToDoItem(ToDoItem toDoItem);
    }
}
