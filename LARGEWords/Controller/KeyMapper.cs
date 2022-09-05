using LARGEWords.DataStore;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LARGEWords.Controller
{
    public class KeyMapper
    {
        public static KeysFunction KeyEventArgsToFunction(KeyEventArgs e)
        {
            DebugWriteLine(e);
            foreach (var i in Settings.Data.KeyMaps)
            {
                if (i.Key == e.KeyValue && IsPressedTargetModifiers(i, e))
                    return (KeysFunction)Enum.ToObject(typeof(KeysFunction), i.function);
            }
            return KeysFunction.None;
        }

        private static bool IsPressedTargetModifiers(SettingJson.KeyMap keyMap, KeyEventArgs e)
        {
            return ((e.Modifiers == Keys.Control) == keyMap.Control) &&
                   ((e.Modifiers == Keys.Shift) == keyMap.Shift) &&
                   ((e.Modifiers == Keys.Alt) == keyMap.Alt);
        }

        private static void DebugWriteLine(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.ControlKey | Keys.Control) || e.KeyData == (Keys.ShiftKey | Keys.Shift) || e.KeyData == (Keys.Menu | Keys.Alt)) return;

            StringBuilder s = new StringBuilder();
            s.Append("Key Down Event: ");
            if (e.KeyData == Keys.Control) s.Append("Ctrl + ");
            if (e.KeyData == Keys.Shift) s.Append("Shift + ");
            if (e.KeyData == Keys.Alt) s.Append("Alt + ");
            s.Append(e.KeyData);
            Debug.WriteLine(s);
        }

    }
}