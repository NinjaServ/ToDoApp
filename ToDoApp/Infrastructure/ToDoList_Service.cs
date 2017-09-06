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
        private const string File_Path_JSON = @"./Data/ToDoListData.json";
        private const string File_Path_Settings = @"./Data/SettingsData.json";

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
        //public Dictionary<string, string> settings  { get; set; }

        public ToDoList_Service()
        {
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
                Initialize_Settings();
                DataFile_Save();
            }

        }

        public bool Initialize_Settings()
        {
            if (settings != null)
            {
                settings.Add(keySet.item_counter, "1");
                settings.Add(keySet.text_file, "1");
                settings.Add(keySet.text_file, File_Path_JSON);
                settings.Add(keySet.settings_file_path, File_Path_Settings);
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool Initialize_ToDoList()
        {
            if (ToDoList != null)
            {
                ToDoList.CreateTestItems();
                return true;
            }

            return false;
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
                ToDoListIO.Save(ToDoList);
                result = true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("File Save Error, Exception: {0}", e.Message);
            }

            return result;
        }

        private bool DataFile_Load()
        {
            bool result = false;

            try
            {
                ToDoListCollection listCollection = ToDoListIO.Load();
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
                SettingsIO.Save(settings);
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
                SettingsDictionary settingsDict = SettingsIO.Load();
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

        public void WriteFile_JSON(string filePath)
        {
            var itemList = _toDoList.ToList<ToDoItem>();

            if (itemList.Count != 0)
            {
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(itemList);
                //    write string to file
                System.IO.File.WriteAllText(filePath, serializedResult);
            }
        }

        public ToDoListCollection ReadFile_JSON(string filePath)
        {
            var itemList = _toDoList.ToList<ToDoItem>();

            if (File.Exists(filePath))
            {
                string data = System.IO.File.ReadAllText(filePath);

                var serializer = new JavaScriptSerializer();
                var deserializedResult = serializer.Deserialize<List<ToDoItem>>(data);

                ToDoListCollection aCollection = ToDoListCollection.ListToCollection(deserializedResult);

                return aCollection;
            }

            return null; 
        }

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
        }

    }

    class SettingsDictionary: Dictionary<string,string>
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
