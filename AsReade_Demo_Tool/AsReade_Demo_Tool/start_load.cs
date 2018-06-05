using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsReade_Demo_Tool
{
    public partial class start_load : Form
    {
        public start_load()
        {
            InitializeComponent();
        }

        private void start_load_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "Welcome to the AsReader Demo Tool GUI." + "\r\n" +"\r\n" + "To connect to a reade,select the COM port that The IRI Device is attached to from the dropdown menu,then press the Connect button to connect.To automatically detect attached devices,press the Scan button.";
            this.textBox1.Enabled = false;
        }
    }
}
