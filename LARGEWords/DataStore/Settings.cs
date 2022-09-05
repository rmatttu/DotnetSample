using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LARGEWords.DataStore
{

    public class Settings
    {
        public static void Save()
        {
            object json = JsonConvert.SerializeObject(Data, Formatting.Indented);
            StreamWriter writer = new StreamWriter(SaveFilePath);
            writer.Write(json);
            writer.Close();
        }

        public static void RestoreForm(Form f)
        {
            SettingJson.FormState state = GetFormState(f);
            f.Bounds = new Rectangle(state.X,state.Y,state.Width,state.Height);
            f.WindowState = (FormWindowState)Enum.ToObject(typeof(FormWindowState), state.FormWindowState);

        }

        public static void SaveForm(Form f)
        {
            SettingJson.FormState state = GetFormState(f);
            state.X = f.Location.X;
            state.Y = f.Location.Y;
            state.Width = f.Width;
            state.Height = f.Height;
            state.FormWindowState = (int)f.WindowState;

        }

        private static SettingJson.FormState GetFormState(Form f)
        {
            foreach (var i in Data.FormStates)
            {
                if (i.Name == "Main")
                    return i;
            }
            throw new Exception("Nothing Form Data: " + f.ToString());
        }

        private readonly static string SaveFilePath = GenerateSaveFilePath();
        private static SettingJson.RootObject data;
        private Settings()
        {
        }

        private static string GenerateSaveFilePath()
        {
            FilePathAnalyzer a = new FilePathAnalyzer(Assembly.GetExecutingAssembly().Location);
            return a.ParentFolderFullPath + "\\" + Application.ProductName + ".json";
        }

        public static SettingJson.RootObject Data
        {
            get
            {
                if (data == null)
                {
                    if (File.Exists(SaveFilePath))
                    {
                        data = Load();
                    }
                    else
                    {
                        // set default
                        data = SettingJson.GetDefaultValues();
                    }
                }
                return data;
            }
            set { data = value; }
        }

        private static SettingJson.RootObject Load()
        {
            StreamReader reader = new StreamReader(SaveFilePath);
            string json = reader.ReadToEnd();
            reader.Close();
            return JsonConvert.DeserializeObject<SettingJson.RootObject>(json);
        }

    }
}
