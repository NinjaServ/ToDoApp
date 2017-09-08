using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Common;
using ToDoApp.Models;

//Add reference to System.Web.Extensions 
using System.Web.Script.Serialization;
//using Newtonsoft.Json;
using System.IO;
using Prism.Mvvm;


namespace ToDoApp.Infrastructure
{
    public class ToDoList_Service : BindableBase, IToDoList_Service
    {
        private const string Directory_Path = @".\Data\";
        private const string File_Path_JSON = Directory_Path + @"ToDoListData.json";
        private const string File_Path_Settings = Directory_Path + @"SettingsData.json";

        struct keySet
        {
            public const string item_counter = "item_counter", text_file = "text_file",
                text_file_path = "text_file_path", settings_file_path = "settings_file_path";
        };


        private ToDoListCollection _toDoList;
        public ToDoListCollection ToDoList
        {
            get { return _toDoList; }
            set
            {
                if (_toDoList != value)
                {
                    _toDoList = value;
                    SetProperty(ref _toDoList, value);
                }
            }
        }

        private SettingsDictionary settings { get; set; }

        public ToDoList_Service()
        {
            DirectoryCheck();

            settings = new SettingsDictionary();
            if (SettingsFile_Check())
                SettingsFile_Load();
            else
            {
                Initialize_Settings();
                SettingsFile_Save();
            }

            _toDoList = new ToDoListCollection();
            if (DataFile_Check())
                DataFile_Load();
            else
            {
                //A test methond for Mock data
                //Initialize_ToDoList();
                DataFile_Save();
            }

        }

        ~ToDoList_Service()
        {
            if (settings != null)
                SettingsFile_Save();
            //if (ToDoList != null)
            //    DataFile_Save();

            settings = null;
            ToDoList = null;

        }


        public bool Initialize_Settings()
        {
            if (settings != null)
            {
                settings.Add(keySet.item_counter, "1");
                settings.Add(keySet.text_file, "1");
                settings.Add(keySet.text_file_path, File_Path_JSON);
                settings.Add(keySet.settings_file_path, File_Path_Settings);
            }
            else
            {
                return false;
            }
            return true;
        }

