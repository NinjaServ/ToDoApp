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

        bool SaveToDoItem(ToDoItem toDoItem);
    }
}
