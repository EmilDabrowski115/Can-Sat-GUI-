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
using GMap.NET; 
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.Threading;



namespace CanSatGUI
{
    public partial class Form1 : Form
    {
        string rxString;
        public Form1()
        {
            InitializeComponent();
            SerialPort1 = new SerialPort();
            SerialPort1.PortName = "COM3";
            SerialPort1.BaudRate = 9600;
            SerialPort1.Parity = Parity.None;
            SerialPort1.DataBits = 8;
            SerialPort1.StopBits = StopBits.One;
            SerialPort1.Open();
            SerialPort1.DataReceived += myPort_DataReceived;
        }
                    
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void UpdateTextboxes(string packet)
        {
            string[] packetElems = Utils.ParsePacket(packet); // możesz odp
            
            psrtxt.Text = packetElems[1];
            tmptxt.Text = packetElems[2];
        }

        //dzialajaca mapa v1.0
        private void MapSearch_Click(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAZouhXULQgPGPckADOmiHqfCc_YvD5QzQ";
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            double lat = Convert.ToDouble(txtLat.Text);
            double longt = Convert.ToDouble(txtLong.Text);
            map.Position = new PointLatLng(lat, longt);
            map.MinZoom = 0;
            map.MaxZoom = 25;
            map.Zoom = 10;
        }

        private void txtLat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLong_TextChanged(object sender, EventArgs e)
        {

        }

        private void map_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cboxserial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataStream_TextChanged(object sender, EventArgs e)
        {
            
        } // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = SerialPort1.ReadExisting();
            this.Invoke(new EventHandler(DisplayText));
        }

        private void DisplayText(object sender, EventArgs e)
        {
            DataStream.AppendText(rxString);
            UpdateTextboxes(rxString);
        }
        //koniec czesci wyckonawczej serial port 
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

    class Utils
    {
        public static string[] ParsePacket(string packet)
        {
            // "$$ZSM-Sat,997.27,1018,END$$$"
            string[] list = packet.Split(',');
            return list;
        }
    }
}




