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
        }
        // 


    }
}
