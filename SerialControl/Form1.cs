using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SerialControl
{
    public partial class Form1 : Form
    {
        //设置功能码   相当于宏定义
        //Divice1
        const byte DIVICE1_OPEN = 0x01; // 设备1打开
        const byte DIVICE1_CLOSE = 0x81; // 设备1关闭
        //Divice2
        const byte DIVICE2_OPEN = 0x02; // 设备2打开
        const byte DIVICE2_CLOSE = 0x82; // 设备2关闭
        //Divice3
        const byte DIVICE3_OPEN = 0x03; // 设备3打开
        const byte DIVICE3_CLOSE = 0x83; // 设备3关闭
        //总开关
        const byte DIVICE_ALL_OPEN = 0x0C; // 全部设备打开
        const byte DIVICE_ALL_CLOSE = 0x8C; // 全部设备关闭

        byte[] SerialPortDataBuffer = new byte[1]; // 串口数据缓冲区
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             // 动态获取系统实际存在的COM口
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            if (comboBox1.Items.Count > 0)
                comboBox1.Text = comboBox1.Items[0].ToString();
            else
                comboBox1.Text = "无可用COM口";

            comboBox2.Text = "9600";  // 默认波特率9600
            // 窗体关闭时确保串口关闭
            this.FormClosing += Form1_FormClosing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                System.Media.SystemSounds.Asterisk.Play(); //播放提示音
                MessageBox.Show("请选择有效COM口", "提示");
                return;
            }
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    button1.Text = "打开串口";
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Open();
                    button1.Text = "关闭串口";
                }            
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Asterisk.Play(); //播放提示音
                MessageBox.Show("串口操作失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void WriteByteToSerialPort(byte command)
        {
            byte[] Buffer = new byte[2] {0x00, command};  //自定义数据包格式，前面加一个0x00字节
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Write(Buffer, 0, Buffer.Length);
                }
                catch (Exception ex)
                {
                    System.Media.SystemSounds.Asterisk.Play(); //播放提示音
                    MessageBox.Show("发送数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play(); //播放提示音
                MessageBox.Show("串口未打开，请先打开串口", "提示");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE1_OPEN); 
        }
        // 窗体关闭时确保串口关闭
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1 != null && serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE1_CLOSE);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE2_OPEN);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE2_CLOSE);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE3_OPEN);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE3_CLOSE);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE_ALL_OPEN);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DIVICE_ALL_CLOSE);
        }
    }
}
