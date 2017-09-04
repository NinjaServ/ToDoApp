using System;
using System.Threading.Tasks;
using Prism.Unity;
using Prism.Interactivity.InteractionRequest;

namespace ToDoApp.Models
{
    public class IMessage : Notification
    {
        public IMessage(string message, string title)
        {
            this.Content = message;
            this.Title = title;
        }
    }
}

