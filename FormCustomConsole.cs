using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UDPServer
{
    public partial class FormCustomConsole : Form
    {
        delegate void SetTextCallback(string text);
        public static bool isActive = false;
        public static FormCustomConsole FormCustomConsolePtr = null;

        private const int texboxMaxChars = 30000; // If texbox has this number of chars, delete oldest ones
        private const int texboxDeleteOffset = 1000; // It will delete this number of chars + how much it needs to display whole event


        public FormCustomConsole()
        {
            InitializeComponent();
            isActive = true;
            FormCustomConsolePtr = this; // Static pointer to last opened instance
        }

        public static void AddText(string text) // Add text to console from every object thru static method
        {
            if (isActive) // If active, update text
            {
                FormCustomConsolePtr.UpdateText(text);
            }
            
            // Othervise ignore

        }

        private void FormCustomConsole_FormClosed(object sender, FormClosedEventArgs e)
        {
            isActive = false;
        }

        private void UpdateText(string text) // This method must not be static, and it ensures that all threads can manipulate it
        {
            if (this.textBoxConsole.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBoxConsole.Text += text;
            }
        }

        private void textBoxConsole_TextChanged(object sender, EventArgs e)
        {
            int ptr;
            if (textBoxConsole.Text.Length > texboxMaxChars)
            {
                textBoxConsole.Text = textBoxConsole.Text.Substring(texboxDeleteOffset);
                ptr = textBoxConsole.Text.IndexOf(Environment.NewLine); // Make a clean cut, from newline
                textBoxConsole.Text = textBoxConsole.Text.Substring(ptr + 1);
            }
            // Make sure we are showing last line in textbox
            textBoxConsole.SelectionStart = textBoxConsole.Text.Length;
            textBoxConsole.ScrollToCaret();
            
        }

        public static void WriteLine(string text) // Add text to console from every object thru static method
        {
            if (isActive) // If active, update text
            {
                FormCustomConsolePtr.UpdateTextLn(text);
            }
        }

        private void UpdateTextLn(string text) // This method must not be static, and it ensures that all threads can manipulate it
        {
            if (this.textBoxConsole.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(WriteLine);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBoxConsole.Text += text + "\r\n";
            }
        }

    }
}
