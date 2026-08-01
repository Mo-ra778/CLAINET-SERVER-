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
using System.Net;
using System.Threading;

namespace server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient cypclint;

        private void button1_Click(object sender, EventArgs e)
        {
            // التحقق الآمن من أن المربع يحتوي على رقم بورت صحيح
            if (!int.TryParse(textBox2.Text.Trim(), out int port))
            {
                MessageBox.Show("يرجى كتابة رقم البورت (مثلاً 5050) في textBox2 أولاً!");
                return; // إيقاف التنفيذ لتجنب الـ Crash
            }
             myserver = new TcpListener(IPAddress.Any, int.Parse(textBox2.Text));
            myserver.Start();
            richTextBox1.Text += "انتضار الاتصال \n";
            MessageBox.Show("تم فتح الاتصال \n");
            Thread t = new Thread(AcceptCon);
            t.Start();
        }
        TcpListener myserver;

        void AcceptCon()
        {
            while (true)
            {
                cypclint = myserver.AcceptTcpClient();
                Invoke((Action)(() =>
                {
                    richTextBox1.Text += "تم دخول اتصال جديد \n";

                    IPEndPoint ipep= cypclint.Client.LocalEndPoint as IPEndPoint;
                    richTextBox1.Text += ipep.Address+"\n";
                }));
                Thread th1 = new Thread(ReciveMessage);
                th1.Start();
            }
        }
        void ReciveMessage()
        {
            while (true)
            {
                byte[] mybuffer = new byte[1000];
                cypclint.Client.Receive(mybuffer);
                Invoke((Action)(() =>
                {
                    richTextBox1.Text += "server>>";
                    richTextBox1.Text += Encoding.UTF8.GetString(mybuffer);
                    richTextBox1.Text += "\n";
                }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] mybuffer = Encoding.UTF8.GetBytes(textBox1.Text);
            cypclint.Client.Send(mybuffer);
            richTextBox1.Text += "server<<>>" + textBox1.Text + "\n";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //المضاف
            byte[] mybuffer1 = new byte[1000];
            cypclint.Client.Receive(mybuffer1);
            richTextBox1.Text += "CLAINT>>";
            richTextBox1.Text += Encoding.UTF8.GetString(mybuffer1);
            richTextBox1.Text += "\n";
        }
    }
}
