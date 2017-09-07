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
        ToDoListCollection GetToDoList();
        bool SyncToDoList();
        bool ReplaceToDoItem(ToDoItem toDoItem, ToDoItem replacementItem); 
        bool DeleteToDoItem(ToDoItem toDoItem);
        bool SaveToDoItem(ToDoItem toDoItem);
    }
}
