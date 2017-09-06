using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

//Add reference to System.Web.Extensions 
using System.Web.Script.Serialization;
//using Newtonsoft.Json;
using System.IO;


namespace ToDoApp.Models
{
    public class ToDoItem : ICloneable, IDataErrorInfo
    {
        public static int counter = 1;

        public int id { get; set; }
        public string task { get; set; }
        public string detail { get; set; }
        public DateTime createDate { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime completeDate { get; set; }

        private float _log;
        public float log
        {
            get { return this._log; }
            set { this._log = value; }
        }


        /// <summary>
        /// Default constructor for ToDoItem. Creates empty ToDoItem.
        /// </summary>
        public ToDoItem()
        {
            this.setDefaults();
            //_task = @"";
            //_detail = @"";
            //_dueDate = this._createDate.AddDays(1.0);

        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="aTask">Value for the Task string</param>
        /// <param name="aDetail">Value for the Detail string</param>
        public ToDoItem(string aTask, string aDetail)
        {
            this.setDefaults();
            this.task = aTask;
            this.detail = aDetail;
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="aTask">Value for the Task string</param>
        /// <param name="aDetail">Value for the Detail string</param>
        public ToDoItem(string aTask, string aDetail, DateTime aCreateDate, DateTime aDueDate)
        {
            this.setDefaults();
            this.task = aTask;
            this.detail = aDetail;
            this.createDate = aCreateDate;
            this.dueDate = aDueDate;

        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="aTask">Value for the Task string</param>
        /// <param name="aDetail">Value for the Detail string</param>
        public ToDoItem(int aID, string aTask, string aDetail, DateTime aCreateDate, DateTime aDueDate, DateTime aCompleteDate)
        {
            this.id = aID;
            this.task = aTask;
            this.detail = aDetail;
            this.createDate = aCreateDate;
            this.dueDate = aDueDate;
            this.completeDate = aCompleteDate;
        }

        /// <summary>
        /// Set intial values for class members
        /// </summary>
        private void setDefaults()
        {
            id = counter++;
            task = @"";
            detail = @"";
            createDate = DateTime.Now;
            dueDate = this.createDate.AddDays(1.0);
            completeDate = new DateTime();
            completeDate = new DateTime(0);
            completeDate = DateTime.MinValue;
        }


        public bool IsComplete()
        {
            DateTime initDate = new DateTime();
            return (this.completeDate > initDate ? true : false);
        }

        public bool IsDue()
        {
            return (this.dueDate.Date == DateTime.Now.Date && (!IsComplete()) ? true : false);
        }

        public bool IsOverdue()
        {
            return (this.dueDate.Date > DateTime.Now.Date && (!IsComplete()) ? true : false);
        }



        /// <summary>
        /// Convert values inside ToDoItem to a Dictionary object
        /// </summary>
        /// <returns>Dictionary<string, string> of keys, (names of each data member) and string values for the ToDoItem</returns>
        public Dictionary<string, string> toDictionary()
        {
            Dictionary<string, string> todoDictionary = new Dictionary<string, string>
            {
                {"ID", id.ToString() },
                {"Task", task },
                {"Details", detail },
                {"Creation Date", createDate.ToString() },
                { "Due Date", dueDate.ToString() },
                { "Completion Date", completeDate.ToString() }
            };

            return todoDictionary;
        }

        ~ToDoItem()
        {
            id = 0;
            task = null;
            detail = null;

        }


        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public ToDoItem DeepCopy()
        {
            ToDoItem other = (ToDoItem)this.MemberwiseClone();
            other.task = String.Copy(task);
            other.detail = String.Copy(detail);

            return other;
        }


        #region IDataErrorInfo Interface
        
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case "id":
                        if (id == 0)
                            error = "Unique id required";
                        break;
                    case "task":
                        if (!DataValidator.StringIsText(task) && !DataValidator.TextIsSentences(task))
                        {
                            error = "Task text invalid";
                        }
                        break;
                    case "detail":
                        if (!DataValidator.StringIsText(detail) && !DataValidator.TextIsParagraphs(detail))
                        {
                            error = "Task text invalid";
                        }
                        break;
                }
                return error; 
            }
        }

        #endregion
    }  
}


//The project goals are simple: We would like you to create an application that provides the ubiquitous
//"TODO list" example application that is all the rage right now.The basic functionality is given below: 
//	As a user, I can add a TODO to the list.
//    As a user, I can see all the TODOs on the list in an overview. 

//    As a user, I can drill into a TODO to see more information about the TODO.
//    As a user, I can delete a TODO.
//    As a user, I can mark a TODO as completed.
//    As a user, when I see all the TODOs in the overview, if today's date is past the TODO's deadline, highlight it. 

//A TODO consists of

//    a task (just a simple sentence or two is fine), 
//	a deadline date, 
//	a completed flag, and
//    an optional "more details" field that allows for more details to be given(again, a single large text area is fine for this). 


