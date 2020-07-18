using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.DateTime;
using System.Net;
using System.Net.Sockets;
//using AsyncTcpServer;
using LahoreSocketAsync;

namespace OrderAnnouncer_ESHOP
{
    public partial class Form1 : Form
    {
        private LahoreSocketServer mServer;

        public Form1()
        {
            InitializeComponent();
             mServer = new LahoreSocketServer();
             mServer.RaiseClientConnectedEvent += HandleClientConnected;
             mServer.RaiseTextReceivedEvent += HandleTextReceived;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Add(this.Button4 );
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //DateTime now = new DateTime();
            DateTime now = DateTime.Now;
            checkedListBox1.Items.Insert(0,
                item: $"CUSTOMER NAME --- PRICE: 52.50 --- PLUS4U --- SIZE: No2 Time: {now:HH:mm:ss}");
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = checkedListBox1.SelectedIndex;
            try
            {
                // Remove the item in the List.
                listBox1.Items.Add(checkedListBox1.SelectedItem);
                checkedListBox1.Items.RemoveAt(selectedIndex);
            }
            catch
            {
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port = Convert.ToInt32(maskedTextBox1.Text);
            mServer.StartListeningForIncomingConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(textBox1.Text.Trim());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs ccea)
        {
            conwindow.AppendText(string.Format("{0} - New client connected: {1}{2}", DateTime.Now, ccea.NewClient,
                Environment.NewLine));
        }

        void HandleTextReceived(object sender, TextReceivedEventArgs trea)
        {
            if (trea.TextReceived != "????????????????????????????????????????????????????????????????")
                checkedListBox1.Items.Insert(0,
                string.Format("{0} --- {1}",
                    DateTime.Now, trea.TextReceived,trea.ClientWhoSentText
                    ));

        }
    }
}