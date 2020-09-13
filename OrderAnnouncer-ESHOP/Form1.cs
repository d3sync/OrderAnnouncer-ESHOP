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
using System.Windows.Forms.VisualStyles;
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
            //checkedListBox1.Items.Insert(0,item: $"CUSTOMER NAME --- PRICE: 52.50 --- PLUS4U --- SIZE: No2 Time: {now:HH:mm:ss}");
            listView1.Items.Insert(0, "random order");
            listView1.Items[0].Group = listView1.Groups[0];
            ListViewItem.ListViewSubItem sbItem1 = new ListViewItem.ListViewSubItem();
            ListViewItem.ListViewSubItem sbItem2 = new ListViewItem.ListViewSubItem();
            ListViewItem.ListViewSubItem sbItem3 = new ListViewItem.ListViewSubItem();
            sbItem1.Text = "Customer Name Test";
            listView1.Items[0].SubItems.Insert(1, sbItem1);
            sbItem2.Text = "23.50";
            listView1.Items[0].SubItems.Insert(2, sbItem2);
            sbItem3.Text = "Big Ass Box";
            listView1.Items[0].SubItems.Insert(3, sbItem3);
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
            conwindow.AppendText(string.Format("{0} - We started listening already {1}", Now, Environment.NewLine));
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
            {
                if (trea.TextReceived.Contains("|"))
                {
                    //checkedListBox1.Items.Insert(0, string.Format("{0} --- {1}",DateTime.Now, trea.TextReceived,trea.ClientWhoSentText));
                    string[] incomingMsg = trea.TextReceived.Split('|');
                    switch (incomingMsg[0])
                    {
                        case "ANNOUNCE":
                            listView1.Items.Insert(0, incomingMsg[1]);
                            listView1.Items[0].Group = listView1.Groups[0];
                            ListViewItem.ListViewSubItem sbItem1 = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem sbItem2 = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem sbItem3 = new ListViewItem.ListViewSubItem();
                            sbItem1.Text = incomingMsg[2];
                            listView1.Items[0].SubItems.Insert(1, sbItem1);
                            sbItem2.Text = incomingMsg[3];
                            listView1.Items[0].SubItems.Insert(2, sbItem2);
                            sbItem3.Text = incomingMsg[4];
                            listView1.Items[0].SubItems.Insert(3, sbItem3);
                            break;
                        case "CLEANSERVED":
                            for (int i = 0; i < listView1.Items.Count; i++)
                            {
                                int ii = 1;
                                if (listView1.Items[i].Group == listView1.Groups[1])
                                {
                                    listView1.Items[i].Remove();
                                }
                                ii++;
                            }
                            break;
                    }
                }
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectedIndex = listView1.SelectedItems;
            try
            {
               // ListViewItem currItem = new ListViewItem(selectedIndex.ToString());
                //var lGroups = listView1.Groups.GetEnumerator().MoveNext();
                //.Group = lGroups.
                //listView1.Items.Add(listView1.SelectedItem);
                //listView1.View = View.Details;
                //ListView.SelectedListViewItemCollection.Group = 
                //checkedListBox1.Items.RemoveAt(selectedIndex);
                listView1.SelectedItems[0].Group = listView1.Groups[1];
            }
            catch
            {
            }
        }
    }
}