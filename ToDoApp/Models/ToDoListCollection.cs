using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ToDoApp.Models; 

namespace ToDoApp.Models
{
    /// <summary>
    /// Collection class for Employee entitites.
    /// </summary>
    public class ToDoListCollection : ObservableCollection<ToDoItem>
    {

        public ToDoListCollection()
        {
            
        }

        static public ToDoListCollection ListToCollection(IList<ToDoItem> itemList)
        {
            ToDoListCollection aCollection = new ToDoListCollection();
            aCollection.AddRange<ToDoItem>(itemList);

            return aCollection;
        }

        //Test method for itemList population - can migrate to Unit Test
        public bool CreateTestItems()
        {
            if (this != null)
            {
                this.Add(new ToDoItem("Item1", "These are Details 1", DateTime.Now, DateTime.Now.AddDays(1.0)));
                this.Add(new ToDoItem("Item2", "These are Details 2", DateTime.Now, DateTime.Now.AddDays(1.0)));
                this.Add(new ToDoItem("Item3", "These are Details 3", DateTime.Now, DateTime.Now.AddDays(1.0)));
                this.Add(new ToDoItem("aItem4", "Oh Details 4", DateTime.Now, DateTime.Now.AddDays(1.0)));
                this.Add(new ToDoItem("Item 5", "For 5 details are here", DateTime.Now, DateTime.Now.AddDays(1.0)));
                this.Add(new ToDoItem("zItem6", "Some 6 details are ny", DateTime.Now, DateTime.Now.AddDays(1.0)));

                int count = this.Count();
                if (count > 0)
                    return true; 
            }

            return false; 
        }

    }
}



//public void AddRange(IEnumerable<ToDoItem> items)
//{
//    foreach (var item in items)
//    {
//        this.Add(item);

//    }

//    //items.ToList().ForEach(this.Add);
//}

