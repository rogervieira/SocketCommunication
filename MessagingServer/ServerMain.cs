﻿using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MessagingServer
{
    public partial class ServerMain : Form
    {
        private TextWriter _writer;
        private Server _server;
        
        public ServerMain()
        {
            InitializeComponent();
            _writer = new TextBoxStreamWriter(this.tbConsole);
            Console.SetOut(_writer);
            Console.WriteLine(FormatLogMessage("Redirecting messages..."));
            _server = new Server();
            _server.RaiseMessage += HandleMessageEvent;
            //dataGridView1.DataSource = _server.ClientSockets;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Console.WriteLine(FormatLogMessage(String.Format("Starting server {0}", Dns.GetHostName())));            
            _server.SetupServer(Int32.Parse(nudPort.Value.ToString()));
            handleGUIElements();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _server.RequestToStop();
            this.handleGUIElements();
        }

        private void handleGUIElements()
        {
            this.btnStart.Enabled = !this.btnStart.Enabled;
            this.nudPort.Enabled = !this.nudPort.Enabled;
            this.btnStop.Enabled = !this.btnStop.Enabled;
        }

        private void HandleMessageEvent(object sender, MessageEventArgs e)
        {
            Console.WriteLine(FormatLogMessage(e.Message));
        }

        private string FormatLogMessage(string message)
        {
            return String.Format("{0} : {1}", System.DateTime.Now, message);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tbConsole.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_server.ClientSockets.Count.ToString());
        }
    }
}
