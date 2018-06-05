using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System.Management;

namespace AsReade_Demo_Tool
{
    
   
    public partial class Form1 : Form
    {
        public static Form1 myform1;
        public GetSerialName mygetserialname = new GetSerialName();
        static UInt32 time_ms = 0;
        public bool Communication_Result = false;
        public Form1()
        {
            InitializeComponent();
            myform1 = this;
            this.serialPort1.DataReceived += new SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
        }

        private void abortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("欢迎使用AsReaderBoxMini", "AsReaderBoxMini Demo Tool V1.0.0");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Disconnect.Enabled = false;
            Reset.Enabled = false;
            start_load myloadform = new start_load();
            myloadform.TopLevel = false;
            this.panel1.Controls.Add(myloadform);
            myloadform.FormBorderStyle = FormBorderStyle.None;
            myloadform.BringToFront();
            myloadform.Show();
            for (int i = 1; i < 100; i++)
            {
                Com.Items.Add("COM" + i.ToString());
            }
            this.comboBox1.Text = "2:info";
            this.Com.Text = "COM1";
            this.comboBox2.Text = "2:info";
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            mygetserialname.Connect_To_Load();
        }

        private void Scan_Click(object sender, EventArgs e)
        {
            mygetserialname.Scan_Serila_Port();    
        }

       private void Disconnect_Click(object sender, EventArgs e)
       {
           mygetserialname.Stop_Connect();
           this.Com.Enabled = true;
           this.Disconnect.Enabled = false;
           this.Reset.Enabled = false;
           this.Connect.Enabled = true;
           this.Scan.Enabled = true;
       }

      private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
      {
          int n = this.serialPort1.BytesToRead;
          byte[] buf = new byte[n];
          this.serialPort1.Read(buf, 0, n);
          timer1.Stop();
          Communication_Result = true;
          time_ms = 0;
      }

      private void timer1_Tick(object sender, EventArgs e)
      {
          time_ms++;
          if (time_ms >= 100)
          {
              timer1.Stop();
              time_ms = 0;
          }
      }

      private void clear_Click(object sender, EventArgs e)
      {
          textBox3.Clear();
      }
    }
}
