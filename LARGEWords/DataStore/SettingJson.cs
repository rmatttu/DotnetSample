using System.Collections.Generic;

namespace LARGEWords.DataStore
{
    public class SettingJson
    {
        public static RootObject GetDefaultValues()
        {
            RootObject root = new RootObject();
            root.FormStates = new List<FormState>();
            root.FormStates.Add(new FormState()
            {
                Name = "Main",
                X = 100,
                Y = 50,
                Width = 800,
                Height = 400,
            });
            root.KeyMaps = new List<KeyMap>();
            root.KeyMaps.Add(new KeyMap()
            {
                Key = 27,
                Control = false,
                Shift = false,
                Alt = false,
                function = 0,
            });
            root.KeyMaps.Add(new KeyMap()
            {
                Key = 123,
                Control = false,
                Shift = false,
                Alt = false,
                function = 1,
            });
            root.KeyMaps.Add(new KeyMap()
            {
                Key = 81,
                Control = true,
                Shift = false,
                Alt = false,
                function = 2,
            });
            root.FontName = "ＭＳ 明朝";
            root.FontSize = 72.0;
            root.FontStyle = (int)System.Drawing.FontStyle.Regular;
            return root;
        }

        public class FormState
        {
            public string Name { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int FormWindowState { get; set; }
        }

        public class KeyMap
        {
            public int Key { get; set; }
            public bool Control { get; set; }
            public bool Shift { get; set; }
            public bool Alt { get; set; }
            public int function { get; set; }
        }

        public class RootObject
        {
            public List<FormState> FormStates { get; set; }
            public List<KeyMap> KeyMaps { get; set; }
            public string FontName { get; set; }
            public double FontSize { get; set; }
            public int FontStyle { get; set; }
            public bool AutoImeMode { get; set; }
        }

    }
}