        bool Sync_Settings()
        {
            bool result;

            if (SettingsFile_Check())
            {
                SettingsFile_Save();
                result = true;
            }
            else
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// A Test methond to provide dummy data to the application
        /// </summary>
        /// <returns></returns>
        public bool Initialize_ToDoList()
        {
            if (ToDoList != null)
            {
                ToDoList.CreateTestItems();
                return true;
            }

            return false;
        }


        public ToDoListCollection GetToDoList()
        {
            return ToDoList;
        }

        public int GetItemCounter()
        {
            var value = settings[keySet.item_counter];
            int count = -1;
            bool result = int.TryParse(value, out count);
            int cnt = Convert.ToInt32(value); 
      
            return count;
        }

        public bool ItemCounterIncrement()
        {
            bool result = false;

            int count = GetItemCounter();
            if (count < int.MaxValue)
            {
                count++;
                result = true; 
            }

            var value = count.ToString();
            settings[keySet.item_counter] = value;
            Sync_Settings(); 

            return result;
        }

        public int GetItemCounterAndIncrement()
        {
            int count = GetItemCounter();
            bool result = ItemCounterIncrement();
           
            return count;
        }

        
        public bool AddItem(ToDoItem addItem)
        {
            bool result = false;
            if(addItem != null && addItem.Error == null)
            {
                int counter = GetItemCounterAndIncrement();
                addItem.id = counter;
                ToDoList.Add(addItem);
                SyncToDoList();
                result = true; 
            }
           
            return result ;
        }

        public bool ToggleItemCompleted(ToDoItem flagItem)
        {
            bool result = false;
            if (flagItem != null && flagItem.Error == null)
            {
                if (ToDoList.Contains(flagItem))
                {
                    flagItem.CompleteToggle();
                    SyncToDoList();
                    result = true;
                }
            }

            return result;
        }



        public bool ReplaceToDoItem(ToDoItem toDoItem, ToDoItem replacementItem)
        {
            bool result = false;

            if (ToDoList != null && toDoItem != null && replacementItem != null)
            {
                if (ToDoList.Contains(toDoItem))
                {
                    int index = ToDoList.IndexOf(toDoItem);
                    //ToDoList.Insert(index, replacementItem);
                    //ToDoList.Remove(toDoItem);
                    ToDoList[index] = replacementItem;
                    SyncToDoList();
                    result = true;
                }
            }
            else
            {
                return false;
            }

            return result;
        }

        public bool DeleteToDoItem(ToDoItem toDoItem)
        {
            bool result;

            if (ToDoList != null && toDoItem != null)
            {
                ToDoList.Remove(toDoItem);
                SyncToDoList();
                result = true;
            }
            else
            {
                return false;
            }

            return result;
        }



        public bool SaveToDoItem(ToDoItem toDoItem)
        {
            if (!_toDoList.Contains(toDoItem))
            {
                _toDoList.Add(toDoItem);
                DataFile_Save();
            }
            else
            {
                //Item already in ToDoList
                return false;
            }
            return true;
        }


        public bool SyncToDoList()
        {
            bool result;

            if (DataFile_Check())
            {
                DataFile_Save();
                result = true;
            }
            else
            {
                return false;
            }

            return result;
        }






        private bool DirectoryCheck()
        {
            // Specify the directory you want to manipulate.
            //string path = @".\Data\";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(Directory_Path))
                {
                    Console.WriteLine("That path exists already. {0}", Directory_Path);
                    return true;
                }

                // Try to create the directory.
                DirectoryInfo dir = Directory.CreateDirectory(Directory_Path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(Directory_Path));

                // Delete the directory.
                //dir.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }

            return false;
        }

        private bool DataFile_Check()
        {
            string filePath = File_Path_JSON;
            return (File.Exists(filePath));
        }

        private bool DataFile_Save()
        {
            bool result = false;

            try
            {
                ToDoListIO.Save(ToDoList, File_Path_JSON);
                result = true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("File Save Error, Exception: {0}", e.ToString());  //e.Message
            }

            return result;
        }

        private bool DataFile_Load()
        {
            bool result = false;

            try
            {
                var aList = ToDoListIO.Load(File_Path_JSON);
                ToDoListCollection listCollection = aList;
                if (listCollection != null)
                {
                    ToDoList = listCollection;
                    result = true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception: {0} \nFile Load Error", e.Message);
            }

            return result;
        }

        private bool SettingsFile_Check()
        {
            string filePath = File_Path_Settings;
            return (File.Exists(filePath));
        }

        private bool SettingsFile_Save()
        {
            bool result = false;

            try
            {
                SettingsIO.Save(settings, File_Path_Settings);
                result = true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception: {0} \nFile Save Error", e.Message);
            }

            return result;
        }

        private bool SettingsFile_Load()
        {
            bool result = false;

            try
            {
                SettingsDictionary settingsDict = SettingsIO.Load(File_Path_Settings);
                if (settingsDict != null)
                {
                    settings = settingsDict;
                    result = true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception: {0} \nFile Load Error", e.Message);
            }

            return result;
        }



        /*
         * - Check if file exists, if not, create file 
         * - If file exists, save to file
         * -- Pass filename and list to save in file format, call Write to file format
         * - If file exist, load from file
         * -- Pass filename and return list to load from file format, call Read from file format
         * 
         * 
         */

        class SettingsIO : FileIO<SettingsDictionary>
        {
            public SettingsDictionary setDictionary { get; set; }

            public void Save()
            {
                this.Save(File_Path_Settings);
            }
        }

        class ToDoListIO : FileIO<ToDoListCollection>
        {
            public ToDoListCollection listCollection { get; set; }

            public void Save()
            {
                this.Save(File_Path_JSON);
            }

            public new static ToDoListCollection Load(string fileName)
            {
                ToDoListCollection t = null;

                if (File.Exists(fileName))
                {
                    //var serializer = new JavaScriptSerializer();
                    //var kvpData = serializer.DeserializeObject(File.ReadAllText(fileName));

                    IEnumerable<ToDoItem> result = (new JavaScriptSerializer()).Deserialize<List<ToDoItem>>(File.ReadAllText(fileName));
                    t = new ToDoListCollection(result);
                }

                return t;
            }

        }

    }



    class SettingsDictionary : Dictionary<string, string>
    {
        public SettingsDictionary()
        {

        }
    }

    public class FileIO<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "data.json";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }
    }
}




// -- For Project in Solution Explorer, choose Properties. Select the Settings tab, click on the hyperlink if settings doesn't exist. 
// -- Use the Settings tab to create application settings. Visual Studio creates the file Settings.settings 
// Properties.Settings.Default["SomeProperty"] = "Some Value";
// Properties.Settings.Default.Save(); // Saves settings in application configuration file
