using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clinet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        TcpClient myclient;
        private void button1_Click(object sender, EventArgs e)
        {
            myclient = new TcpClient(textBox1.Text, int.Parse(textBox2.Text));
            richTextBox1.Text = "تم الاتصال بالسيرفر بنجاح " + textBox2.Text + "\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] mybuffer = new byte[1000];
            myclient.Client.Receive(mybuffer);
            richTextBox1.Text += "server>>";
            richTextBox1.Text += Encoding.UTF8.GetString(mybuffer);
            richTextBox1.Text += "\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // المضاف  
            byte[] mybuffer1 = Encoding.UTF8.GetBytes(textBox3.Text);
            myclient.Client.Send(mybuffer1);
            richTextBox1.Text += "clinet<<>>" + textBox3.Text + "\n";
        }
    }
}
