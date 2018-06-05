using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace AsReade_Demo_Tool
{
    public partial class Seting_load : Form
    {
        GetSerialName getserilananme = new GetSerialName();
        public Seting_load()
        {
            InitializeComponent();
        }
        
        private void stop_Click(object sender, EventArgs e)
        {
            
        }

        private void start_inventory_Click(object sender, EventArgs e)
        {
            Form1.myform1.mygetserialname.Start_Inventory();

            //getserilananme.Start_Inventory();
            start_inventory.Enabled = false;
            stop_inventory.Enabled = true;
        }

        private void stop_inventory_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("stop_inventory_Click");
            Form1.myform1.mygetserialname.Stop_Inventory();
            //getserilananme.Stop_Inventory();
            start_inventory.Enabled = true;
            stop_inventory.Enabled = false;
        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Seting_load_Load(object sender, EventArgs e)
        {
            comboBox13.Text = comboBox13.Items[0].ToString();
            comboBox14.Text = comboBox14.Items[0].ToString();
            comboBox15.Text = comboBox15.Items[0].ToString();
            comboBox16.Text = comboBox16.Items[0].ToString();

            comboBox17.Text = comboBox17.Items[0].ToString();
            comboBox18.Text = comboBox18.Items[0].ToString();
            comboBox19.Text = comboBox19.Items[0].ToString();
            comboBox20.Text = comboBox20.Items[0].ToString();

            comboBox21.Text = comboBox21.Items[0].ToString();
            stop_inventory.Enabled = false;

        }


    }
}
