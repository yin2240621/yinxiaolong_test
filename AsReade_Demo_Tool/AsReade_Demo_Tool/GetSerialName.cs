using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO.Ports;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
namespace AsReade_Demo_Tool
{

    public class GetSerialName
    {
        [DllImport("AsReaderBoxMini.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static UInt32 AsReaderConnect(char* Serial_Name);
        [DllImport("AsReaderBoxMini.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static UInt32 AsReaderStartInventory();
        [DllImport("AsReaderBoxMini.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static UInt32 AsReaderStop();
        [DllImport("AsReaderBoxMini.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static UInt32 AsReaderDisconnect();

        [DllImport("AsReaderBoxMini.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static UInt32 AsReaderOutputData(UInt16* buff, UInt16* len);

        private bool start_scan_flag = false;

        unsafe public void Connect_Device()
        {
           char* p = (char*)Marshal.StringToHGlobalAnsi(Form1.myform1.Com.SelectedItem.ToString()).ToPointer();
           AsReaderConnect(p);//Connect AsReaderBoxMini Device
          
               Seting_load mysetingload = new Seting_load();
               mysetingload.TopLevel = false;
               Form1.myform1.panel1.Controls.Add(mysetingload);
               mysetingload.FormBorderStyle = FormBorderStyle.None;
               mysetingload.BringToFront();
               mysetingload.Show();

               Form1.myform1.textBox3.Text = "AsReaderBoxMini Device Connect Success"+"\r\n";
               Form1.myform1.Connect.Enabled = false;
               Form1.myform1.Disconnect.Enabled = true;
               Form1.myform1.Reset.Enabled = true;
               Form1.myform1.Com.Enabled = false;
               Form1.myform1.Scan.Enabled = false;
         }

        public void Connect_To_Load()//连接加载设置页面
        {
            byte[] data = { 0x8D, 0x70, 0x6A, 0x21, 0x02, 0x40, 0x00, 0x18, 0x00, 0xEC, 0x1A, 0x0A, 0x32, 0x08, 0x22, 0x06, 0x08, 0x0A, 0x18, 0x00, 0x20, 0x00, 0xDF, 0xEB };

            try
            {
                if (Form1.myform1.serialPort1.IsOpen)
                {
                    Form1.myform1.serialPort1.Close();
                    Form1.myform1.serialPort1.PortName = Form1.myform1.Com.SelectedItem.ToString();
                    Form1.myform1.serialPort1.Open();
                    Form1.myform1.serialPort1.Write(data, 0, data.Length);
                    if (Form1.myform1.timer1.Enabled == false)
                    {
                        Form1.myform1.timer1.Start();
                    }
                    Thread.Sleep(200);
                    if (Form1.myform1.Communication_Result == true)
                    {
                        Form1.myform1.Communication_Result = false;
                        Form1.myform1.serialPort1.Close();
                        Connect_Device();
                    }
                    else
                    {
                        Form1.myform1.textBox3.Text += "Serial is open,but not connect with device" + "\r\n";
                    }
                }
                else 
                {
                    Form1.myform1.serialPort1.PortName = Form1.myform1.Com.SelectedItem.ToString();
                    Form1.myform1.serialPort1.Open();
                    Form1.myform1.serialPort1.Write(data, 0, data.Length);
                    if (Form1.myform1.timer1.Enabled == false)
                    {
                        Form1.myform1.timer1.Start();
                    }
                    Thread.Sleep(200);
                    if (Form1.myform1.Communication_Result == true)
                    {
                        Form1.myform1.Communication_Result = false;
                        Form1.myform1.serialPort1.Close();
                        Connect_Device();
                    }
                    else
                    {
                        Form1.myform1.textBox3.Text += "Serial is open,but not connect with device." + "\r\n";
                    }
                }

            }
            catch (Exception ex)
            {
                Form1.myform1.textBox3.Text += ex.Message + "\r\n";
            }
        }

        public void Scan_Serila_Port()
        {
            string[] serial_port = SerialPort.GetPortNames();
            byte[] data = { 0x8D, 0x70, 0x6A, 0x21, 0x02, 0x40, 0x00, 0x18, 0x00, 0xEC, 0x1A, 0x0A, 0x32, 0x08, 0x22, 0x06, 0x08, 0x0A, 0x18, 0x00, 0x20, 0x00, 0xDF, 0xEB };

            if (null == serial_port)
            {
                Form1.myform1.Com.Items.Clear();
            }
            else
            {
                Form1.myform1.Com.Items.Clear();
                for (int i = 0; i < serial_port.Length; i++)
                {
                    try
                    {
                        if (Form1.myform1.serialPort1.IsOpen)
                        {
                            Form1.myform1.serialPort1.Close();
                            Form1.myform1.serialPort1.PortName = serial_port[i];//将搜索的串口号赋值给串口
                            Form1.myform1.serialPort1.Open();
                            Form1.myform1.serialPort1.Write(data, 0, data.Length);

                            if (Form1.myform1.timer1.Enabled == false)
                            {
                                Form1.myform1.timer1.Start();//开启定时器。
                            }

                            Thread.Sleep(200);//休眠200ms 等待串口数据的接受
                            if (Form1.myform1.Communication_Result == true)
                            {
                                Form1.myform1.Com.Items.Add(serial_port[i]);
                                Form1.myform1.Com.SelectedItem = Form1.myform1.Com.Items[0];
                                Form1.myform1.Communication_Result = false;
                            }
                            //此处做一个判断 是否收到数据的判断
                            Form1.myform1.serialPort1.Close();//判断完毕后将串
                        }
                        else
                        {
                            Form1.myform1.serialPort1.PortName = serial_port[i];//将当前集合的串口名赋值给串口;
                            Form1.myform1.serialPort1.Open();
                            Form1.myform1.serialPort1.Write(data, 0, data.Length);

                            if (Form1.myform1.timer1.Enabled == false)
                            {
                                Form1.myform1.timer1.Start();
                            }
                            Thread.Sleep(200);
                            if (Form1.myform1.Communication_Result == true)
                            {
                                Form1.myform1.Com.Items.Add(serial_port[i]);
                                Form1.myform1.Com.SelectedItem = Form1.myform1.Com.Items[0];
                                Form1.myform1.Communication_Result = false;
                            }
                            Form1.myform1.serialPort1.Close();//判断完毕后将串口关闭
                        }
                    }
                    catch(Exception ex)
                    {
                        Form1.myform1.textBox3.Text += ex.Message + "\r\n";
                    }
                }
            }
        }

        public void Start_Inventory()
        {
            if (start_scan_flag == false)
            {
                start_scan_flag = true;
                ThreadStart ts = new ThreadStart(Deal_Thread_Start_Inventory);
                Thread t = new Thread(ts);
                t.Start();

                ts = new ThreadStart(Rec_Inventory_Data);
                Thread t_1 = new Thread(ts);
                t_1.Start();
            }
            
        }
        public void Stop_Inventory()
        {
            MessageBox.Show(DateTime.Now.ToString());
            //string a = DateTime.Now.ToString();
            if (start_scan_flag == true)
            {
                start_scan_flag = false;
                AsReaderStop();
                MessageBox.Show(DateTime.Now.ToString());
            }
        }
        public void Stop_Connect()
        {
            Stop_Inventory();
            AsReaderDisconnect();

            start_load mystartload = new start_load();
            mystartload.TopLevel = false;
            Form1.myform1.panel1.Controls.Add(mystartload);
            mystartload.FormBorderStyle = FormBorderStyle.None;
            mystartload.BringToFront();
            mystartload.Show();
            Form1.myform1.textBox3.Clear();
        }
        public void Deal_Thread_Start_Inventory()
        {
            UInt32 error = 0;
            error = AsReaderStartInventory();
            MessageBox.Show("Deal_Thread_Start_Inventory");
            //if (error == 1)
            //{
                
            //    return;
            //}

        }
        unsafe public void Rec_Inventory_Data()
        {
            UInt32 error = 0;
            //error
            UInt16* mem_data = stackalloc UInt16[100];
            UInt16 data_len =0;
            string str_epc;
            //MessageBox.Show("Rec_Inventory_Data");
            while (start_scan_flag == true)
            {
                error = AsReaderOutputData(mem_data, &data_len);
                if (data_len > 0)
                {
                    str_epc = Data16_to_str_epc(mem_data, data_len);
                    Form1.myform1.textBox3.Invoke(new MethodInvoker(delegate { Form1.myform1.textBox3.Text += str_epc; }));     
                    Form1.myform1.textBox3.Invoke(new MethodInvoker(delegate { Form1.myform1.textBox3.Text += "\r\n";}));
                    Form1.myform1.textBox3.Invoke(new MethodInvoker(delegate { Form1.myform1.textBox3.Focus(); }));
                    //Form1.myform1.textBox3.Invoke(new MethodInvoker(delegate { Form1.myform1.textBox3.Focus(); }));
                    Form1.myform1.textBox3.Invoke(new MethodInvoker(delegate { Form1.myform1.textBox3.Select(Form1.myform1.textBox3.TextLength, 0); }));
                    Form1.myform1.textBox3.Invoke(new MethodInvoker(delegate { Form1.myform1.textBox3.ScrollToCaret(); }));
                    Thread.Sleep(100);
                    data_len = 0;
                }
            }
        }

        unsafe public string Data16_to_str_epc(UInt16* mem_data,UInt16 data_len)
        {
            int i = 0;
            string str_epc ="";
            uint data_of_short;
            for (i = 0; i < (data_len/2); i++)
            {
                if ((i % 1 == 0) && i > 0)
                {
                    str_epc += "-";
                }
                data_of_short = 0;
                data_of_short = ((mem_data[i] & 0xFF00U) >> 8) | ((mem_data[i] &0x00FFU) << 8);
                str_epc += data_of_short.ToString("x4");
            }
            return str_epc;
        }
    }
}
