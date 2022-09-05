using LARGEWords.Controller;
using LARGEWords.DataStore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LARGEWords
{
    public partial class MainForm : Form
    {
        private string clearUndoBuffer;

        public MainForm()
        {
            InitializeComponent();

            StringBuilder s = new StringBuilder();
            s.Append(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            s.Append(" ");
            s.Append(Application.ProductVersion);
            Text = s.ToString();

            Load += MainForm_Load;
            FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Settings.RestoreForm(this);
            textBox1.Font = new Font(Settings.Data.FontName, (float)Settings.Data.FontSize, (FontStyle)Enum.ToObject(typeof(FontStyle), Settings.Data.FontStyle));
            textBox1.KeyDown += TextBox1_KeyDown;
            if (Settings.Data.AutoImeMode) { textBox1.ImeMode = ImeMode.On; }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.SaveForm(this);
            Settings.Save();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeysFunction func = KeyMapper.KeyEventArgsToFunction(e);
            switch (func)
            {
                case KeysFunction.ClearWords:
                    textBox1.Clear();
                    break;
                case KeysFunction.Settings:
                    FontDialog fd = new FontDialog();
                    fd.Font = textBox1.Font;
                    fd.FontMustExist = true;
                    fd.AllowVerticalFonts = true;
                    fd.ShowColor = false;
                    fd.ShowEffects = false;
                    if (fd.ShowDialog() != DialogResult.Cancel)
                    {
                        //TextBox1のフォントと色を変える
                        textBox1.Font = fd.Font;
                        textBox1.ForeColor = fd.Color;
                    }
                    break;
                case KeysFunction.ExitApplication:
                    Close();
                    break;
            }
        }

        private void Undo()
        {
            if (clearUndoBuffer != null)
            {
                // TextBox.Clear()してしまうと、Undoバッファも消えてしまう。
                // TextBox.Clear()してもUndoできるように対応した
                textBox1.Text = clearUndoBuffer;
                clearUndoBuffer = null;
            }
            else
            {
                textBox1.Undo();
            }

        }
    }
}