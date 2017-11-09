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
    public partial class FormConnectionString : Form
    {
        public Server_v2 serverPtr2 = null;
        //string path = AppDomain.CurrentDomain.BaseDirectory + @"constr.txt";

        public FormConnectionString()
        {
            InitializeComponent();
        }

        private void FormConnectionString_Load(object sender, EventArgs e)
        {
            
            textBox1.Text = Storage.ReadConnectionString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Storage.UpdateConnectionString(textBox1.Text); // Update connection string with whatever is in texbox
            this.Hide();
        }
    }
}
