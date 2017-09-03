using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Add reference to System.Web.Extensions 
using System.Web.Script.Serialization;
//using Newtonsoft.Json;
using System.IO;

namespace ToDoApp.Models
{
    public class ToDoItem
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


        //Json.Net, see example below:
        //public void Write_JSON_NET()
        //{
        //    List<Person> _data = new List<Person>();
        //    _data.Add(new Person()
        //    {
        //        PersonID = 1,
        //        Name = "A Message",
        //        Registered = false
        //    });

        //    string json = JsonConvert.SerializeObject(_data.ToArray());

        //    //write string to file
        //    System.IO.File.WriteAllText(@"C:\Atest\path.JSON", json);

        //    //open file stream
        //    using (StreamWriter file = File.CreateText(@"C:\Atest\path.txt"))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        //serialize object directly into file stream
        //        serializer.Serialize(file, _data);
        //    }

        //    string json_fmt = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

        //    Person person = new Person()
        //    {
        //        PersonID = 1,
        //        Name = "A Message",
        //        Registered = false
        //    };

        //    string json_fmt2 = JsonConvert.SerializeObject(person, Formatting.Indented);
        //    File.WriteAllText(@"c:\Atest\person.json", json_fmt2);


        //    using (FileStream fs = File.Open(@"c:\Atest\person2.json", FileMode.CreateNew))
        //    using (StreamWriter sw = new StreamWriter(fs))
        //    using (JsonWriter jw = new JsonTextWriter(sw))
        //    {
        //        jw.Formatting = Formatting.Indented;

        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(jw, person);

        //        string json_ser = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);


        //    }
        //}

        public void Write_JSS()
        {

            var RegisteredUsers = new List<Person>();
            RegisteredUsers.Add(new Person() { PersonID = 1, Name = "Bryon Hetrick", Registered = true });
            RegisteredUsers.Add(new Person() { PersonID = 2, Name = "Nicole Wilcox", Registered = true });
            RegisteredUsers.Add(new Person() { PersonID = 3, Name = "Adrian Martinson", Registered = false });
            RegisteredUsers.Add(new Person() { PersonID = 4, Name = "Nora Osborn", Registered = false });

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(RegisteredUsers);
            // Produces string value of:
            // [
            //     {"PersonID":1,"Name":"Bryon Hetrick","Registered":true},
            //     {"PersonID":2,"Name":"Nicole Wilcox","Registered":true},
            //     {"PersonID":3,"Name":"Adrian Martinson","Registered":false},
            //     {"PersonID":4,"Name":"Nora Osborn","Registered":false}
            // ]

            var deserializedResult = serializer.Deserialize<List<Person>>(serializedResult);

            // Produces List with 4 Person objects

        }

    }


    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public bool Registered { get; set; }
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








